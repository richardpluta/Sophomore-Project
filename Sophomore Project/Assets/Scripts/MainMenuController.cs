using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    Dictionary<string, GameObject> frames = new Dictionary<string, GameObject>();
    string[] frameStack = new string[4];
    int top = -1;
    float volume = 50;
    float brightness = 50;
    int difficulty = 0;

    GameObject currentFrame;

    void Awake()
    {
        frames.Add("TitleScreen", gameObject.transform.Find("TitleScreen").gameObject);
        frames.Add("LevelSelect", gameObject.transform.Find("LevelSelect").gameObject);
        frames.Add("SettingsMenu", gameObject.transform.Find("SettingsMenu").gameObject);
        frames.Add("KeybindsMenu", gameObject.transform.Find("KeybindsMenu").gameObject);
        frames.Add("StatsMenu", gameObject.transform.Find("StatsMenu").gameObject);

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
                    case "Stats":
                        changeFrame("StatsMenu");
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
                        string levelName = button.gameObject.name;
                        bool unlocked = SceneController.GetLevelUnlocked(levelName);
                        bool completed = SceneController.GetLevelCompleted(levelName);
                        float record = SceneController.GetLevelRecord(levelName);

                        if (!record.Equals(0f))
                        {
                            int minutes = (int)record / 60;
                            float seconds = record % 60;
                            button.Find("Record").gameObject.GetComponent<Text>().text = "" + minutes + ":" + seconds.ToString("F2");
                        }

                        if (unlocked)
                        {
                            button.GetComponent<Image>().color = completed ? new Color((float)209/255, (float)134/255, (float)50/255, 1) : new Color((float)221 / 255, (float)216 / 255, (float)43 / 255, 1);
                            button.gameObject.GetComponent<Button>().onClick.AddListener(delegate {
                                SceneController.ChangeScene(button.gameObject.name);
                            });
                        } else
                        {
                            button.GetComponent<Image>().color = new Color((float)109/255,(float)103/255,(float)96/255,1);
                        }
                    }
                    break;
                case "SettingsMenu":
                    foreach (Transform settingPanel in frame.transform.Find("Panel").Find("Settings"))
                    {
                        switch (settingPanel.gameObject.name)
                        {
                            case "Difficulty":
                                {
                                    Slider slider = settingPanel.Find("Slider").gameObject.GetComponent<Slider>();

                                    slider.onValueChanged.AddListener(delegate { changeDifficulty((int)slider.value); });
                                    break;
                                }
                            case "Brightness":
                                {
                                    Slider slider = settingPanel.Find("Slider").gameObject.GetComponent<Slider>();
                                    InputField inputField = settingPanel.Find("InputField").GetComponent<InputField>();

                                    slider.onValueChanged.AddListener(delegate {
                                        inputField.text = "" + slider.value;
                                        changeBrightness((int)slider.value);
                                    });

                                    inputField.onEndEdit.AddListener(delegate {
                                        int val = int.Parse(inputField.text);
                                        slider.value = val;
                                        changeBrightness(val);
                                    });
                                    break;
                                }
                            case "Volume":
                                {
                                    Slider slider = settingPanel.Find("Slider").gameObject.GetComponent<Slider>();
                                    InputField inputField = settingPanel.Find("InputField").GetComponent<InputField>();

                                    slider.onValueChanged.AddListener(delegate {
                                        inputField.text = "" + slider.value;
                                        changeVolume((int)slider.value);
                                    });

                                    inputField.onEndEdit.AddListener(delegate {
                                        int val = int.Parse(inputField.text);
                                        slider.value = val;
                                        changeVolume(val);
                                    });
                                }
                                break;
                            case "Keybinds":
                                settingPanel.Find("Text").GetComponent<Button>().onClick.AddListener(delegate {
                                    changeFrame("KeybindsMenu");
                                });
                                break;
                        }
                    }
                    break;
                case "StatsMenu":
                    foreach (Transform statsPanel in frame.transform.Find("Panel").Find("Stats"))
                    {
                        switch(statsPanel.gameObject.name)
                        {
                            case "Deaths":
                                statsPanel.Find("Value").GetComponent<Text>().text = "" + StatsController.Deaths;
                                break;
                            case "Enemies":
                                statsPanel.Find("Value").GetComponent<Text>().text = "" + StatsController.EnemiesKilled;
                                break;
                            case "Accuracy":
                                statsPanel.Find("Value").GetComponent<Text>().text = StatsController.GetAccuracy().ToString("P");
                                break;
                            case "Hearts":
                                statsPanel.Find("Value").GetComponent<Text>().text = "" + StatsController.HeartPickups;
                                break;
                            case "Ammo":
                                statsPanel.Find("Value").GetComponent<Text>().text = "" + StatsController.AmmoPickups;
                                break;
                            case "Playtime":
                                float t = StatsController.Playtime;

                                int minutes = (int)t / 60;
                                float seconds = t % 60;
                                statsPanel.Find("Value").GetComponent<Text>().text = "" + minutes + ":" + seconds.ToString("F2");
                                break;
                        }
                    }
                    break;
            }
        }
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

    private void changeDifficulty(int newDifficulty)
    {
        difficulty = newDifficulty;
    }

    private void changeBrightness(float newBrightness)
    {
        brightness = newBrightness;
        Screen.brightness = brightness / 100;
    }

    private void changeVolume(float newVolume)
    {
        volume = newVolume;
        AudioListener.volume = volume;
    }
}
