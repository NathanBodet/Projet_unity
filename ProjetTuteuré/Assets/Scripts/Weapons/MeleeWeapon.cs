using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon {

    public int range;
    public float hitRate;


	public void Hit()
    {
        Vector2 directionCoup = new Vector2(player.GetComponent<Animator>().GetFloat("DirectionX"), player.GetComponent<Animator>().GetFloat("DirectionY"));
        

        LayerMask mask = LayerMask.GetMask("Enemy");
        RaycastHit2D hit = Physics2D.Raycast(player.transform.position, directionCoup,range,mask);
        if (hit.collider != null && hit.collider.gameObject.tag == "Enemy")
        {
            float rnd = Random.Range(0, 100);
            if (rnd > player.gameObject.GetComponent<Player>().agility) //coup normal
            {
                hit.collider.GetComponent<Enemy>().TakeDamage(
                    player.GetComponent<Player>().strength * this.strratio + //ajout des degats en fonction du ratio de degats force du joueur
                    player.GetComponent<Player>().agility * this.agiratio + //ajout des degats en fonction du ratio de degats agilite du joueur
                    player.GetComponent<Player>().endurance * this.endratio + //ajout des degats en fonction du ratio de degats endurance du joueur
                    this.damage,
                    directionCoup,
                    50,
                    false);
            }
            else // coup critique
            {
                hit.collider.GetComponent<Enemy>().TakeDamage(
                    (player.GetComponent<Player>().strength * this.strratio +
                    player.GetComponent<Player>().agility * this.agiratio +
                    player.GetComponent<Player>().endurance * this.endratio +
                    this.damage) * 2, //idem mais en *2
                    directionCoup,
                    50,
                    true);
            }
        }
    }

}
