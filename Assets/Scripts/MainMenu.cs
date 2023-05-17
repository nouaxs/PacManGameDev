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
        SceneManager.LoadScene(1);
    }

    public void ProfileChanged(int id)
    {
        string s = profileDropdown.options[id].text;
        GameManager.singleton.LoadProfile(s);
    }

    public void CreateProfileButton()
    {
        if (!File.Exists(Application.dataPath + "/profiles/" + nameField.text + ".profile"))
        {
            File.WriteAllText(Application.dataPath + "/profiles/" + nameField.text + ".profile", "");
            RebuildDropdown();

        }

    }


}