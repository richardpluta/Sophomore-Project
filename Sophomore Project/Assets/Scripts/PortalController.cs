using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;


public class PortalController : MonoBehaviour
{
    private CharacterController2D player;
    private float startTime;
    private string levelName;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController2D>();
        if (player == null) {
            Debug.Log("Player not found!");
        }

        startTime = Time.time;
        levelName = SceneManager.GetActiveScene().name;
    }

    private void OnTriggerEnter2D(Collider2D obj) {
        if (obj.tag == "Player") {
            Debug.Log("Level finished!");

            float levelTime = Time.time - startTime;
            float oldRecord = SceneController.GetLevelRecord(levelName);

            if (!oldRecord.Equals(0f) ? oldRecord > levelTime : true)
                SceneController.SetLevelRecord(levelName, levelTime);
            SceneController.SetLevelCompleted(levelName, true);
            SceneController.ChangeScene("MainMenu");
        }
    }
}
