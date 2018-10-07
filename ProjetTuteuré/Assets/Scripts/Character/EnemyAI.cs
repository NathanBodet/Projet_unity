using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

    public Transform[] patrolPoints;
    public float speed;
    Transform currentPatrolPoint;
    int currentPatrolIndex;
    public Rigidbody2D rigidBody;
    public Animator animator;

    void Start()
    {
        currentPatrolIndex = 0;
        currentPatrolPoint = patrolPoints[currentPatrolIndex];
        rigidBody.GetComponent<Rigidbody2D>();
        animator.GetComponent<Animator>();
    }

    void Update()
    {

        //check to see if reached patrol point
        if(Vector3.Distance(transform.position, currentPatrolPoint.position) < .1f)
        {
            //check to see if is anymore patrol points (not go back to the beginning)
            if(currentPatrolIndex + 1 < patrolPoints.Length)
            {
                currentPatrolIndex++;
            } else
            {
                currentPatrolIndex = 0;
            }
            currentPatrolPoint = patrolPoints[currentPatrolIndex];
        }
    }

    void FixedUpdate()
    {
        Vector3 patrolPointDir = currentPatrolPoint.position - transform.position;
        animator.SetFloat("DirectionX", patrolPointDir.x);
        animator.SetFloat("DirectionY", patrolPointDir.y);
        animator.SetBool("IsMoving", true);
        transform.Translate(patrolPointDir * speed * Time.deltaTime);
        //rigidBody.MovePosition(transform.position + (patrolPointDir * speed) * Time.deltaTime);
    }
}
