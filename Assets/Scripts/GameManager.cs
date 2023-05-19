using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using static System.TimeZoneInfo;


public class GameManager : MonoBehaviour
{
    [SerializeField] private int score;
    [SerializeField] private int lives;
    public static GameManager singleton;
    private int highScore;
    private string curProfile;
    public bool isPaused;

    private void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        lives = 3;

    }

    public void addScore(int amount)
    {
        score += amount;
    }

    public int GetScore()
    {
        return score;
    }

    public int GetLives()
    {
        return lives;
    }


    /**
     * Function responsible of decrementing lives
     * If no more lives, loads the next scene
     * If the player loses all his lives without collecting any fruits, Fruitless achievement obtained
     */
    public void RemoveLives(int amount)
    {
        lives -= amount;
        if (lives <= 0)
        {
            if (score == 0 && SceneManager.GetActiveScene().buildIndex == 1)
            {
                Achievement.singleton.SetAchievement("Fruitless", 1, true);
            }
            SaveCurrentProfile();
            LoadScene(0);
        }
    }

    /**
     * Resets all enemies positions
     */
    public void RestartEnemies()
    {
        foreach (GameObject e in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            if (e.transform.GetComponent<Enemy>() != null)
            {
                e.transform.position = e.transform.GetComponent<Enemy>().GetStartPosition();
            }
        }

    }


    public void setHighScore(int newHighscore)
    {
        highScore = newHighscore;
    }

    public int GetHighScore()
    {
        return highScore;
    }

    public string GetCurProfile()
    {
        return curProfile;
    }

    /**
     * Function responsible of saving the currently selected profile's data into a file using JSON
     */
    public void SaveCurrentProfile()
    {
        ProfileData profileData = new ProfileData();
        profileData.name = curProfile;
        if (highScore < score)
        {
            profileData.highScore = score;
        }
        profileData.flawlessStatus = Achievement.singleton.GetAchievementStatus("Flawless");
        profileData.fruitlessStatus = Achievement.singleton.GetAchievementStatus("Fruitless");

        string jsonData = JsonUtility.ToJson(profileData);
        File.WriteAllText(Application.dataPath + "/profiles/" + curProfile + ".profile", jsonData);
    }

    /**
     * Function responsible of deserializing a JSON file containing a profile
     */
    public void LoadProfile(string profile)
    {
        curProfile = profile;
        string filePath = Application.dataPath + "/profiles/" + curProfile + ".profile";

        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);

            if (!string.IsNullOrEmpty(jsonData))
            {
                ProfileData profileData = JsonUtility.FromJson<ProfileData>(jsonData);

                highScore = profileData.highScore;
                Achievement.singleton.SetAchievement("Flawless", profileData.flawlessStatus, false);
                Achievement.singleton.SetAchievement("Fruitless", profileData.fruitlessStatus, false);
            }
            else
            {
                // Handle the case where the file is empty by initializing with default values
                ProfileData defaultProfileData = new ProfileData();
                defaultProfileData.name = profile;
                defaultProfileData.highScore = 0;
                defaultProfileData.flawlessStatus = 0;
                defaultProfileData.fruitlessStatus = 0;

                string defaultJsonData = JsonUtility.ToJson(defaultProfileData);
                File.WriteAllText(filePath, defaultJsonData);

                // Set the values from the default profile
                highScore = defaultProfileData.highScore;
                Achievement.singleton.SetAchievement("Flawless", defaultProfileData.flawlessStatus, false);
                Achievement.singleton.SetAchievement("Fruitless", defaultProfileData.fruitlessStatus, false);
            }
        }
    }


    public void LoadScene(int sceneIndex)
    {
        StartCoroutine(DoSceneTransition(sceneIndex));
    }

    public IEnumerator DoSceneTransition(int sceneIndex)
    {
        isPaused = true;
        GameObject.FindGameObjectWithTag("SceneTransitioner").GetComponent<SceneTransitioner>().transitionAnimator.SetBool("Hide", true);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(sceneIndex);
    }

    /**
     * Sets the enemies state using the state pattern
     */
    public void SetEnemiesState(IEnemyState state)
    {
        foreach (Enemy e in GameObject.FindObjectsOfType<Enemy>())
        {
            e.SetState(state);
        }
    }
}
