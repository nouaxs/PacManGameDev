using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer spriteRend;
    [SerializeField] private Animator animator;

    private LevelComplete levelComplete;
    private bool empowered;
    private Vector2 startPosition;
    private IPlayerDirection curDirection = new PlayerDirectionRight();

    void Start()
    {
        levelComplete = GameObject.FindGameObjectWithTag("LevelComplete").GetComponent<LevelComplete>();
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.singleton.isPaused) {
            animator.SetFloat("Velocity", rb.velocity.magnitude);
            if (empowered) { 
                spriteRend.color = Color.red;
            } 
            else
            {
                spriteRend.color = Color.white;
            }
            if (Input.GetKey(KeyCode.W)) {
                curDirection = new PlayerDirectionUp();
            } else if (Input.GetKey(KeyCode.A)) {
                transform.localScale = new Vector3(-1, 1, 1);
                curDirection = new PlayerDirectionLeft();
            } else if (Input.GetKey(KeyCode.S)) {
                curDirection = new PlayerDirectionDown();
            } else if (Input.GetKey(KeyCode.D)) {
                transform.localScale = new Vector3(1, 1, 1);
                curDirection = new PlayerDirectionRight();
            }
        }
    }

    void FixedUpdate() {
        if (!GameManager.singleton.isPaused) {
            //transform.Translate(new Vector2(curDirection.getX(), curDirection.getY()) * speed * Time.deltaTime);
            rb.velocity = new Vector2(curDirection.getX(), curDirection.getY()) * speed * Time.fixedDeltaTime;
        } else
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "Food")
        {
            Food food = other.transform.GetComponent<Food>();
            if (food != null)
            {
                GameManager.singleton.addScore(food.data.points);
                if (food.data.isPowerUp)
                {
                    StopCoroutine(nameof(Powerup));
                    StartCoroutine(nameof(Powerup));
                }
                levelComplete.DecrementFood();
                Destroy(other.gameObject);
            }

        }
    }

    private IEnumerator Powerup()
    {
        empowered = true;
        GameManager.singleton.SetEnemiesState(new EnemyScaredState());
        yield return new WaitForSeconds(5f);
        empowered = false;
        GameManager.singleton.SetEnemiesState(new EnemyChaseState());
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        
        if (other.transform.tag == "Enemy")
        {
            Enemy enemy = other.transform.GetComponent<Enemy>();
            if (enemy != null)
            {
                if (empowered)
                {
                    enemy.transform.position = enemy.GetStartPosition();
                    GameManager.singleton.addScore(20);
                } else { 
                    GameManager.singleton.RemoveLives(enemy.GetDamage());
                    transform.position = startPosition;
                    GameManager.singleton.RestartEnemies();
                }
            }
        }
    }
}

