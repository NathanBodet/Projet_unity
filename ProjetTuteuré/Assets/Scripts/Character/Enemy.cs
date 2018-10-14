using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character {
    
    public static int TotalEnemies;

    /*public Transform[] patrolPoints;
    public float checkTime;
    public Transform currentPatrolPoint;
    int currentPatrolIndex;
    public GameObject fov;*/
    //public bool estEnChasse;

    //public GameObject target; // cible (joueur)

    public EnemyAI ai;

    protected override void Start()
    {
        base.Start();
        /*currentPatrolIndex = 0;
        currentPatrolPoint = patrolPoints[currentPatrolIndex];
        estEnChasse = false;*/
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
            return;
        }
        /*
        //check si l'ennemi chasse le joueur
        if (!estEnChasse)
        {
            //check to see if reached patrol point
            if (Vector3.Distance(transform.position, currentPatrolPoint.position) < .1f)
            {
                //check to see if is anymore patrol points (not go back to the beginning)
                if (currentPatrolIndex + 1 < patrolPoints.Length)
                {
                    currentPatrolIndex++;
                }
                else
                {
                    currentPatrolIndex = 0;
                }
                currentPatrolPoint = patrolPoints[currentPatrolIndex];
            }
        }*/
    }

   /*public void JoueurDetecte(GameObject joueur) //Appel lorsque le fov touche le joueur
    {
        //estEnChasse = true;
        target = joueur;

    }*/

    private void FixedUpdate()
    {
        if (!isAlive)
        {
            return;
        }

        /*if (estEnChasse)
        {

            var layermask1 = 1 << 10;
            layermask1 = ~layermask1;
            RaycastHit2D vision = Physics2D.Raycast(transform.position, -transform.position + target.transform.position, 2f, layermask1);
            Debug.Log(vision.collider);
            if (vision.collider == null || vision.collider.gameObject.tag == "Player")
            {
                estEnChasse = false;
                speed = 4;
            }
            else
            {
                Vector2 dir = (-transform.position + target.transform.position);
                dir.Normalize();
                transform.Translate(dir * speed * Time.deltaTime);
            }

        }
        else
        {
            Vector3 patrolPointDir = currentPatrolPoint.position - transform.position;
            animator.SetFloat("DirectionX", patrolPointDir.x);
            animator.SetFloat("DirectionY", patrolPointDir.y);
            animator.SetBool("IsMoving", true);
            patrolPointDir.Normalize();
            transform.Translate(patrolPointDir * speed * Time.deltaTime);
            //rigidBody.MovePosition(transform.position + (patrolPointDir * speed) * Time.deltaTime);
        }*/
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
        TotalEnemies--;
    }

    /*public void MoveTo(Vector3 targetPositon)
    {
        Vector3 targetDirection = targetPositon - transform.position;
        targetDirection.Normalize();
        //transform.Translate(targetDirection * speed * Time.deltaTime);
        rigidBody.MovePosition(transform.position + (targetDirection * speed) * Time.deltaTime);
    }*/

}
