using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Player : MonoBehaviour
{
    public static Player instance;

    [SerializeField] float moveSpeed = 1;
    [SerializeField] float jumpHeight = 1;
    [SerializeField] int jumpCharges = 2;

    [SerializeField] public int maxHealth;
    [SerializeField] public int health;

    // Time in seconds after being hit by an orb
    // that you can't be hit by the same orb
    [SerializeField] public float orbGracePeriod;

    public bool dead;

    float moveDirection;

    Collider2D collider;
    Rigidbody2D rigidbody;
    Animator animator;
    SpriteRenderer spriteRenderer;

    InputActionMap actionMap;

    float distanceToGround;

    private void Awake()
    {
        if (instance != null) 
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        collider = GetComponent<Collider2D>();
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        actionMap = InputManager.instance.GetActionMap("Player");

    }

    private void Start()
    {
        distanceToGround = collider.bounds.extents.y;

        actionMap.FindAction("Move").performed += OnMove;
        actionMap.FindAction("Move").canceled += OnMove;

        actionMap.FindAction("Jump").performed += OnJump;

        InitializePlayer();

    }

    private void OnDisable()
    {
        actionMap.FindAction("Move").performed -= OnMove;
        actionMap.FindAction("Move").canceled -= OnMove;

        actionMap.FindAction("Jump").performed -= OnJump;
    }

    public void OnMove(InputAction.CallbackContext context) 
    {
        moveDirection = context.ReadValue<float>();
        bool isMoving = moveDirection > 0.01 || moveDirection < -0.01;
        
        animator.SetBool("isMoving", isMoving);

        if (isMoving)
            spriteRenderer.flipX = moveDirection < -0.01;
    }

    private bool isGrounded() 
    {
        string[] layers = { "Blocking" };

        RaycastHit2D hit = Physics2D.Raycast(
            collider.bounds.center, 
            Vector2.down, 
            distanceToGround + 0.1f,
            LayerMask.GetMask(layers)
            ) ;

        return hit;



    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (dead)
            return;

        if (isGrounded())
            jumpCharges = 2;
    }

    public void OnJump(InputAction.CallbackContext context) 
    {
        if (!context.performed)
            return;


        if (isGrounded())
            jumpCharges = 2;

        if (jumpCharges > 0)
            Jump();
            jumpCharges -= 1;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (dead)
            return;

        Interactable interactable;
        if (other.gameObject.TryGetComponent<Interactable>(out interactable))
            interactable.OnInteract();
    }

    private void Jump()
    {
        float yVelocity = jumpHeight;

        // if double jump
        if (jumpCharges == 0)
            yVelocity = rigidbody.velocity.y + jumpHeight / 2;

        rigidbody.velocity = new Vector2(rigidbody.velocity.x, yVelocity);
        animator.SetTrigger("Jump");
    }

    private void ApplyMovement() 
    {
        rigidbody.velocity = new Vector2(moveDirection * moveSpeed, rigidbody.velocity.y);
    }

    private void FixedUpdate()
    {
        ApplyMovement();
    }

    public void TakeDamage(int damage) 
    {
        animator.SetTrigger("TakeDamage");

        int newHealth = health - damage;
        if (newHealth > 0) 
        {
            SetHealth(newHealth);
        }
        else
        {
            SetHealth(0);
            Die();
        }
    }

    public void SetHealth(int newHealth, bool overheal = false) 
    {
        if (newHealth > maxHealth) 
        {
            if (overheal)
            {
                maxHealth = newHealth;
                health = newHealth;
            }
            else 
            {
                health = maxHealth;
            }
        }
        else 
        {
            health = newHealth; 
        }

        UIManager.instance.UpdateHealthDisplay();
    }

    public void Heal(int healAmount, bool overheal = false) 
    {
        int newHealth = health + healAmount;
        SetHealth(newHealth, overheal);
    }

    public void InitializePlayer() 
    {
        dead = false;
        SetHealth(maxHealth);
    }


    private void Die()
    {
        dead = true;
        animator.SetBool("isDead", true);
        GameManager.instance.KillPlayer();
    }
}
