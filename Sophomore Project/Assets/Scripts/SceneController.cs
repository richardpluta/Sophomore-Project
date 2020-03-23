using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

static public class SceneController
{
    static Dictionary<string, bool> levels = new Dictionary<string, bool>(); //Determines if a level is unlocked or not

    static SceneController()
    {
        levels.Add("1", true);
        levels.Add("2", false);
        levels.Add("3", false);
        levels.Add("4", false);
        levels.Add("5", false);
    }
    
    static public void ChangeScene(string sceneName)
    {
        if (int.TryParse(sceneName, out int var) && IsLevelUnlocked(sceneName) && SceneManager.GetSceneByName(sceneName) != null)
        {
            SceneManager.LoadScene(sceneName);
        } else if (SceneManager.GetSceneByName(sceneName) != null) {
            SceneManager.LoadScene(sceneName);
        }
    }

    static public bool IsLevelUnlocked(string levelName)
    {
        return levels.ContainsKey(levelName) && levels[levelName];
    }

    static public void SetLevelUnlocked(string levelName, bool unlocked)
    {
        if (int.TryParse(levelName, out int var))
        {
            levels.Add(levelName, unlocked);
        }
    }
}
