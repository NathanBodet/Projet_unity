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

    public float fireRate = 0.5f;
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
            float dispertion;
            Vector3 originDirection = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));//10.0f car si z = 0f, la fonction retourne la position de la caméra
            GameObject projectileInstance;
            Vector3 projectileDirection;

            for (int i = 0; i < nbBalles; i++)
            {
                dispertion = UnityEngine.Random.Range(0, 1);
                //récupération des coordonnées de la souris et création du vecteur du projectile tiré
                projectileDirection.x = originDirection.x - player.GetComponent<Transform>().position.x;
                projectileDirection.y = originDirection.y - player.GetComponent<Transform>().position.y;
                projectileDirection.z = 0;
                projectileDirection.Normalize();

                //instanciation du projectile et addition du vecteur vitesse
                projectileInstance = Instantiate(projectilePrefab, projectilePosition, Quaternion.identity);
                projectileInstance.transform.rotation = Quaternion.FromToRotation(player.transform.position, projectileDirection);
                projectileInstance.GetComponent<Rigidbody2D>().velocity = new Vector2(projectileDirection.x * projectileSpeed, projectileDirection.y * projectileSpeed);
                float rnd = UnityEngine.Random.Range(0, 100);

                if (rnd > player.gameObject.GetComponent<Player>().agility)//coup normal
                {
                    projectileInstance.GetComponent<Projectile>().damage = (this.damage + player.gameObject.GetComponent<Player>().strength);
                    projectileInstance.GetComponent<Projectile>().isCrit = false;
                }
                else//coup critique
                {
                    projectileInstance.GetComponent<Projectile>().damage = (this.damage + player.gameObject.GetComponent<Player>().strength) * 2;
                    projectileInstance.GetComponent<Projectile>().isCrit = true;
                }
                projectileInstance.GetComponent<Projectile>().range = this.range;

                //update de nb de munition
                GameObject UIEquip = GameObject.FindGameObjectWithTag("ArmeUI");
                UIEquip.GetComponent<RectTransform>().GetChild(2).GetComponent<RectTransform>().GetChild(0).gameObject.GetComponent<Text>().text =
                GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().armeDistanceEquipee.GetComponent<RangedWeapon>().ammunition + "/" +
                GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().armeDistanceEquipee.GetComponent<RangedWeapon>().totalammunition;

            }

        }


    }

}
