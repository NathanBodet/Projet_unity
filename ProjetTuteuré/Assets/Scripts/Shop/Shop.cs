using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{

    public static Shop instance;

    public GameObject shopMenu, buyMenu, sellMenu;

    public Text goldText;

    public List<Items> itemsForSale;

    public ItemButton[] buyItemButtons;
    public ItemButton[] sellItemButtons;

    public Items selectedItem;
    public Text buyItemName, buyItemDescription, buyItemPrice;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        shopMenu.SetActive(false);
        buyMenu.SetActive(false);
        sellMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OpenShop()
    {
        shopMenu.SetActive(true);
        OpenBuyMenu();

        GameManager.instance.shopActive = true;

        goldText.text = Player.instance.gold.ToString() + "g";
    }

    public void CloseShop()
    {
        shopMenu.SetActive(false);
        buyMenu.SetActive(false);
        sellMenu.SetActive(false);
        GameManager.instance.shopActive = false;
    }

    public void OpenBuyMenu()
    {
        buyMenu.SetActive(true);
        sellMenu.SetActive(false);
        
        for(int i = 0; i < itemsForSale.Count; i++)
        {
            buyItemButtons[i].buttonImage.gameObject.SetActive(true);
            buyItemButtons[i].buttonImage.sprite = itemsForSale[i].GetComponent<SpriteRenderer>().sprite;
            buyItemButtons[i].amountText.text = Random.Range(1, 3).ToString();
            buyItemButtons[i].setItemReferenced(itemsForSale[i]);
        }

        for(int i = itemsForSale.Count; i < buyItemButtons.Length; i++)
        {
            buyItemButtons[i].buttonImage.gameObject.SetActive(false);
            buyItemButtons[i].amountText.text = "";
            buyItemButtons[i].gameObject.GetComponent<Button>().interactable = false;
        }
    }

    public void SelectBuyItem(Items buyItemSelected)
    {
        selectedItem = buyItemSelected;
        buyItemName.text = selectedItem.itemName;
        buyItemDescription.text = selectedItem.description;
        buyItemPrice.text = "Value: " + selectedItem.price.ToString() +"g";
    }

    public void OpenSellMenu()
    {
        sellMenu.SetActive(true);
        buyMenu.SetActive(false);
    }
}
