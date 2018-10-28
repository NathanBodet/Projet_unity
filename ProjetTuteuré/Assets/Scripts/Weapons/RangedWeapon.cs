using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : Weapon {


    public int ammunition;
    public float range;
    public float projectileSpeed;

    public float fireRate = 0.5f;
    private float nextFire = 0f;

    public GameObject projectilePrefab;



    public void Fire()
    {
        Vector2 projectilePosition = player.GetComponent<Transform>().position;
        projectilePosition += new Vector2(0.5f * player.GetComponent<Animator>().GetFloat("DirectionX"), 0.5f * player.GetComponent<Animator>().GetFloat("DirectionY"));

        //récupération des coordonnées de la souris et création du vecteur du projectile tiré
        Vector3 projectileDirection = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));//10.0f car si z = 0f, la fonction retourne la position de la caméra
        projectileDirection.x = projectileDirection.x - player.GetComponent<Transform>().position.x - 0.6f;
        projectileDirection.y = projectileDirection.y - player.GetComponent<Transform>().position.y;
        projectileDirection.Normalize();

        //instanciation du projectile et addition du vecteur vitesse
        GameObject projectileInstance = Instantiate(projectilePrefab, projectilePosition, Quaternion.identity);
        projectileInstance.GetComponent<Rigidbody2D>().velocity = new Vector2(projectileDirection.x * projectileSpeed, projectileDirection.y * projectileSpeed);
        projectileInstance.GetComponent<Projectile>().damage = this.damage;
        projectileInstance.GetComponent<Projectile>().range = this.range;


    }

}
