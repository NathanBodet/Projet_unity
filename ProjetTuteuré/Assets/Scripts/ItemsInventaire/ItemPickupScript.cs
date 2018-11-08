using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickupScript : MonoBehaviour {

    public GameObject inventaire;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (!collision.gameObject.GetComponent<InventaireScript>().isFull())
            {
                collision.gameObject.GetComponent<InventaireScript>().addItem(this.gameObject);
                Debug.Log("vous avez récupéré : " + this.gameObject.GetComponent<Item>().nom);
                this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                collision.gameObject.GetComponent<Player>().armeDistanceEquipee = this.gameObject;
                this.gameObject.GetComponent<Weapon>().equip(collision.gameObject);
            }
        }
    }
}
