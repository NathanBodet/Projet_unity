using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : Actor {

    Vector2 directionMovement;
    public GameObject ball;
    Vector2 ballPos;
    public float fireRate = 0.5f;
    float nextFire = 0.0f;
    float switchCooldown = 0.0f;
    bool typeArmeEquipee = false; // true -> arme de cac, false -> arme a distance


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

        if (Input.GetKey("e") && Time.time > switchCooldown) { //switch le type d'arme : cooldown de 1s
            switchCooldown = Time.time + 1f;
            Debug.Log("switch");
            typeArmeEquipee = !typeArmeEquipee;
        }

        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("Attacking");
            if(!typeArmeEquipee && Time.time > nextFire)
            {
                nextFire = Time.time + fireRate; //firerate -> cooldown de tir
                fire();
            }
        }
    }

    public void fire()
    {

        ballPos = transform.position;
        ballPos += new Vector2(0.5f,0f);

        //récupération des coordonnées de la souris et création du vecteur du projectile tiré
        Vector3 ballDir = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));//10.0f car si z = 0f, la fonction retourne la position de la caméra
        Debug.Log(ballDir);
        ballDir.x  = ballDir.x - transform.position.x;
        ballDir.y = ballDir.y - transform.position.y;
        ballDir.Normalize();

        //instanciation du projectile et addition du vecteur vitesse
        GameObject ballInstance = Instantiate(ball, ballPos, Quaternion.identity);
        ballInstance.GetComponent<Rigidbody2D>().velocity = new Vector2(ballDir.x * 5, ballDir.y * 5);


    }

    void FixedUpdate()
    {
        rigidBody.MovePosition(rigidBody.position + directionMovement * speed * Time.deltaTime);
    }
}
