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
            Debug.Log(datasnames.n);
            if (datasnames.n == 1)
            {
                Datas datas = (Datas)DataManager.Load("Slot1.sav");
                if (datas != null)
                {
                    if (datas.i == 1)
                    {
                        loadDatas();
                        datas.i = 0;
                        DataManager.Save(datas, "Slot1.sav");
                        datasnames.n = 0;
                        DataManager.Save(datasnames, "names.sav");
                    }
                }
            }
            if (datasnames.n == 2)
            {
                Datas datas = (Datas)DataManager.Load("Slot2.sav");
                if (datas != null)
                {
                    if (datas.i == 1)
                    {
                        loadDatas2();
                        datas.i = 0;
                        DataManager.Save(datas, "Slot2.sav");
                        datasnames.n = 0;
                        DataManager.Save(datasnames, "names.sav");
                    }
                }
            }
            if (datasnames.n == 3)
            {
                Datas datas = (Datas)DataManager.Load("Slot3.sav");
                if (datas != null)
                {
                    if (datas.i == 1)
                    {
                        loadDatas3();
                        datas.i = 0;
                        DataManager.Save(datas, "Slot3.sav");
                        datasnames.n = 0;
                        DataManager.Save(datasnames, "names.sav");
                    }
                }
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

        //input d'attaque
        if (Input.GetMouseButtonDown(0))
        {
            if(Time.time > nextFire)
            {
                if (typeArmeEquipee)
                {
                    animator.SetTrigger("Attacking");
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

    
    }

    public void Move()
    {
        rigidBody.velocity = direction.normalized * speed;
    }

    public override void TakeDamage(float damage, Vector3 hitVector,float force)
    {
        base.TakeDamage(damage, hitVector,force);
        lifeBar.EnableLifeBar(true);
        lifeBar.SetProgress(currentHealth / maxHealth);
    }


    public void saveDatas(string guess)
    {
        if (guess.Length != 0)
        {
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
            datasnames.name = guess;
            DataManager.Save(datas, "Slot1.sav");
            DataManager.Save(datasnames, "names.sav");
        }
    }

    public void saveDatas2(string guess) {
        if (guess.Length != 0)
        {
            DatasNames datasnames = (DatasNames)DataManager.LoadNames("names.sav");
            Datas datas = new Datas();
            datas.name = datasnames.name2;
            datas.nameScene = SceneManager.GetActiveScene().name;
            datas.x = transform.position.x;
            datas.y = transform.position.y;
            datas.strength = strength;
            datas.agility = agility;
            datas.endurance = endurance;
            datas.switchCooldown = switchCooldown;
            datas.typeArmeEquipee = typeArmeEquipee;
            datas.currentHealth = currentHealth;
            datasnames.name2 = guess;
            DataManager.Save(datas, "Slot2.sav");
            DataManager.Save(datasnames, "names.sav");
        }
    }

    public void saveDatas3(string guess)
    {
        if (guess.Length != 0)
        {
            DatasNames datasnames = (DatasNames)DataManager.LoadNames("names.sav");
            Datas datas = new Datas();
            datas.name = datasnames.name3;
            datas.nameScene = SceneManager.GetActiveScene().name;
            datas.x = transform.position.x;
            datas.y = transform.position.y;
            datas.strength = strength;
            datas.agility = agility;
            datas.endurance = endurance;
            datas.switchCooldown = switchCooldown;
            datas.typeArmeEquipee = typeArmeEquipee;
            datas.currentHealth = currentHealth;
            datasnames.name3 = guess;
            DataManager.Save(datas, "Slot3.sav");
            DataManager.Save(datasnames, "names.sav");
        }
    }

    public void loadDatas()
    {
        if (File.Exists(Application.persistentDataPath + "/names.sav"))
        {
            Debug.Log("On va charger les datas");
            DatasNames dtn = (DatasNames)DataManager.LoadNames("names.sav");
                Datas datas = (Datas)DataManager.Load("Slot1.sav");
                if (datas != null)
                {
                    Vector2 position = new Vector2(datas.x, datas.y);
                    this.transform.position = position;
                    strength = datas.strength;
                    agility = datas.agility;
                    endurance = datas.endurance;
                    switchCooldown = datas.switchCooldown;
                    typeArmeEquipee = datas.typeArmeEquipee;
                    currentHealth = datas.currentHealth;
                    DataManager.Save(dtn, "names.sav");
                }
            }
           
    }

    public void loadDatas2()
    {
        if (File.Exists(Application.persistentDataPath + "/names.sav"))
        {
            Debug.Log("On va charger les datas");
            DatasNames dtn = (DatasNames)DataManager.LoadNames("names.sav");
            Datas datas = (Datas)DataManager.Load("Slot2.sav");
            if (datas != null)
            {
                Vector2 position = new Vector2(datas.x, datas.y);
                this.transform.position = position;
                strength = datas.strength;
                agility = datas.agility;
                endurance = datas.endurance;
                switchCooldown = datas.switchCooldown;
                typeArmeEquipee = datas.typeArmeEquipee;
                currentHealth = datas.currentHealth;
                DataManager.Save(dtn, "names.sav");
            }
        }

    }

    public void loadDatas3()
    {
        if (File.Exists(Application.persistentDataPath + "/names.sav"))
        {
            Debug.Log("On va charger les datas");
            DatasNames dtn = (DatasNames)DataManager.LoadNames("names.sav");
            Datas datas = (Datas)DataManager.Load("Slot3.sav");
            if (datas != null)
            {
                Vector2 position = new Vector2(datas.x, datas.y);
                this.transform.position = position;
                strength = datas.strength;
                agility = datas.agility;
                endurance = datas.endurance;
                switchCooldown = datas.switchCooldown;
                typeArmeEquipee = datas.typeArmeEquipee;
                currentHealth = datas.currentHealth;
                DataManager.Save(dtn, "names.sav");
            }
        }

    }

}
