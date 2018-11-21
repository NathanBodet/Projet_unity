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
                hit.collider.GetComponent<Enemy>().TakeDamage(player.GetComponent<Player>().strength + this.damage, directionCoup);
            }
            else // coup critique
            {
                hit.collider.GetComponent<Enemy>().TakeDamage((player.GetComponent<Player>().strength + this.damage) * 2, directionCoup);
            }
        }
    }

    public void equip(GameObject player)
    {
        base.equip(player);
        this.player.gameObject.GetComponent<Player>().armeCorpsACorpsScript = this;
    }

}
