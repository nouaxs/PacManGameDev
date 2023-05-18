using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;


public class GameManager : MonoBehaviour
{
    [SerializeField] int score;
    [SerializeField] int lives;
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
        } else { 
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

    public void RemoveLives(int amount)
    {
        lives -= amount;
        if (lives <= 0)
        {
            if (score == 0 && SceneManager.GetActiveScene().buildIndex == 1)
            {
                Achievement.singleton.SetAchievement("s", 1, true);
            }
            SaveCurrentProfile();
            LoadScene(0);
        }
    }

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

    public void LoadProfile(string profile)
    {
        curProfile = profile;
        string[] data = File.ReadAllLines(Application.dataPath + "/profiles/" + curProfile + ".profile");
        foreach (string s in data)
        {
            if (s.StartsWith("HighScore:"))
            {
                if (int.TryParse(s.Substring(s.IndexOf(":") + 1), out int hs)) // loading highscore if it exists
                {
                    highScore = hs;
                }
            }
            else if (s.StartsWith("Flawless:"))
            {
                if (int.TryParse(s.Substring(s.IndexOf(":") + 1), out int status)) // loading highscore if it exists
                {
                    Achievement.singleton.SetAchievement("Flawless", status, false);
                }
            }
            else if (s.StartsWith("Fruitless:"))
            {
                if (int.TryParse(s.Substring(s.IndexOf(":") + 1), out int status)) // loading highscore if it exists
                {
                    Achievement.singleton.SetAchievement("Fruitless", status, false);
                }
            }
        }
    }

    public string GetCurProfile()
    {
        return curProfile;
    }

    public void SaveCurrentProfile()
    {
        string data = "";
        if (highScore < score)
        {
            highScore = score;
        }
        data += "Highscore:" + highScore + "\n";
        for(int i = 0; i < Achievement.singleton.data.Length; i++)
        {
            data += Achievement.singleton.data[i].name + ":" + Achievement.singleton.data[i].status + "\n";
        }
        File.WriteAllText(Application.dataPath + "/profiles/" + curProfile + ".profile", data);
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

    public void SetEnemiesState(IEnemyState state)
    {
        foreach(Enemy e in GameObject.FindObjectsOfType<Enemy>())
        {
            e.SetState(state);
        }
    }

    
}
