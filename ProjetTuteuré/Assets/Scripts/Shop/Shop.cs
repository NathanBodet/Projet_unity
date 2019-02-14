using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{

    public static Shop instance;

    public GameObject shopMenu, buyMenu, sellMenu;

    public Text goldText;

    public List<object[]> itemsForSale;

    public ItemButton[] buyItemButtons;
    public ItemButton[] sellItemButtons;

    public GameObject selectedItem;
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
            GameObject item = (GameObject)itemsForSale[i][0];
            int amount = (int)itemsForSale[i][1];

            buyItemButtons[i].buttonImage.gameObject.SetActive(true);
            buyItemButtons[i].buttonImage.sprite = item.GetComponent<SpriteRenderer>().sprite;
            buyItemButtons[i].amountText.text = amount.ToString();
            buyItemButtons[i].setItemReferenced(item);
        }

        for(int i = itemsForSale.Count; i < buyItemButtons.Length; i++)
        {
            buyItemButtons[i].buttonImage.gameObject.SetActive(false);
            buyItemButtons[i].amountText.text = "";
            buyItemButtons[i].gameObject.GetComponent<Button>().interactable = false;
        }
    }

    public void SelectBuyItem(GameObject buyItemSelected)
    {
        selectedItem = buyItemSelected;
        buyItemName.text = selectedItem.GetComponent<Items>().itemName;
        buyItemDescription.text = selectedItem.GetComponent<Items>().description;
        buyItemPrice.text = "Value: " + selectedItem.GetComponent<Items>().price.ToString() +"g";
    }

    public void BuyItem()
    {
        if(selectedItem != null)
        {
            if(Player.instance.gold >= selectedItem.GetComponent<Items>().price)
            {
                Player.instance.gold -= selectedItem.GetComponent<Items>().price;
                InventaireScript.instance.addItem(selectedItem);
            }
        }

        goldText.text = Player.instance.gold.ToString() + "g";
    }

    public void OpenSellMenu()
    {
        sellMenu.SetActive(true);
        buyMenu.SetActive(false);
    }
}
