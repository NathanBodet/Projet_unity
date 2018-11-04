using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventaireScript : MonoBehaviour {

    GameObject[] listeItems;
    public GameObject player;

	// Use this for initialization
	void Start () {
        listeItems = new GameObject[10];
	}

    public bool isFull()
    {
        for (int i = 0; i < 10; i++)
        {
            if (listeItems[i] == null)
            {
                return false;
            }
        }
        return true;
    }

    public void addItem(GameObject item)
    {
        for(int i = 0; i<10; i++)
        {
            if(listeItems[i] == null)
            {
                listeItems[i] = item;
                return;
            }
        }
    }

    //pour retirer un objet spécifique
    public void retirerItem(GameObject item)
    {
        for (int i = 0; i < 10; i++)
        {
            if (listeItems[i] == item)
            {
                if (player.GetComponent<Player>().armeCorpsACorpsEquipee != listeItems[i] && player.GetComponent<Player>().armeDistanceEquipee != listeItems[i])
                {
                    listeItems[i] = null;
                }

            }
        }
    }

    //pour retirer un objet à l'indice indice
    public void retirerItem(int indice)
    {
        if(player.GetComponent<Player>().armeCorpsACorpsEquipee != listeItems[indice] && player.GetComponent<Player>().armeDistanceEquipee != listeItems[indice])
        {
            listeItems[indice] = null;
        }
    }
	
}
