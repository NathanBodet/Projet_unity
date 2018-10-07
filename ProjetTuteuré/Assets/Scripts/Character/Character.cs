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

    [SerializeField]
    protected Transform hitBox;

    public bool isMoving
    {
        get
        {
            return direction.x != 0 || direction.y != 0;
        }
    }

    public float health = 100f;

    // Use this for initialization
    protected virtual void Start () {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	protected virtual void Update () {
        HandleMovement();
	}

    private void FixedUpdate()
    {
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
        health -= damage;
        if(health <= 0)
        {
            animator.SetBool("IsAlive", false);
        }
    }

}
