using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public abstract class Character : MonoBehaviour {

    public float speed;

    protected Vector2 direction;

    public Animator animator;
    public Rigidbody2D rigidBody;
    public SpriteRenderer sprite;

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

    public virtual void Attack()
    {
        animator.SetTrigger("Attacking");
    }

    public virtual void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log(damage);
        if(currentHealth <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        isAlive = false;
        animator.SetBool("isDead", true);
        Debug.Log("ded");
    }

    public Vector2 getDirection()
    {
        return direction;
    }

}
