using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyState
{
    public Vector2 getTargetPosi(Vector2 playerPosi, Vector2 enemyPosi);
}

