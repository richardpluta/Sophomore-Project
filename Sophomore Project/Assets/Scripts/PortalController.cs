using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class PortalController : MonoBehaviour
{
    private CharacterController2D player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController2D>();
        if (player == null) {
            Debug.Log("Player not found!");
        }
    }

    private void OnTriggerEnter2D(Collider2D obj) {
        if (obj.tag == "Player") {
            Debug.Log("Level finished!");
            
            // TODO : Implement Level End
            player.GetComponent<SpriteRenderer>().enabled = !(player.GetComponent<SpriteRenderer>().enabled);   // Temporary for now
        }
    }
}
