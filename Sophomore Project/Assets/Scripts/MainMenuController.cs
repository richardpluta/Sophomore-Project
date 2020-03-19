using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    Dictionary<string, GameObject> frames = new Dictionary<string, GameObject>();
    string[] frameStack = new string[4];
    int top = 0;

    GameObject currentFrame;

    void Awake()
    {
        frames.Add("TitleScreen", gameObject.transform.Find("TitleScreen").gameObject);
        frames.Add("LevelSelect", gameObject.transform.Find("LevelSelect").gameObject);
        frames.Add("SettingsMenu", gameObject.transform.Find("SettingsMenu").gameObject);
        frames.Add("KeybindsMenu", gameObject.transform.Find("KeybindsMenu").gameObject);

        currentFrame = frames["TitleScreen"];

        foreach (GameObject button in currentFrame.Find("Buttons").gameObject)
        {

        }

        foreach (GameObject frame in frames.Values)
        {
            Transform backButton = frame.transform.Find("BackButton");
            if (backButton != null)
            {
                backButton.gameObject.GetComponent<Button>().onClick.AddListener(popFrame);
            }
            Debug.Log(frame.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void setCurrentFrame(string newFrame)
    {
        if (frames[newFrame] != null)
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
        frameStack[top++] = frameName;
        setCurrentFrame(frameStack[top]);
    }

    private void popFrame()
    {
        frameStack[top--] = null;
        setCurrentFrame(frameStack[top]);
    }
}
