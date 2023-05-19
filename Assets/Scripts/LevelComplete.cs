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

    /**
     * Function keeping track of the number of food elements on the board
     * if the food finishes, the next scene is loaded
     * if the player ate all the food without dying at all, they get the Flawless achievement
     */
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
