using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item : MonoBehaviour {

    public string nom;
    public GameObject inventaire;
    public GameObject player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (!collision.gameObject.GetComponent<InventaireScript>().isFull())
            {
                player = collision.gameObject;
                collision.gameObject.GetComponent<InventaireScript>().addItem(this.gameObject);
                Debug.Log("vous avez récupéré : " + this.gameObject.GetComponent<Item>().nom);
                this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                this.gameObject.GetComponent<BoxCollider2D>().enabled = false;

            }
        }
    }
}
