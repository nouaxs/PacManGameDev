using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text scoreTxt;
    public Text livesTxt;
    public Text pausedTxt;
    public GameObject achievementPanel;
    public Text achievementTxt;
    public GameObject pausePanel;
    public GameObject pauseButton;

    
    public void PauseButton()
    {
        GameManager.singleton.isPaused = !GameManager.singleton.isPaused;
        pausePanel.SetActive(true);
        pauseButton.SetActive(!GameManager.singleton.isPaused);
    }

    // Update is called once per frame
    void Update()
    {
        pausedTxt.gameObject.SetActive(GameManager.singleton.isPaused);
        scoreTxt.text = "score: " + GameManager.singleton.GetScore();
        livesTxt.text = "lives: " + GameManager.singleton.GetLives();
    }

    public void ShowAchievement(string achievementName, float showDuration)
    {
        StartCoroutine(DoAchievement(achievementName, showDuration));
    }

    public IEnumerator DoAchievement(string achievement, float showDuration)
    {
        Debug.Log(showDuration);
        achievementTxt.text = "Congrats! You have earned " + achievement + " achievement!";
        achievementPanel.SetActive(true);
        yield return new WaitForSeconds(showDuration);
        achievementPanel.SetActive(false);
    }

    public void RestartButton()
    {
        GameManager.singleton.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    public void ExitButton()
    {
        GameManager.singleton.LoadScene(0);
    }


    public void ResumeButton()
    {
        pausePanel.SetActive(false);
        GameManager.singleton.isPaused = false;
        pauseButton.SetActive(true);
    }
}
