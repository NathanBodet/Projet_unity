using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterDetector : MonoBehaviour {

    public Porte porte;

    private void OnTriggerEnter2D(Collider2D collision)//lorsque le joueur a dépassé la porte
    {
        if (collision.gameObject.tag == "Player")
        {
            porte.salle.debut(collision.gameObject);
            Destroy(gameObject);
        }
    }

    public void suicide()
    {
        Destroy(gameObject);
    }
}
