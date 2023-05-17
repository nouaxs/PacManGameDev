using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private IPlayerDirection curDirection = new PlayerDirectionRight();
    [SerializeField] private float speed;
    private Vector2 startPosition;
    [SerializeField] private Rigidbody2D rb;



    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
       if (Input.GetKey(KeyCode.W))
        {
            curDirection = new PlayerDirectionUp();
        } 
        else if (Input.GetKey(KeyCode.A))
        {
            curDirection = new PlayerDirectionLeft();
        }
        else if (Input.GetKey(KeyCode.S))
        {
            curDirection = new PlayerDirectionDown();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            curDirection = new PlayerDirectionRight();
        }

        if (!GameManager.singleton.isPaused)
        {
            //transform.Translate(new Vector2(curDirection.getX(), curDirection.getY()) * speed * Time.deltaTime);
            rb.velocity = new Vector2(curDirection.getX(), curDirection.getY()) * speed * Time.deltaTime;
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
                Destroy(other.gameObject);
            }

        }
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        
        if (other.transform.tag == "Enemy")
        {
            Enemy enemy = other.transform.GetComponent<Enemy>();
            if (enemy != null)
            {
                GameManager.singleton.RemoveLives(enemy.GetDamage());
                transform.position = startPosition;
                GameManager.singleton.RestartEnemies();
            }
        }
    }
}

