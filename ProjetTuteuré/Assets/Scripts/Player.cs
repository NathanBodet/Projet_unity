using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : Actor {

    Vector2 currentDir;

    //Statistiques du joueur
    private int strength = 5;
    private int endurance = 5;
    private int agility = 5;

    //attributs concernant les tirs
    public GameObject ball;
    Vector2 ballPos;
    public float fireRate = 0.5f;
    float nextFire = 0.0f;

    // attributs concernant le combat
    float switchCooldown = 0.0f;
    bool typeArmeEquipee = false; // true -> arme de cac, false -> arme a distance

    protected override void Start () {
        base.Start();
	}

    void Update() {

        currentDir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        currentDir.Normalize();

        if(currentDir == Vector2.zero)
        {
            Stop();
        } else
        {
            Walk();
        }

        /*if (Input.GetKey("e") && Time.time > switchCooldown) { //switch le type d'arme : cooldown de 1s
            switchCooldown = Time.time + 1f;
            Debug.Log("switch");
            typeArmeEquipee = !typeArmeEquipee;
        }*/

        if (Input.GetMouseButtonDown(0))
        {
            Attack();
            /*if(!typeArmeEquipee && Time.time > nextFire)
            {
                nextFire = Time.time + fireRate-0.01f*agility; //firerate -> cooldown de tir
                Fire();
            }*/
        }
    }

    void FixedUpdate()
    {
        Vector3 moveVector = currentDir * speed;
        //rigidBody.MovePosition(transform.position + moveVector * Time.deltaTime);
        rigidBody.velocity = moveVector;
    }

    public void Stop()
    {
        speed = 0f;
        animator.SetBool("IsMoving", false);
    }

    public void Walk()
    {
        speed = 2f;
        animator.SetFloat("DirectionX", currentDir.x);
        animator.SetFloat("DirectionY", currentDir.y);
        animator.SetBool("IsMoving", true);
    }
}
