using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public ScriptableFood data;
    private LevelComplete lvComplete;

    void Start()
    {
        lvComplete = GameObject.FindGameObjectWithTag("LevelComplete").GetComponent<LevelComplete>();
    }

    private void OnDestroy()
    {
        lvComplete.DecrementFood();
    }
}
