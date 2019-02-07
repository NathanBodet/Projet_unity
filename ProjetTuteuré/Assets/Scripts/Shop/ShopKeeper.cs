using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopKeeper : MonoBehaviour
{

    private bool canOpen;

    public Items[] itemsInStock = new Items[24];
    public List<Items> itemsForSale;

    // Start is called before the first frame update
    void Start()
    {
        itemsForSale = new List<Items>();
        selectItemsToSale();
    }

    // Update is called once per frame
    void Update()
    {
        if(canOpen && !Shop.instance.shopMenu.activeInHierarchy)
        {
            Shop.instance.itemsForSale = itemsForSale;
            Shop.instance.OpenShop();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            canOpen = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            canOpen = false;
        }
    }

    private void selectItemsToSale()
    {
        for(int i = 0; i < itemsInStock.Length; i++)
        {
            if(itemsInStock[i] != null)
            {
                int prob = Random.Range(0, 100);
                if(prob >= 50)
                {
                    itemsForSale.Add(itemsInStock[i]);
                }
            }
        }
    }
}
