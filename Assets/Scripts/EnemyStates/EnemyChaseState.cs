using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Enemy chase state implementing the state pattern
 * Inherites from IEnemyState
 * This is the default state of the enemies where they track the player and chase him
 */
public class EnemyChaseState : IEnemyState
{ 
    // The enemies target in this case will be the player's position
    public Vector2 getTargetPosi(Vector2 playerPosi, Vector2 enemyPosi)
    {
        return playerPosi;
    }
}
