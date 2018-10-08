using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public abstract class Character : MonoBehaviour {

    [SerializeField]
    private float speed;

    protected Vector2 direction;

    protected Animator animator;
    private Rigidbody2D rigidBody;
    protected SpriteRenderer sprite;

    [SerializeField]
    protected Transform hitBox;

    public bool isMoving
    {
        get
        {
            return direction.x != 0 || direction.y != 0;
        }
    }

    public float maxHealth = 100f;
    public float currentHealth;

    protected bool isAlive;


    // Use this for initialization
    protected virtual void Start () {
        currentHealth = maxHealth;
        isAlive = true;
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	protected virtual void Update () {
        if (!isAlive)
        {
            return;
        }
        HandleMovement();
	}

    private void FixedUpdate()
    {
        if (!isAlive)
        {
            return;
        }
        Move();
    }

    public void Move()
    {
        rigidBody.velocity = direction.normalized * speed;
    }

    public void HandleMovement()
    {
        if (isMoving)
        {
            animator.SetBool("IsMoving", true);
            animator.SetFloat("DirectionX", direction.x);
            animator.SetFloat("DirectionY", direction.y);
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }
    }

    public virtual void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            isAlive = false;
            animator.SetBool("IsAlive", false);
        }
    }

}
