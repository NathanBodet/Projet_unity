using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Character {

    //Statistiques du joueur
    private int strength = 5;
    private int endurance = 5;
    private int agility = 5;

    //attributs concernant les tirs
    float nextFire = 0.0f;

    // attributs concernant le combat
    float switchCooldown = 0.0f;
    bool typeArmeEquipee = false; // true -> arme de cac, false -> arme a distance

    //attributs concernant l'interface
    public LifeBar lifeBar;

    //attributs concernant l'inventaire
    public GameObject armeDistanceEquipee;
    public GameObject armeCorpsACorpsEquipee;


    void Awake()
    {
        if(File.Exists(Application.persistentDataPath + "/Save1.sav"))
        {
            Datas datas = (Datas)DataManager.Load("Save1.sav");
            if (datas.i == 1)
            {
                loadDatas();
                datas.i = 0;
                DataManager.Save(datas, "Save1.sav");
            }
        }
    }



    protected override void Start()
    {
        base.Start();
        armeDistanceEquipee.GetComponent<RangedWeapon>().equip(this.gameObject);
        armeCorpsACorpsEquipee.GetComponent<MeleeWeapon>().equip(this.gameObject);
        /*lifeBar = GameObject.FindGameObjectWithTag("PlayerLifeBar").GetComponent<LifeBar>();
        lifeBar.SetProgress(currentHealth / maxHealth);*/
    }

    protected override void Update() {

        /*if (Time.timeScale == 1)
        {
            GetInput();
        }*/
        GetInput();
        base.Update();
    }

    private void FixedUpdate()
    {
        if (!isAlive)
        {
            
        }
        Move();
    }

    private void GetInput()
    {
        //inputs directionels
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

        //input de tir
        if (Input.GetMouseButtonDown(0))
        {

            if (typeArmeEquipee)
            {
                nextFire = Time.time + armeCorpsACorpsEquipee.GetComponent<MeleeWeapon>().hitRate - 0.01f * agility;
                armeCorpsACorpsEquipee.GetComponent<MeleeWeapon>().Hit();
            } else
            {
                if(Time.time > nextFire)
                {
                    nextFire = Time.time + armeDistanceEquipee.GetComponent<RangedWeapon>().fireRate - 0.01f * agility; //firerate -> cooldown de tir
                    armeDistanceEquipee.GetComponent<RangedWeapon>().Fire();
                }
            }
        }

        //input de switch d'arme
        if (Input.GetKey(KeyCode.E))
        {
            //switch le type d'arme : cooldown de 1s
            if (Time.time > switchCooldown)
            {
                switchCooldown = Time.time + 1f;
                typeArmeEquipee = !typeArmeEquipee;
            }
        }

    
    }

    public void Move()
    {
        rigidBody.velocity = direction.normalized * speed;
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        lifeBar.EnableLifeBar(true);
        lifeBar.SetProgress(currentHealth / maxHealth);
    }

    public void saveDatas()
    {
        Debug.Log("On va sauver les datas dans " + Application.persistentDataPath);
        Datas datas = new Datas();
        datas.nameScene = SceneManager.GetActiveScene().name;
        datas.x = transform.position.x;
        datas.y = transform.position.y;
        datas.strength = strength;
        datas.agility = agility;
        datas.endurance = endurance;
        datas.switchCooldown = switchCooldown;
        datas.typeArmeEquipee = typeArmeEquipee;
        datas.currentHealth = currentHealth;
        DataManager.Save(datas, "Save1.sav");
    }

    public void loadDatas()
    {
        if (File.Exists(Application.persistentDataPath + "/Save1.sav"))
        {
            Debug.Log("On va charger les datas");
            Datas datas = (Datas)DataManager.Load("Save1.sav");
            Vector2 position = new Vector2(datas.x, datas.y);
            this.transform.position = position;
            strength = datas.strength;
            agility = datas.agility;
            endurance = datas.endurance;
            switchCooldown = datas.switchCooldown;
            typeArmeEquipee = datas.typeArmeEquipee;
            currentHealth = datas.currentHealth;
        }
    }
}
