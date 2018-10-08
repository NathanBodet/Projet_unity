using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class Player : Character {

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

    public LifeBar lifeBar;



    protected override void Start()
    {
        base.Start();
        lifeBar = GameObject.FindGameObjectWithTag("HeroLifeBar").GetComponent<LifeBar>();
        lifeBar.SetProgress(currentHealth / maxHealth);
    }

    protected override void Update() {

        GetInput();
        base.Update();
    }

    private void GetInput()
    {
        direction = Vector2.zero;
        if (Input.GetKey(KeyCode.Z))
        {
            direction += Vector2.up;
        }

        if (Input.GetKey(KeyCode.Q))
        {
            direction += Vector2.left;
        }

        if (Input.GetKey(KeyCode.S))
        {
            direction += Vector2.down;
        }

        if (Input.GetKey(KeyCode.D))
        {
            direction += Vector2.right;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (Time.timeScale == 1)
            {
                Attack();
                if (!typeArmeEquipee && Time.time > nextFire)
                {
                    nextFire = Time.time + fireRate - 0.01f * agility; //firerate -> cooldown de tir
                    Fire();
                }
            }
        }

        if (Input.GetKey(KeyCode.E))
        {
            //switch le type d'arme : cooldown de 1s
            if (Time.time > switchCooldown)
            {
                switchCooldown = Time.time + 1f;
                Debug.Log("switch");
                typeArmeEquipee = !typeArmeEquipee;
            }
        }
    
    }

    private void Attack()
    {
        animator.SetTrigger("Attacking");
    }

    public void Fire()
    {
        ballPos = transform.position;
        ballPos += new Vector2(0.5f * animator.GetFloat("DirectionX"), 0.5f * animator.GetFloat("DirectionY"));

        //récupération des coordonnées de la souris et création du vecteur du projectile tiré
        Vector3 ballDir = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));//10.0f car si z = 0f, la fonction retourne la position de la caméra
        ballDir.x = ballDir.x - transform.position.x - 0.6f;
        ballDir.y = ballDir.y - transform.position.y;
        ballDir.Normalize();

        //instanciation du projectile et addition du vecteur vitesse
        GameObject ballInstance = Instantiate(ball, ballPos, Quaternion.identity);
        ballInstance.GetComponent<Rigidbody2D>().velocity = new Vector2(ballDir.x * 5, ballDir.y * 5);
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        lifeBar.EnableLifeBar(true);
        lifeBar.SetProgress(currentHealth / maxHealth);
    }

    public void saveDatas()
    {
        Debug.Log("On va sauver les datas dans "+Application.persistentDataPath);
        Datas datas = new Datas();
        datas.x = transform.position.x;
        datas.y = transform.position.y;
        DataManager.Save(datas, "Save1.sav");
    }

    public void loadDatas()
    {
        if (File.Exists(Application.persistentDataPath+"/Save1.sav"))
        {
            Debug.Log("On va charger les datas");
            Datas datas = (Datas)DataManager.Load("Save1.sav");
            Vector2 position = new Vector2(datas.x, datas.y);
            transform.position = position;
        }
    }
}
