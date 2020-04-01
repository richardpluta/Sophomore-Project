using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Pathfinding;

public class EnemyController : MonoBehaviour
{

    [SerializeField] private int Speed;
    [SerializeField] private int MaxHealth;
    [SerializeField] private int DetectionRange;
    [SerializeField] private LayerMask WhatIsGround;
    [SerializeField] private float JumpForce = 400f;
    [Range(0, .3f)] [SerializeField] private float MovementSmoothing = .05f;  // The time it takes to change velocity

    const float GroundedRadius = .1f; // Radius of the overlap circle to determine if grounded
    const float CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up

    private Transform GroundCheck; 
    private Transform CeilingCheck;
    private SpriteRenderer Renderer;
    private CapsuleCollider2D Collider;
    private Rigidbody2D Rigidbody2D;
    private Seeker Seeker;

    private Transform Player;

    private bool Grounded;
    private bool FacingRight = false; 
    private Vector2 Velocity = Vector2.zero;
    private int Health;

    private bool UsingPath;
    [SerializeField] private float NextWaypointDistance;
    private Path Path;
    private int CurrentWaypoint;

    private bool SeenPlayer;
    


    public UnityEvent OnLandEvent;

    private void Awake()
    {
        GroundCheck = gameObject.transform.GetChild(0);
        CeilingCheck = gameObject.transform.GetChild(1);
        Renderer = GetComponent<SpriteRenderer>();
        Collider = GetComponent<CapsuleCollider2D>();
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Seeker = GetComponent<Seeker>();

        Player = GameObject.Find("Player").transform;

        Health = MaxHealth;

        UsingPath = false;
        SeenPlayer = false;
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
   
        if (SeenPlayer)
        {
            //Debug.Log("Enemy is engaging player");
            //move towards player on X, jump if something is in its way
            Vector2 offset = Player.position - transform.position;
            float horizontalMove = offset.normalized.x * Speed;

            //Debug.Log("Offset: " + offset + "\nHorizontal: " + horizontalMove);
            RaycastHit2D hit = Physics2D.Raycast(GroundCheck.position, -GroundCheck.right, .25f, LayerMask.GetMask("Ground"));
            bool jump = (hit.transform != null) || (offset.y > .5f);

            //bool jump = (Physics2D.Raycast(transform.position, -transform.right, .25f, LayerMask.GetMask("Ground")) != null);
            Debug.Log("Jump: " + jump);

            Move(horizontalMove * Time.fixedDeltaTime, jump);
        } else if (Vector2.Distance(Player.position, transform.position) <= DetectionRange)
        {
            Debug.Log("Enemy in range");
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Player.position - transform.position, 10f, LayerMask.GetMask("Default", "Ground"));
            Debug.Log("Hit: " + hit.transform);
            SeenPlayer = hit.transform == Player;

        }
        /*
        //Check if player is in range or not
        if (!UsingPath)
        {
            Debug.Log("Enemy not following path");
            if (Vector2.Distance(Player.position, transform.position) <= DetectionRange)
            {
                Debug.Log("Player within range");
                UsingPath = true;
                this.Seeker.StartPath(this.Rigidbody2D.position, this.Player.position, OnPathComplete);
            }
            else
            {

            }
        } else if (this.Path != null) 
        {
            Debug.Log("Path is not null");
            if (CurrentWaypoint >= this.Path.vectorPath.Count)
            {
                Debug.Log("Current waypoint is outside of the path");
                this.Path = null;
                CurrentWaypoint = 0;
                UsingPath = false;
                return;
            }

            Vector2 direction = ((Vector2)this.Path.vectorPath[CurrentWaypoint] - this.Rigidbody2D.position).normalized;
            Vector2 force = direction * Speed * Time.deltaTime;

            Debug.Log("Current waypoint: " + this.Path.vectorPath[CurrentWaypoint] + "Current position: " + this.Rigidbody2D.position + "Direction:" + direction + "\nForce: " + force);

            this.Rigidbody2D.AddForce(force);

            float distance = Vector2.Distance(this.Rigidbody2D.position, this.Path.vectorPath[CurrentWaypoint]);

            if (distance < NextWaypointDistance)
            {
                CurrentWaypoint++;
            }

        } 
        */
    }

    private void Move(float move, bool jump)
    {
        Debug.Log("Move called, grounded: " + Grounded + " move: " + move);


        // Move the character by finding the target velocity
        Vector2 targetVelocity = new Vector2(move * 10f, Rigidbody2D.velocity.y);
        // And then smoothing it out and applying it to the character
        Rigidbody2D.velocity = Vector2.SmoothDamp(Rigidbody2D.velocity, targetVelocity, ref Velocity, MovementSmoothing);

        // If enemy is moving in a direction they are not facing
        if ((move > 0 && !FacingRight) || (move < 0 && FacingRight)) 
            Flip();

        // If the Enemy should jump...
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

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            Debug.Log("Path generated");
            this.Path = p;
            CurrentWaypoint = 0;
        }
    }

    
}
