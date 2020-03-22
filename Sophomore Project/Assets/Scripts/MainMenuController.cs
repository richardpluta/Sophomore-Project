using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    Dictionary<string, GameObject> frames = new Dictionary<string, GameObject>();
    string[] frameStack = new string[4];
    int top = -1;
    int volume = 50;
    int brightness = 50;
    int difficulty = 0;

    GameObject currentFrame;

    void Awake()
    {
        frames.Add("TitleScreen", gameObject.transform.Find("TitleScreen").gameObject);
        frames.Add("LevelSelect", gameObject.transform.Find("LevelSelect").gameObject);
        frames.Add("SettingsMenu", gameObject.transform.Find("SettingsMenu").gameObject);
        frames.Add("KeybindsMenu", gameObject.transform.Find("KeybindsMenu").gameObject);

        changeFrame("TitleScreen");

        foreach (Transform button in currentFrame.transform.Find("Buttons"))
        {
            void callMethod() {
                switch (button.gameObject.name)
                {
                    case "Play":
                        changeFrame("LevelSelect");
                        break;
                    case "Settings":
                        changeFrame("SettingsMenu");
                        break;
                    case "Credits":
                        break;
                    case "Quit":
                        Application.Quit();
                        break;
                }
            }
            button.gameObject.GetComponent<Button>().onClick.AddListener(callMethod);
        }

        foreach (GameObject frame in frames.Values)
        {
            Transform backButton = frame.transform.Find("BackButton");
            if (backButton != null)
            {
                backButton.gameObject.GetComponent<Button>().onClick.AddListener(popFrame);
            }
            
            switch (frame.name)
            {
                case "LevelSelect":
                    foreach (Transform button in frame.transform.Find("Panel").Find("Levels"))
                    {
                        void changeLevel()
                        {
                            //maybe some method call to see if level is unlocked
                            //or do it in ChangeScene
                            //sceneManager.ChangeScene(button.gameObject.name);
                        }
                        button.gameObject.GetComponent<Button>().onClick.AddListener(changeLevel);
                    }
                    break;
                case "SettingsMenu":
                    foreach (Transform settingPanel in frame.transform.Find("Panel").Find("Settings"))
                    {
                        switch (settingPanel.gameObject.name)
                        {
                            case "Difficulty":
                                break;
                            case "Brightness":
                                break;
                            case "Volume":
                                break;
                            case "Keybinds":
                                break;
                        }
                    }
                    break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void setCurrentFrame(string newFrame)
    {
        if (frames.ContainsKey(newFrame))
        {
            currentFrame = frames[newFrame];
            currentFrame.GetComponent<Canvas>().enabled = true;

            foreach (GameObject frame in frames.Values)
            {
                if (frame != currentFrame)
                {
                    frame.GetComponent<Canvas>().enabled = false;
                }
            }
        }
    }

    private void changeFrame(string frameName)
    {
        frameStack[++top] = frameName;
        setCurrentFrame(frameStack[top]);
    }

    private void popFrame()
    {
        frameStack[top--] = null;
        setCurrentFrame(frameStack[top]);
    }
}
