
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
        Vector2 directionCoup = new Vector2(player.GetComponent<Animator>().GetFloat("DirectionX"), player.GetComponent<Animator>().GetFloat("DirectionY"));

        Vector2 slashPos = player.GetComponent<Transform>().position;
        Vector3 originDirection = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));//10.0f car si z = 0f, la fonction retourne la position de la caméra
        GameObject slash;
        slash = Instantiate(GameObject.Find("Player").GetComponent<Player>().slashPrefab, GameObject.Find("Player").transform.position,Quaternion.identity );
        
        
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
        float angle = (float)Math.Atan2(originDirection.y, originDirection.x);
        angle = (float)(angle * (180 / Math.PI));
        //Vector3 rotat = new Vector3(0, 0, angle);
        slash.GetComponent<Transform>().Rotate(0, 0, angle);
        slash.GetComponent<Projectile>().isFriendly = true;
    }
    

}
