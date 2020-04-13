using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyController : MonoBehaviour
{

    [SerializeField] private int Speed;
    [SerializeField] private int MaxHealth;
    [SerializeField] private int Damage;
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
    private Animator animator;

    private Transform Player;

    private bool Grounded;
    private bool FacingRight; 
    private Vector2 Velocity = Vector2.zero;
    private int Health;

    private bool SeenPlayer;
    private bool DamageDebounce;

    public UnityEvent OnLandEvent;

    private void Awake()
    {
        GroundCheck = gameObject.transform.GetChild(0);
        CeilingCheck = gameObject.transform.GetChild(1);
        Renderer = GetComponent<SpriteRenderer>();
        Collider = GetComponent<CapsuleCollider2D>();
        Rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        Player = GameObject.Find("Player").transform;

        Health = MaxHealth;
        Damage = Damage == 0 ? 2 : Damage;
        FacingRight = transform.rotation.y.Equals(0) ? false : true;

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

        if (DamageDebounce)
        {
            Move(0f, false);
        } else if (SeenPlayer)
        {
            //move towards player on X, jump if something is in its way
            Vector2 offset = Player.position - transform.position;
            float horizontalMove = offset.normalized.x * Speed;
            animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
            RaycastHit2D hit = Physics2D.Raycast(GroundCheck.position, -GroundCheck.right, .25f, LayerMask.GetMask("Ground"));
            bool jump = (hit.transform != null) || (offset.y > .5f);

            Move(horizontalMove * Time.fixedDeltaTime, jump);
        } else if (Vector2.Distance(Player.position, transform.position) <= DetectionRange)
        {
            SoundManagerScript.PlaySound("EnemySeesCharacter");

            RaycastHit2D hit = Physics2D.Raycast(transform.position, Player.position - transform.position, 10f, LayerMask.GetMask("Default", "Ground"));
            SeenPlayer = hit.transform == Player;

        }
    }

    private void Move(float move, bool jump)
    {
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
        SoundManagerScript.PlaySound("EnemyDie");
        StatsController.EnemiesKilled++;
        Renderer.enabled = false;
        Collider.enabled = false;
        GameObject.Destroy(this, 0);
    }

    private IEnumerator DamagePlayer()
    {
        DamageDebounce = true;
        playerHealth health = Player.GetComponent<playerHealth>();
        health.damagePlayer(Damage);
        if (health.health <= 0)
            SeenPlayer = false;
        yield return new WaitForSeconds(.5f);
        DamageDebounce = false;
    }

    public void DamageSelf(int amount)
    {
        Health -= amount;

        if (Health <= 0)
            Kill();
    }

    private void OnTriggerStay2D(Collider2D obj)
    {
        if (obj.gameObject.transform == Player)
            StartCoroutine(DamagePlayer());
    }
}
