﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character {
    float deathTime = 0f;

    public EnemyAI ai;
    public EnemyDistanceIA distAi;
    public bool isBoss;

    public SalleManager manager;

    public int borneInfMoney, borneSupMoney;

    protected override void Start()
    {
        base.Start();
        speed = 4f;
        maxHealth = GameObject.Find("GameManager").GetComponent<GameManager>().numeroNiveau * 16 + 100;
        //lifeBar = GameObject.FindGameObjectWithTag("EnemyLifeBar").GetComponent<LifeBar>();
        lifeBar.SetProgress(currentHealth / maxHealth);
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

    public override void Die()
    {
        base.Die();
        if (!isBoss)
        {
            GetComponent<BoxCollider2D>().enabled = false;
            manager.RemoveEnemy();
        }
        
        lifeBar.EnableLifeBar(false);
        
        dropItem();
        if(ai != null)
        {
            ai.enabled = false;
        } else if( distAi != null)
        {
            distAi.enabled = false;
        }

        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().gold += UnityEngine.Random.Range(borneInfMoney, borneSupMoney);
    }

    public override void Attack(float range)
    {
        base.Attack(range);
        Vector2 directionCoup = new Vector2(GetComponent<Animator>().GetFloat("DirectionX"), GetComponent<Animator>().GetFloat("DirectionY"));
        directionCoup.Normalize();
        LayerMask mask = LayerMask.GetMask("Player");
        RaycastHit2D hit = Physics2D.Raycast(transform.position, directionCoup, range , mask);
        if (hit.collider != null && hit.collider.gameObject.tag == "Player")
        {
            float rnd = UnityEngine.Random.Range(0, 100);
            if (rnd > 20) //coup normal
            {
                hit.collider.GetComponent<Player>().TakeDamage(10, directionCoup, 0.2f, false);
            }
            else // coup critique
            {
                hit.collider.GetComponent<Player>().TakeDamage(20, directionCoup, 0.2f, true);
            }
        }
    }

    public void Attack(GameObject proj)
    {
        base.Attack(2f);
        Vector3 direction = GameObject.Find("Player").transform.position - transform.position;
        direction.Normalize();
        GameObject projectile = Instantiate(proj, gameObject.transform.position, Quaternion.identity);
        projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(direction.x * distAi.projectileSpeed, direction.y * distAi.projectileSpeed);
        projectile.GetComponent<Projectile>().isFriendly = false;


    }

    public override void TakeDamage(float damage, Vector3 hitVector, float force, bool crit)
    {
        lifeBar.EnableLifeBar(true);
        base.TakeDamage(damage, hitVector, force, crit);
    }

    public void dropItem()
    {
        
        if(UnityEngine.Random.Range(0f,100f) > 90)
        {
            GameObject objInst = Instantiate(GameObject.Find("PoolDropGobelin").GetComponent<Pool>().tire(), this.gameObject.transform.position, Quaternion.identity);
            GameObject.Find("GameManager").GetComponent<GameManager>().listeObjetsDroppés.Add(objInst);
            objInst.GetComponent<Items>().price = objInst.GetComponent<Items>().price * 1 + GameObject.Find("GameManager").GetComponent<GameManager>().numeroNiveau / 10;
            if (objInst.GetComponent<Weapon>() != null)
            {
                objInst.GetComponent<Weapon>().damage = objInst.GetComponent<Weapon>().damage * (int)(Math.Pow(1.023f, GameObject.Find("GameManager").GetComponent<GameManager>().numeroNiveau));
            }
        }
    }

}
