using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character {
    
    public static int TotalEnemies;
    float deathTime = 0f;


    public EnemyAI ai;

    protected override void Start()
    {
        base.Start();
        speed = 4f;
    }

    public void RegisterEnemy()
    {
        TotalEnemies++;
    }

    protected override void Update()
    {
        if (!isAlive)
        {
            if(deathTime == 0f)
            {
                deathTime = Time.time;
            } else
            {
                if(Time.time > deathTime + 10f)
                {
                    Destroy(gameObject);
                }
            }
        }
        
    }

    private void FixedUpdate()
    {
        if (!isAlive)
        {
            return;
        }

       
    }

    public void StopMovement()
    {
        rigidBody.velocity = Vector2.zero;
        speed = 0f;
        animator.SetBool("IsMoving", false);
    }

    protected override void Die()
    {
        base.Die();
        ai.enabled = false;
        //gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        //gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        TotalEnemies--;
    }

}
