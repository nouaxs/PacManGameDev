using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class MainMenu : MonoBehaviour
{
    public Dropdown profileDropdown;
    public InputField nameField;
    public GameObject profileDataPanel;

    // Start is called before the first frame update
    void Start()
    {
        RebuildDropdown();
    }

    void RebuildDropdown()
    {
        profileDropdown.options.Clear();
        string[] profiles = Directory.GetFiles(Application.dataPath + "/profiles/", "*.profile");
        List<string> profilesList = new List<string>();
        foreach (string s in profiles)
        {
            
            string pname = s.Substring(s.LastIndexOf("/") + 1);
            if (pname.Equals(".profile"))  
            {
                continue;
            }
            profilesList.Add(pname.Substring(0, pname.LastIndexOf(".")));
        }
        profileDropdown.AddOptions(profilesList);
        if(profileDropdown.options.Count > 0)
        {
            ProfileChanged(0);
        }
    }


    public void PlayButtonClicked()
    {
        //SceneManager.LoadScene(1);
        GameManager.singleton.LoadScene(1);
    }

    public void ProfileChanged(int id)
    {
        string s = profileDropdown.options[id].text;
        GameManager.singleton.LoadProfile(s);
    }

    public void CreateProfileButton()
    {
        string filePath = Application.dataPath + "/profiles/" + nameField.text + ".profile";
        if (!File.Exists(filePath))
        {
            File.WriteAllText(Application.dataPath + "/profiles/" + nameField.text + ".profile", "");
            RebuildDropdown();

        }

    }

    public void ShowProfileButton()
    {
        int selectedIndex = profileDropdown.value;
        string profileName = profileDropdown.options[selectedIndex].text;
        string filePath = Application.dataPath + "/profiles/" + profileName + ".profile";

        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            ProfileData profileData = JsonUtility.FromJson<ProfileData>(jsonData);

            profileDataPanel.SetActive(true);
            Text profileText = profileDataPanel.GetComponentInChildren<Text>();
            profileText.text = "Profile Name: " + profileName + "\n";
            profileText.text += "Highest score: " + profileData.highScore + "\n";
            if (profileData.flawlessStatus > 1)
            {
                profileText.text += "Flawless achievement was obtained" + "\n";
            } else
            {
                profileText.text += "Flawless achievement was not obtained" + "\n";
            }
            if (profileData.fruitlessStatus > 1)
            {
                profileText.text += "Fruitless achievement was obtained" + "\n";
            }
            else
            {
                profileText.text += "Fruitless achievement was not obtained" + "\n";
            }
        }
    }
        //string filePath = Application.dataPath + "/profiles/" + pname + ".profile";

        //if (File.Exists(filePath))
        //{
        //    string jsonData = File.ReadAllText(filePath);
        //    ProfileData profileData = JsonUtility.FromJson<ProfileData>(jsonData);

        //    // Display the profile data
        //    Debug.Log("Profile Name: " + pname);
        //    Debug.Log("High Score: " + profileData.highScore);
        //    Debug.Log("Flawless Status: " + profileData.flawlessStatus);
        //    Debug.Log("Fruitless Status: " + profileData.fruitlessStatus);
        //}
        //else
        //{
        //    Debug.Log("Profile does not exist!");
        //}

        public void ClosePopupPanel()
        {
            profileDataPanel.SetActive(false);
        }
    }



