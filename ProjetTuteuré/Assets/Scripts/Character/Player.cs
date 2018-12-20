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
    public bool typeArmeEquipee = false; // true -> arme de cac, false -> arme a distance

    //attributs concernant l'inventaire
    public GameObject armeDistanceEquipee;
    public GameObject armeCorpsACorpsEquipee;


    public InputField iu;

    public GameObject gameOverText;
    private bool gameOver = false;


    void Awake()
    {
        DatasNames datasnames = (DatasNames)DataManager.LoadNames("names.sav");
        if (datasnames != null)
        {
            if (datasnames.n == 1)
            {
                Datas datas = (Datas)DataManager.Load("Slot1.sav");
                if (datas != null)
                {
                    if (datas.i == 1)
                    {
                        loadDatas("Slot1.sav", 1);
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
                        loadDatas("Slot2.sav", 2);
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
                        loadDatas("Slot3.sav", 3);
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
        if (Time.timeScale == 1)
        {
            GetInput();
        }

        if (gameOver)
        {
            SceneManager.LoadScene("MainMenu");
        }
        base.Update();
    }

    private void FixedUpdate()
    {
        if (!isAlive)
        {
            return;
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
                GameObject UIEquip = GameObject.FindGameObjectWithTag("ArmeUI");

                //On met en "évidence" le type d'arme équipée
                if (typeArmeEquipee)
                {
                    UIEquip.GetComponent<RectTransform>().GetChild(1).GetComponent<RectTransform>().GetChild(0).gameObject.GetComponent<Image>().enabled = false;
                    UIEquip.GetComponent<RectTransform>().GetChild(1).GetComponent<RectTransform>().GetChild(1).gameObject.GetComponent<Image>().enabled = true;
                }
                else
                {
                    UIEquip.GetComponent<RectTransform>().GetChild(1).GetComponent<RectTransform>().GetChild(0).gameObject.GetComponent<Image>().enabled = true;
                    UIEquip.GetComponent<RectTransform>().GetChild(1).GetComponent<RectTransform>().GetChild(1).gameObject.GetComponent<Image>().enabled = false;
                }
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

    public override void Die()
    {
        base.Die();
        StartCoroutine("ShowGameOver");
    }

    private IEnumerator ShowGameOver()
    {
        for (int i = 0; i < 4; i++)
        {
            gameOverText.SetActive(true);
            yield return new WaitForSeconds(0.3f);
            gameOverText.SetActive(false);
            yield return new WaitForSeconds(0.3f);
        }
        gameOver = true;
    }


    public void saveDatasf1(string guess)
    {
        saveDatas(guess, "Slot1.sav", 1);
    }

    public void saveDatasf2(string guess)
    {
        saveDatas(guess, "Slot2.sav", 2);
    }

    public void saveDatasf3(string guess)
    {
        saveDatas(guess, "Slot3.sav", 3);
    }

    public void loadDatasf1()
    {
        loadDatas("Slot1.sav", 1);
    }

    public void loadDatasf2()
    {
        loadDatas("Slot2.sav", 2);
    }

    public void loadDatasf3()
    {
        loadDatas("Slot3.sav", 3);
    }


    public void saveDatas(string guess, string filename, int numeroPartie)
    {
        if (guess.Length != 0)
        {
            DatasNames datasnames = (DatasNames)DataManager.LoadNames("names.sav");
            Datas datas = new Datas();
            if (numeroPartie == 1)
            {
                datasnames.name = guess;
                datas.name = datasnames.name;
            }
            else if (numeroPartie == 2)
            {
                datasnames.name2 = guess;
                datas.name = datasnames.name2;
            }
            else
            {
                datasnames.name3 = guess;
                datas.name = datasnames.name3;
            }
            datas.nameScene = SceneManager.GetActiveScene().name;
            datas.x = transform.position.x;
            datas.y = transform.position.y;
            datas.strength = strength;
            datas.agility = agility;
            datas.endurance = endurance;
            datas.switchCooldown = switchCooldown;
            datas.typeArmeEquipee = typeArmeEquipee;
            datas.currentHealth = currentHealth;
            DataManager.Save(datas, filename);
            DataManager.Save(datasnames, "names.sav");
        }
    }


    public void loadDatas(string filename, int numeroPartie)
    {
        Datas datas = (Datas)DataManager.Load(filename);
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
        }
    }
}
