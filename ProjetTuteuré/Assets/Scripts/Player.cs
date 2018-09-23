using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : Actor {

    Vector2 directionMovement;


    protected override void Start () {
        base.Start();
	}

    void Update() {

        directionMovement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if(directionMovement != Vector2.zero)
        {
            animator.SetFloat("DirectionX", directionMovement.x);
            animator.SetFloat("DirectionY", directionMovement.y);
            animator.SetBool("IsMoving", true);
        } else
        {
            animator.SetBool("IsMoving", false);
        }

        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("Attacking");
        }
    }

    void FixedUpdate()
    {
        rigidBody.MovePosition(rigidBody.position + directionMovement * speed * Time.deltaTime);
    }
}
