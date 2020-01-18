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
    public float speed = 20.0f;
    public float jumpSpeed = 10.0f;
    public float horizontalInput;
    public float verticalInput;
    private Rigidbody2D rigidbody2d;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        rigidbody2d = transform.GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Console.WriteLine("Hello");
            float jumpVelocity = 10f;
            rigidbody2d.velocity = Vector2.up * jumpVelocity;
        }
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * Time.deltaTime * speed * horizontalInput);
        

    }
}
