using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

static public class SceneController
{
    private class Level
    {
        public string name;
        public bool unlocked;
        public bool completed;
        public float record;

        public Level(string name, bool unlocked, bool completed)
        {
            this.name = name;
            this.unlocked = unlocked;
            this.completed = completed;
            this.record = 0f;
        }
    }

    static string CurrentScene = "Main Menu";
    static Dictionary<string, Level> levels = new Dictionary<string, Level>();
    
    static SceneController()
    {
        levels.Add("1", new Level("1", true, false));
        levels.Add("2", new Level("2", false, false));
        levels.Add("3", new Level("3", false, false));
        levels.Add("4", new Level("4", false, false));
        levels.Add("5", new Level("5", false, false));
        levels.Add("6", new Level("6", false, false));
    }
    
    static public void ChangeScene(string sceneName)
    {
        if (int.TryParse(sceneName, out int var) && GetLevelUnlocked(sceneName) && SceneManager.GetSceneByName(sceneName) != null)
        {
            CurrentScene = sceneName;
            SceneManager.LoadScene(sceneName);
        } else if (SceneManager.GetSceneByName(sceneName) != null) {
            CurrentScene = sceneName;
            SceneManager.LoadScene(sceneName);
        }
    }

    static public bool GetLevelUnlocked(string levelName)
    {
        return levels.ContainsKey(levelName) && levels[levelName].unlocked;
    }

    static public void SetLevelUnlocked(string levelName, bool unlocked)
    {
        if (levels.ContainsKey(levelName) && int.TryParse(levelName, out int var))
        {
            levels[levelName].unlocked = unlocked;
        }
    }

    static public bool GetLevelCompleted(string levelName)
    {
        return levels.ContainsKey(levelName) && levels[levelName].completed;
    }

    static public void SetLevelCompleted(string levelName, bool completed)
    {
        if (levels.ContainsKey(levelName) && int.TryParse(levelName, out int var))
        {
            levels[levelName].completed = completed;
            if (levels.ContainsKey("" + (var + 1)))
            {
                levels["" + (var + 1)].unlocked = true;
            }
        }
    }

    static public float GetLevelRecord(string levelName)
    {
        return levels.ContainsKey(levelName) ? levels[levelName].record : 0f;
    }

    static public void SetLevelRecord(string levelName, float record)
    {
        if (levels.ContainsKey(levelName) && int.TryParse(levelName, out int var))
        {
            levels[levelName].record = record;
        }
    }
}
