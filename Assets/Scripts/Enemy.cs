using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Transform player;
    [SerializeField] private float speed;
    [SerializeField] private int damage;
    private Vector2 startPosition;
    [SerializeField] private Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.singleton.isPaused)
        {
            return;
        }
        Vector2 moveDir = player.position - transform.position;
        //transform.Translate(moveDir.normalized * speed * Time.deltaTime);
        rb.velocity = moveDir.normalized * speed * Time.deltaTime;
    }

    public int GetDamage()
    {
        return damage;
    }

    public Vector2 GetStartPosition()
    {
        return startPosition;
    }


}
