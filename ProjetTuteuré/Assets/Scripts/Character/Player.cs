using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : Character {

    //Statistiques du joueur
    public int strength = 5;
    public int endurance = 5;
    public int agility = 5;

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


    public InputField iu;


    void Awake()
    {
        DatasNames datasnames = (DatasNames)DataManager.LoadNames("names.sav");
        if (datasnames != null)
        {
            Datas datas = (Datas)DataManager.Load(datasnames.name + ".sav");
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
        lifeBar = GameObject.FindGameObjectWithTag("PlayerLifeBar").GetComponent<LifeBar>();
        lifeBar.SetProgress(currentHealth / maxHealth);
    }

    protected override void Update() {

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
        //inputs directionnels
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
            if(Time.time > nextFire)
            {
                if (typeArmeEquipee)
                {
                    nextFire = Time.time + armeCorpsACorpsEquipee.GetComponent<MeleeWeapon>().hitRate - 0.01f * agility;
                    armeCorpsACorpsEquipee.gameObject.GetComponent<MeleeWeapon>().Hit();
                } else
                {
                    nextFire = Time.time + armeDistanceEquipee.GetComponent<RangedWeapon>().fireRate - 0.01f * agility; //firerate -> cooldown de tir
                    armeDistanceEquipee.gameObject.GetComponent<RangedWeapon>().Fire();
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

        //input d'attaque càc
        if (Input.GetMouseButton(0))
        {
            Attack();
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

    public void saveDatas(string guess)
    {
        if (File.Exists(Application.persistentDataPath + "/names.sav"))
        {
            if (guess.Length != 0)
            {
                Debug.Log("On va sauver les datas dans " + Application.persistentDataPath);
                DatasNames datasnames = (DatasNames)DataManager.LoadNames("names.sav");
                Datas datas = new Datas();
                datas.name = datasnames.name;
                datas.nameScene = SceneManager.GetActiveScene().name;
                datas.x = transform.position.x;
                datas.y = transform.position.y;
                datas.strength = strength;
                datas.agility = agility;
                datas.endurance = endurance;
                datas.switchCooldown = switchCooldown;
                datas.typeArmeEquipee = typeArmeEquipee;
                datas.currentHealth = currentHealth;
                if (datasnames.name != guess)
                {
                    File.Delete(Application.persistentDataPath + "/" + datasnames.name + ".sav");
                }
                datasnames.name = guess;
                DataManager.Save(datasnames, "names.sav");
                DataManager.Save(datas, datasnames.name + ".sav");
            }
        }
        else
        {
            if (guess.Length != 0)
            {
                Debug.Log("On va sauver les datas dans " + Application.persistentDataPath);
                DatasNames dtn = new DatasNames();
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
                dtn.name = guess;
                DataManager.Save(dtn, "names.sav");
                DataManager.Save(datas, guess + ".sav");
                Debug.Log("On va sauver les datas dans " + Application.persistentDataPath);
                Debug.Log(guess);
            }
        }


    }

    public void loadDatas()
    {
        if (File.Exists(Application.persistentDataPath + "/names.sav"))
        {
            Debug.Log("On va charger les datas");
            DatasNames dtn = (DatasNames)DataManager.LoadNames("names.sav");
            Datas datas = (Datas)DataManager.Load(dtn.name + ".sav");
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
