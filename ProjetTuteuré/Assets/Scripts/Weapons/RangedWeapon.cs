using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class RangedWeapon : Weapon {


    public int ammunition;
    public int totalammunition;
    public float range;
    public float projectileSpeed;
    public int nbBalles;//nombres de projectiles tirés en même temps

    public float fireRate;
    private float nextFire = 0f;

    public GameObject projectilePrefab;

    void Start()
    {
        this.ammunition = this.totalammunition;
    }

    public void Fire()
    {
        if(ammunition == 0)
        {
            Debug.Log("Plus de munitions !");
        }
        else
        {
            ammunition--;
            Vector2 projectilePosition = player.GetComponent<Transform>().position;
            Vector3 originDirection = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));//10.0f car si z = 0f, la fonction retourne la position de la caméra
            GameObject projectileInstance;
            Vector3 projectileDirection;

            for (int i = 0; i < nbBalles; i++)
            {
                //récupération des coordonnées de la souris et création du vecteur du projectile tiré
                projectileDirection.x = originDirection.x - player.GetComponent<Transform>().position.x;
                projectileDirection.y = originDirection.y - player.GetComponent<Transform>().position.y;
                projectileDirection.z = 0;
                projectileDirection.Normalize();

                //instanciation du projectile et addition du vecteur vitesse
                projectileInstance = Instantiate(projectilePrefab, projectilePosition, Quaternion.identity);
                projectileInstance.GetComponent<Projectile>().isFriendly = true;
                //projectileInstance.transform.rotation = Quaternion.FromToRotation(player.transform.position, projectileDirection);
                float angle = (float)Math.Atan2(projectileDirection.y,projectileDirection.x);
                angle = (float)(angle * (180 / Math.PI));
                //Vector3 rotat = new Vector3(0, 0, angle);
                projectileInstance.GetComponent<Transform>().Rotate(0, 0, angle);
                projectileInstance.GetComponent<Rigidbody2D>().velocity = new Vector2(projectileDirection.x * projectileSpeed, projectileDirection.y * projectileSpeed);
                float rnd = UnityEngine.Random.Range(0, 100);

                if (rnd > player.GetComponent<Player>().agility)//coup normal
                {
                    projectileInstance.GetComponent<Projectile>().damage = 
                        (player.GetComponent<Player>().strength * this.strratio + //ajout des degats en fonction du ratio de degats force du joueur
                        player.GetComponent<Player>().agility * this.agiratio + //ajout des degats en fonction du ratio de degats agilite du joueur
                        player.GetComponent<Player>().endurance * this.endratio +  //ajout des degats en fonction du ratio de degats endurance du joueur
                        this.damage
                        );
                    projectileInstance.GetComponent<Projectile>().isCrit = false;
                }
                else//coup critique
                {
                    projectileInstance.GetComponent<Projectile>().damage = 
                        (player.GetComponent<Player>().strength * this.strratio + //ajout des degats en fonction du ratio de degats force du joueur
                        player.GetComponent<Player>().agility * this.agiratio + //ajout des degats en fonction du ratio de degats agilite du joueur
                        player.GetComponent<Player>().endurance * this.endratio + //ajout des degats en fonction du ratio de degats endurance du joueur
                        this.damage
                        ) * 2;
                    projectileInstance.GetComponent<Projectile>().isCrit = true;
                }
                projectileInstance.GetComponent<Projectile>().range = this.range;

                //update de nb de munition
                try
                {
                    GameObject UIEquip = GameObject.FindGameObjectWithTag("ArmeUI");
                    UIEquip.GetComponent<RectTransform>().GetChild(2).GetComponent<RectTransform>().GetChild(0).gameObject.GetComponent<Text>().text =
                    GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().armeDistanceEquipee.GetComponent<RangedWeapon>().ammunition + "/" +
                    GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().armeDistanceEquipee.GetComponent<RangedWeapon>().totalammunition;
                } catch (NullReferenceException e)
                {
                    Debug.Log(e.Message);
                }
                

            }

        }


    }

}
