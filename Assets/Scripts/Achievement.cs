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
                    GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>().ShowAchievement(achievementName,5f);
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

    public int GetAchievementStatus(string achievementName)
    {
        foreach (AchievementData achievement in data)
        {
            if (achievement.name == achievementName)
            {
                return achievement.status;
            }
        }

        return 0; // Default status if achievement name is not found
    }
}


[Serializable]
public struct AchievementData
{
    public string name;
    public int status;
}
