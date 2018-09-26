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

        if (Input.GetKey("e") && Time.time > switchCooldown) { //switch le type d'arme : cooldown de 1s
            switchCooldown = Time.time + 1f;
            Debug.Log("switch");
            typeArmeEquipee = !typeArmeEquipee;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (Time.timeScale==1)
            {
                Attack();
                if (!typeArmeEquipee && Time.time > nextFire)
                {
                    nextFire = Time.time + fireRate - 0.01f * agility; //firerate -> cooldown de tir
                    Fire();
                }
            }
        }
    }

    public void Fire()
    {
        ballPos = transform.position;
        ballPos += new Vector2(0.5f*animator.GetFloat("DirectionX"), 0.5f*animator.GetFloat("DirectionY"));

        //récupération des coordonnées de la souris et création du vecteur du projectile tiré
        Vector3 ballDir = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));//10.0f car si z = 0f, la fonction retourne la position de la caméra
        ballDir.x = ballDir.x - transform.position.x - 0.6f;
        ballDir.y = ballDir.y - transform.position.y;
        ballDir.Normalize();

        //instanciation du projectile et addition du vecteur vitesse
        GameObject ballInstance = Instantiate(ball, ballPos, Quaternion.identity);
        ballInstance.GetComponent<Rigidbody2D>().velocity = new Vector2(ballDir.x * 5, ballDir.y * 5);
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
