using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Porte : MonoBehaviour {

    public SalleManager salle;
    public GameObject objectSalle;

	// Use this for initialization
	void Start () {
        salle = objectSalle.GetComponent<SalleManager>();
        GetComponent<SpriteRenderer>().enabled = false;

    }

    public void close()//lorsque le joueur est entré dans la pièce
    {
        GetComponent<BoxCollider2D>().isTrigger = false;
        GetComponent<SpriteRenderer>().enabled = true;
    }

    public void open()
    {
        Debug.Log("Opened");
        Destroy(gameObject);
    }
}
