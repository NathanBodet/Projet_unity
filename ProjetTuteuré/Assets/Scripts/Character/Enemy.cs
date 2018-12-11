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
        //lifeBar = GameObject.FindGameObjectWithTag("EnemyLifeBar").GetComponent<LifeBar>();
        lifeBar.SetProgress(currentHealth / maxHealth);
    }

    public void RegisterEnemy()
    {
        TotalEnemies++;
    }

    protected override void Update()
    {
        Vector3 pos = new Vector3(gameObject.transform.position.x - 0.2f, gameObject.transform.position.y + 1.2f, gameObject.transform.position.z);
        lifeBar.transform.position = Camera.main.WorldToScreenPoint(pos);

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
        GetComponent<BoxCollider2D>().enabled = false;
        lifeBar.EnableLifeBar(false);
        TotalEnemies--;
    }

    public override void Attack()
    {
        base.Attack();
        Vector2 directionCoup = new Vector2(GetComponent<Animator>().GetFloat("DirectionX"), GetComponent<Animator>().GetFloat("DirectionY"));
        directionCoup.Normalize();
        LayerMask mask = LayerMask.GetMask("Player");
        RaycastHit2D hit = Physics2D.Raycast(transform.position, directionCoup, 1f, mask);
        if (hit.collider != null && hit.collider.gameObject.tag == "Player")
        {
            float rnd = Random.Range(0, 100);
            if (rnd > 20) //coup normal
            {
                hit.collider.GetComponent<Player>().TakeDamage(10, directionCoup,0.2f);
            }
            else // coup critique
            {
                hit.collider.GetComponent<Player>().TakeDamage(20, directionCoup,0.2f);
            }
        }
    }

    public override void TakeDamage(float damage, Vector3 hitVector, float force)
    {
        lifeBar.EnableLifeBar(true);
        base.TakeDamage(damage, hitVector, force);
    }

}
