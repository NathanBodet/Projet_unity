using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item : MonoBehaviour {

    public string nom;
    public GameObject inventaire;
    public GameObject player;

    private void Start()
    {
        player = GameObject.Find("Player");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player = collision.gameObject;
            player.GetComponent<Player>().objetsProximite.Add(this.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(player != null)
        {
            player.GetComponent<Player>().objetsProximite.Remove(this.gameObject);
            player = null;
        }
    }

    public void take()
    {
        if (!player.GetComponent<InventaireScript>().isFull())
        {
            player.GetComponent<InventaireScript>().addItem(this.gameObject);
            Debug.Log("vous avez récupéré : " + this.gameObject.GetComponent<Item>().nom);
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    public void drop()
    {
        GameObject.Find("Player").GetComponent<InventaireScript>().retirerItem(this.gameObject);
        if(gameObject.scene.name == null)
        {
            Instantiate(gameObject, GameObject.Find("Player").transform.position, Quaternion.identity);
        }
        else
        {
            this.gameObject.transform.position = GameObject.Find("Player").gameObject.transform.position;
            this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            this.gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }
        
    }
}
