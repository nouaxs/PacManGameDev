using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Enemy scared state implementing the state pattern
 * Inherites from IEnemyState
 * This is the state the ghosts enter when the player consumes a powered up food
 */
public class EnemyScaredState : IEnemyState
{
    public Vector2 getTargetPosi(Vector2 playerPosi, Vector2 enemyPosi)
    {
        Vector2 dir = (enemyPosi - playerPosi).normalized;
        return enemyPosi + dir;
    }
}
