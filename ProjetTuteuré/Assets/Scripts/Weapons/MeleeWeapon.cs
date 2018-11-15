using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon {

    public int range;
    public float hitRate;

	public void Hit()
    {
        Debug.Log("Je tape");
        Vector2 directionCoup = new Vector2(player.GetComponent<Player>().getDirection().x, player.GetComponent<Player>().getDirection().y);
        Debug.Log(player.GetComponent<Player>().getDirection().x);
        Debug.Log(player.GetComponent<Player>().getDirection().y);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, directionCoup,2f);
        Debug.Log(hit.collider.gameObject.tag);
        if (hit.collider != null && hit.collider.gameObject.tag == "Enemy")
        {
            Debug.Log("Je touche");
            hit.collider.GetComponent<Enemy>().TakeDamage(player.GetComponent<Player>().strength + this.damage);
        }
    }

}
