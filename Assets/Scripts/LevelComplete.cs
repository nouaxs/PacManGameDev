using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour
{
    int foodAmount;
    
    // Start is called before the first frame update
    void Start()
    {
        foodAmount = GameObject.FindGameObjectsWithTag("Food").Length;
        
        
    }


    public void DecrementFood()
    {
        foodAmount -= 1;
        if (foodAmount <= 0)
        {
            if (SceneManager.sceneCountInBuildSettings > SceneManager.GetActiveScene().buildIndex + 1) {
                if(GameManager.singleton.GetLives() == 3 && SceneManager.GetActiveScene().buildIndex == 1)
                {
                    Achievement.singleton.SetAchievement("Flawless", 1, true);
                }
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            } else
            {
                Debug.Log("All levels completed yay");
            }
            
        }
    }

}
