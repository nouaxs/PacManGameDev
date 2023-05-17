using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Achievement : MonoBehaviour
{
    public AchievementData[] data;
    public static Achievement singleton;

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
    }

    public void SetAchievement(string achievementName, int status, bool save)
    {
        for (int i = 0; i < data.Length; i++)
        {
            if (data[i].name == achievementName && data[i].status != status)
            {
                if (save) {
                    Debug.Log("You have just unlocked " + achievementName + "!");
                }
                
                data[i].status = status;
                break;
            }
        }
        if (save)
        {
            GameManager.singleton.SaveCurrentProfile();
        }
        
    }
}

[Serializable]
public struct AchievementData
{
    public string name;
    public int status;
}