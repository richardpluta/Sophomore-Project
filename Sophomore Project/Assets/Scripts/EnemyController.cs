using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyController : MonoBehaviour
{

    [SerializeField] private int Speed;
    [SerializeField] private int MaxHealth;
    [SerializeField] private int ActivationRange;
    [SerializeField] private LayerMask WhatIsGround;
    [SerializeField] private float JumpForce = 400f;
    [Range(0, .3f)] [SerializeField] private float MovementSmoothing = .05f;  // How much to smooth out the movement

    const float GroundedRadius = .09f; // Radius of the overlap circle to determine if grounded
    const float CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up

    private Transform GroundCheck; 
    private Transform CeilingCheck;
    private SpriteRenderer Renderer;
    private CircleCollider2D Collider;
    private Rigidbody2D Rigidbody2D;

    private bool Grounded;
    private bool FacingRight = true; 
    private Vector3 Velocity = Vector3.zero;
    private int Health;

    


    public UnityEvent OnLandEvent;

    private void Awake()
    {
        GroundCheck = gameObject.transform.GetChild(0);
        CeilingCheck = gameObject.transform.GetChild(1);
        Renderer = GetComponent<SpriteRenderer>();
        Collider = GetComponent<CircleCollider2D>();
        Rigidbody2D = GetComponent<Rigidbody2D>();


        Health = MaxHealth;
        
        Debug.Log("GroundCheck: " + GroundCheck);
        Debug.Log("CeilingCheck: " + CeilingCheck);

        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();
    }

    private void FixedUpdate()
    {
        bool wasGrounded = Grounded;
        Grounded = false;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(GroundCheck.position, GroundedRadius, WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                Grounded = true;
                if (!wasGrounded)
                    OnLandEvent.Invoke();
            }
        }
    }

    private void Move(float move, bool jump)
    {
        //only control the player if grounded or airControl is turned on
        if (Grounded)
        {
            // Move the character by finding the target velocity
            Vector3 targetVelocity = new Vector2(move * 10f, Rigidbody2D.velocity.y);
            // And then smoothing it out and applying it to the character
            Rigidbody2D.velocity = Vector3.SmoothDamp(Rigidbody2D.velocity, targetVelocity, ref Velocity, MovementSmoothing);

            // If enemy is moving in a direction they are not facing
            if ((move > 0 && !FacingRight) || (move < 0 && FacingRight)) 
                Flip();
        }
        // If the player should jump...
        if (Grounded && jump)
        {
            // Add a vertical force to the player.
            Grounded = false;
            Rigidbody2D.AddForce(new Vector2(0f, JumpForce));
        }
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        FacingRight = !FacingRight;

        transform.Rotate(0f, 180f, 0f);
    }
    
    private void Kill()
    {
        Renderer.enabled = false;
        Collider.enabled = false;
    }

    public void Damage(int amount)
    {
        Health -= amount;

        if (Health <= 0)
            Kill();
    }
}
