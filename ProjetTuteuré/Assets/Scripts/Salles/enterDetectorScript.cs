using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enterDetectorScript : MonoBehaviour {

    public Porte porte;

    private void OnTriggerEnter2D(Collider2D collision)//lorsque le joueur a dépassé la porte
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Entré");
            porte.salle.debut();
            Destroy(gameObject);
        }
    }
}
