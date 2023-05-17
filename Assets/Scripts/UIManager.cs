using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text scoreTxt;
    public Text livesTxt;
    public Text pausedTxt;


    public void PauseButton()
    {
        GameManager.singleton.isPaused = !GameManager.singleton.isPaused;
    }

    // Update is called once per frame
    void Update()
    {
        pausedTxt.gameObject.SetActive(GameManager.singleton.isPaused);
        scoreTxt.text = "score: " + GameManager.singleton.GetScore();
        livesTxt.text = "lives: " + GameManager.singleton.GetLives();
    }
}
