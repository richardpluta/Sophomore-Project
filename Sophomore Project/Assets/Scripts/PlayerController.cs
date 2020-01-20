/*
  Rafael Montes 
  1/17/2020
  Sophmore Project

  Description: This code will allow the player to move.
 */

using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private LayerMask platformsLayerMask;
    public float speed = 20.0f;
    public float horizontalInput;


    
    // Update is called once per frame
    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * Time.deltaTime * speed * horizontalInput);
    }

    
    
}