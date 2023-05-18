using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScaredState : IEnemyState
{
    public Vector2 getTargetPosi(Vector2 playerPosi, Vector2 enemyPosi)
    {
        Vector2 dir = (enemyPosi - playerPosi).normalized;
        return enemyPosi + dir;
    }
}
