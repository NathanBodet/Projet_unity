using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : Actor {

    Vector2 directionMovement;
    public GameObject ball;
    Vector2 ballPos;
    public float fireRate = 0.5f;
    float nextFire = 0.0f;
    bool typeArmeEquipee = true; // true -> arme de cac, false -> arme a distance


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
            if(!typeArmeEquipee && Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                fire();
            }
        }
    }

    public void fire()
    {
        ballPos = transform.position;
        ballPos += new Vector2(1f,0f);
        Vector3 shootDirection;
        shootDirection = Input.mousePosition;
        shootDirection.z = 0.0f;
        shootDirection = Camera.main.ScreenToWorldPoint(shootDirection);
        shootDirection = transform.position - shootDirection;


        GameObject ballInstance = Instantiate(ball, ballPos, Quaternion.identity);
        ballInstance.GetComponent<Rigidbody2D>().velocity = new Vector2(shootDirection.x * speed, shootDirection.y * speed);


    }

    void FixedUpdate()
    {
        rigidBody.MovePosition(rigidBody.position + directionMovement * speed * Time.deltaTime);
    }
}
