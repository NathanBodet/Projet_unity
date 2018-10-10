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
        target = joueur;

    }

    void FixedUpdate()
    {
        if (estEnChasse)
        {
            transform.Translate((-transform.position+target.transform.position) * speed * Time.deltaTime);
        } else
        {
            Vector3 patrolPointDir = currentPatrolPoint.position - transform.position;
            animator.SetFloat("DirectionX", patrolPointDir.x);
            animator.SetFloat("DirectionY", patrolPointDir.y);
            animator.SetBool("IsMoving", true);
            transform.Translate(patrolPointDir * speed * Time.deltaTime);
            //rigidBody.MovePosition(transform.position + (patrolPointDir * speed) * Time.deltaTime);
        }

    }
}
