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
    public CharacterController2D controller;
    public Animator animator;
    public float runSpeed = 40f;
    public float horizontalMove = 0f;
    bool jump = false;

    // Update is called once per frame
    void Update()
    {
        // Move our character

        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        animator.SetFloat("Speed", Math.Abs(horizontalMove));

        if (Input.GetButtonDown("Jump"))
        {
            SoundManagerScript.PlaySound("CharacterJump");
            jump = true;
            animator.SetBool("IsJumping", true);
        }

        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;

    }
    
    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
    }

}