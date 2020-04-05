using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelTimerController : MonoBehaviour
{
    public Text timerText;
    private float startTime;

    // Start is called before the first frame update
    void Start()
    {
        timerText = GetComponent<Text>() as Text;
        startTime = Time.time;   
    }

    // Update is called once per frame
    void Update()
    {
        float t = Time.time - startTime;

        int minutes;
        float seconds, mseconds;
        minutes  = (int) t / 60;
        seconds  = t % 60;
        mseconds = (seconds - (int) seconds) * 100;


        string minText, secText, msText;
        minText = minutes.ToString().PadLeft(2, '0');
        secText = ((int) seconds).ToString().PadLeft(2, '0');
        msText  = ((int) mseconds).ToString().PadRight(2, '0');

        timerText.text = minText + ":" + secText + "." + msText;
    }
}
