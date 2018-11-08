using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class InventaireScript : MonoBehaviour {

    public GameObject[] listeItems;
    public GameObject player;
    GameObject c;

	// Use this for initialization
	void Start () {
        listeItems = new GameObject[10];
        c = player.GetComponent<MenusJeu>().canvas2;
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
                updateMenuInventaire();
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
        updateMenuInventaire();
    }

    //pour retirer un objet à l'indice indice
    public void retirerItem(int indice)
    {
        if(player.GetComponent<Player>().armeCorpsACorpsEquipee != listeItems[indice] && player.GetComponent<Player>().armeDistanceEquipee != listeItems[indice])
        {
            listeItems[indice] = null;
        }
        updateMenuInventaire();
    }
	
    public void updateMenuInventaire()
    {
        
        for (int i=0; i<10; i++)
        {
            GameObject button = c.GetComponent<RectTransform>().GetChild(0).gameObject.GetComponent<RectTransform>().GetChild(i).gameObject;

            if (listeItems[i] != null)
            {
                button.GetComponent<Image>().sprite = listeItems[i].GetComponent<SpriteRenderer>().sprite;
            }
            
        }
    }
}
