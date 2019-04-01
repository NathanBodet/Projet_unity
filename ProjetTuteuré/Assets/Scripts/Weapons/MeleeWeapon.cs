
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MeleeWeapon : Weapon {

    public int range;
    public float hitRate;


	public void Hit()
    {
        
        Vector3 originDirection = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));//10.0f car si z = 0f, la fonction retourne la position de la caméra
        Vector3 projectileDirection;

        projectileDirection.x = originDirection.x - player.GetComponent<Transform>().position.x;
        projectileDirection.y = originDirection.y - player.GetComponent<Transform>().position.y;
        projectileDirection.z = 0;
        projectileDirection.Normalize();
        float angle = (float)Math.Atan2(projectileDirection.y, projectileDirection.x);
        GameObject slash;
        slash = Instantiate(GameObject.Find("Player").GetComponent<Player>().slashPrefab, GameObject.Find("Player").transform.position, Quaternion.identity);
        slash.GetComponent<Transform>().position = new Vector3(slash.GetComponent<Transform>().position.x + ((float)Math.Cos(angle)),
            slash.GetComponent<Transform>().position.y + ((float)Math.Sin(angle)),
            slash.GetComponent<Transform>().position.z);
        angle = (float)(angle * (180 / Math.PI));
        //Debug.Log(angle);
        
        
        //Vector3 rotat = new Vector3(0, 0, angle);
        slash.GetComponent<Transform>().Rotate(0, 0, angle);

        float rnd = UnityEngine.Random.Range(0, 100);
        if (rnd > player.gameObject.GetComponent<Player>().agility) //coup normal
        {
            slash.GetComponent<Projectile>().damage =
                    player.GetComponent<Player>().strength * this.strratio + //ajout des degats en fonction du ratio de degats force du joueur
                    player.GetComponent<Player>().agility * this.agiratio + //ajout des degats en fonction du ratio de degats agilite du joueur
                    player.GetComponent<Player>().endurance * this.endratio + //ajout des degats en fonction du ratio de degats endurance du joueur
                    this.damage;
        }
        else // coup critique
        {
            slash.GetComponent<Projectile>().damage =
                (player.GetComponent<Player>().strength * this.strratio +
                player.GetComponent<Player>().agility * this.agiratio +
                player.GetComponent<Player>().endurance * this.endratio +
                this.damage) * 2;
        }
        
        slash.GetComponent<Projectile>().isFriendly = true;
    }
    

}
