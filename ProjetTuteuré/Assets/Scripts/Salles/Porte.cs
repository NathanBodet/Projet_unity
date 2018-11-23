using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Porte : MonoBehaviour {

    bool ouverte;
    SalleManager salle;
    public GameObject objectSalle;
    public Sprite[][] listeSprites;

	// Use this for initialization
	void Start () {
        salle = objectSalle.GetComponent<SalleManager>();
        ouverte = true;
        listeSprites = new Sprite[4][];
        for(int i =0; i < 4; i++)
        {
            listeSprites[i] = new Sprite[2];
        }
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            salle.debut();
        }
    }

    public void close()
    {
        GetComponent<BoxCollider2D>().isTrigger = false;
        //GetComponent<SpriteRenderer>().sprite =
    }
}
