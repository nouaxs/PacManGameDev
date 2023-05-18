using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy : MonoBehaviour
{
    private Vector2 startPosition;
    private Transform player;
    [SerializeField] private Seeker seeker;
    [SerializeField] private float speed;
    [SerializeField] private int damage;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;

    private IEnemyState state = new EnemyChaseState(); // State of the nemey set
    private Vector2 target;
    private float nextWaypointDistance = 0.1f;
    private Path curPath;
    private int curWaypoint;
    private bool reachedEndOfPath;


    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        speed = Random.Range(speed - 50, speed + 50);
        player = GameObject.FindGameObjectWithTag("Player").transform;

        InvokeRepeating(nameof(GetNewPath), 0, 0.5f);
    }

    public void SetState(IEnemyState state)
    {
        this.state = state;
    }

    /**
     * Generates a new path for the enemy to follow by using the Seeker component from the A* algorithm package
     */
    void GetNewPath()
    {
        seeker.StartPath(rb.position, state.getTargetPosi(player.position, transform.position), OnPathGenerated);
    }

    /**
     * Handles the generated path to check for errors and act accordingly
     */
    void OnPathGenerated(Path p)
    {
        if (!p.error)
        {
            curPath = p;
            curWaypoint = 0; 
        }
    }

    void UpdatePathfinding()
    {
        // Do not do anything if we do not have a path
        if (curPath != null)
        {

            // Check if we are at the end of the path or not
            if (curWaypoint >= curPath.vectorPath.Count)
            {
                reachedEndOfPath = true;
                return;
            }
            else
            {
                reachedEndOfPath = false;
            }

            // Move to our path
            Vector2 dir = ((Vector2)curPath.vectorPath[curWaypoint] - rb.position).normalized;
            rb.AddForce(dir * speed * Time.fixedDeltaTime);

            // Iterate to next waypoint once we are close to the current one
            float distance = Vector2.Distance(rb.position, curPath.vectorPath[curWaypoint]);
            if (distance < nextWaypointDistance)
            {
                curWaypoint += 1;
            }
        }
    }



    void FixedUpdate()
    {
        animator.SetFloat("Velocity", rb.velocity.magnitude);
        if (rb.velocity.x >= 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (GameManager.singleton.isPaused)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        UpdatePathfinding();
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
