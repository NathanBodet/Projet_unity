using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

    public Transform[] patrolPoints;
    public float speed, checkTime;
    Transform currentPatrolPoint;
    int currentPatrolIndex;
    public Rigidbody2D rigidBody;
    public Animator animator;
    public GameObject fov;
    public bool estEnChasse;

    public GameObject target; // cible (joueur)

    void Start()
    {
        currentPatrolIndex = 0;
        currentPatrolPoint = patrolPoints[currentPatrolIndex];
        rigidBody.GetComponent<Rigidbody2D>();
        animator.GetComponent<Animator>();
        estEnChasse = false;
        speed = 6;
    }

    void Update()
    {

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
        }
    }

    public void JoueurDetecte(GameObject joueur) //Appel lorsque le fov touche le joueur
    {
        estEnChasse = true;
        speed = 4;
        target = joueur;

    }

    void FixedUpdate()
    {
        if (estEnChasse)
        {

            var layermask1 = 1 << 10;
            layermask1 = ~layermask1;
            RaycastHit2D vision = Physics2D.Raycast(transform.position, -transform.position + target.transform.position, 2f,layermask1);
            Debug.Log(vision.collider);
            if(vision.collider == null || vision.collider.gameObject.tag == "Player" )
            {
                estEnChasse = false;
                speed = 6;
            }
            else
            {
                Vector2 dir = (-transform.position + target.transform.position);
                dir.Normalize();
                transform.Translate(dir * speed * Time.deltaTime);
            }
            
        } else
        {
            Vector3 patrolPointDir = currentPatrolPoint.position - transform.position;
            animator.SetFloat("DirectionX", patrolPointDir.x);
            animator.SetFloat("DirectionY", patrolPointDir.y);
            animator.SetBool("IsMoving", true);
            patrolPointDir.Normalize();
            transform.Translate(patrolPointDir * speed * Time.deltaTime);
            //rigidBody.MovePosition(transform.position + (patrolPointDir * speed) * Time.deltaTime);
        }

    }
}
