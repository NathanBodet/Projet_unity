using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : Weapon {


    public int ammunition;
    public float range;
    public float projectileSpeed;

    public float fireRate = 0.5f;
    private float nextFire = 0f;

    public GameObject player;

    

    // Use this for initialization
    void Start () {
        
    }

    public void Fire(GameObject projectilePrefab)
    {
        Vector2 ballPos = player.GetComponent<Transform>().position;
        ballPos += new Vector2(0.5f * player.GetComponent<Animator>().GetFloat("DirectionX"), 0.5f * player.GetComponent<Animator>().GetFloat("DirectionY"));

        //récupération des coordonnées de la souris et création du vecteur du projectile tiré
        Vector3 ballDir = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));//10.0f car si z = 0f, la fonction retourne la position de la caméra
        ballDir.x = ballDir.x - player.GetComponent<Transform>().position.x - 0.6f;
        ballDir.y = ballDir.y - player.GetComponent<Transform>().position.y;
        ballDir.Normalize();

        //instanciation du projectile et addition du vecteur vitesse
        GameObject ballInstance = Instantiate(projectilePrefab, ballPos, Quaternion.identity);
        ballInstance.GetComponent<Rigidbody2D>().velocity = new Vector2(ballDir.x * projectileSpeed, ballDir.y * projectileSpeed);

    }

    public void equip(GameObject player)
    {
        this.player = player;
    }

    // Update is called once per frame
    void Update () {
		
	}

}
