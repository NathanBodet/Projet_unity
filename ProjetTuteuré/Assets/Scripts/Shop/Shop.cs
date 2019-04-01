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
    public Text sellItemName, sellItemDescription, sellItemPrice;

    public InventaireScript inventaire;

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

        Player.instance.Stop();
        Player.instance.canAttack = false;
    }

    public void CloseShop()
    {
        shopMenu.SetActive(false);
        buyMenu.SetActive(false);
        sellMenu.SetActive(false);
        GameManager.instance.shopActive = false;
        Player.instance.canMove = true;
        Player.instance.canAttack = true;
    }

    public void OpenBuyMenu()
    {
        buyMenu.SetActive(true);
        sellMenu.SetActive(false);
        ShowBuyItems();

    }

    public void ShowBuyItems()
    {
        for (int i = 0; i < itemsForSale.Count; i++)
        {
            GameObject item = (GameObject)itemsForSale[i][0];
            int amount = (int)itemsForSale[i][1];

            buyItemButtons[i].buttonImage.gameObject.SetActive(true);
            buyItemButtons[i].buttonImage.sprite = item.GetComponent<SpriteRenderer>().sprite;
            buyItemButtons[i].amountText.text = amount.ToString();
            buyItemButtons[i].setItemReferenced(item);
        }

        for (int i = itemsForSale.Count; i < buyItemButtons.Length; i++)
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

    public void BuyItemCallBack()
    {
        StartCoroutine(BuyItem());
    }

    public IEnumerator BuyItem()
    {
        if(selectedItem != null)
        {
            if(Player.instance.gold >= selectedItem.GetComponent<Items>().price)
            {
                ConfirmPopUp.instance.OpenPopupConfirm("Are you sure you want to buy this item?");

                //Tant qu'un choix n'a pas été fait sur la popup
                while (ConfirmPopUp.instance.result == -1)
                {
                    for(int i = 0; i < itemsForSale.Count; i++)
                    {
                        buyItemButtons[i].gameObject.GetComponent<Button>().interactable = false;
                    }
                    yield return null;

                    if(ConfirmPopUp.instance.result == 1)
                    {
                        Player.instance.gold -= selectedItem.GetComponent<Items>().price;
                        inventaire.addItem(selectedItem);

                        for (int i = 0; i < itemsForSale.Count; i++)
                        {
                            if (selectedItem == (GameObject)itemsForSale[i][0])
                            {
                                int amount = (int)itemsForSale[i][1];
                                amount--;
                                itemsForSale[i][1] = amount;

                                if ((int)itemsForSale[i][1] <= 0)
                                {
                                    itemsForSale.Remove(itemsForSale[i]);
                                }

                                break;
                            }
                        }
                        ShowBuyItems();
                    } else if(ConfirmPopUp.instance.result == 0)
                    {
                        break;
                    }
                }
                for (int i = 0; i < itemsForSale.Count; i++)
                {
                    buyItemButtons[i].gameObject.GetComponent<Button>().interactable = true;
                }
            }
            else
            {
                ConfirmPopUp.instance.OpenPopupSimple("Sorry, you do not have enough money for this item");
            }
        }

        goldText.text = Player.instance.gold.ToString() + "g";
    }

    public void OpenSellMenu()
    {
        sellMenu.SetActive(true);
        buyMenu.SetActive(false);

        ShowSellItem();
    }

    public void ShowSellItem()
    {
        GameObject[] listItems = inventaire.listeItems;

        for (int i = 0; i < listItems.Length; i++)
        {
            GameObject item = (GameObject)listItems[i];

            if (item != null)
            {
                Debug.Log(item.name);
                sellItemButtons[i].buttonImage.gameObject.SetActive(true);
                sellItemButtons[i].buttonImage.sprite = item.GetComponent<SpriteRenderer>().sprite;
                sellItemButtons[i].amountText.text = "";
                sellItemButtons[i].gameObject.GetComponent<Button>().interactable = true;
                sellItemButtons[i].setItemReferenced(item);
            }
            else
            {
                sellItemButtons[i].buttonImage.gameObject.SetActive(false);
                sellItemButtons[i].amountText.text = "";
                sellItemButtons[i].gameObject.GetComponent<Button>().interactable = false;
            }
        }

        for (int i = listItems.Length; i < sellItemButtons.Length; i++)
        {
            sellItemButtons[i].buttonImage.gameObject.SetActive(false);
            sellItemButtons[i].amountText.text = "";
            sellItemButtons[i].gameObject.GetComponent<Button>().interactable = false;
        }
    }

    public void SelectSellItem(GameObject sellItemSelected)
    {
        selectedItem = sellItemSelected;
        sellItemName.text = selectedItem.GetComponent<Items>().itemName;
        sellItemDescription.text = selectedItem.GetComponent<Items>().description;
        sellItemPrice.text = "Value: " + Mathf.FloorToInt(selectedItem.GetComponent<Items>().price * 0.5f).ToString() + "g";
    }

    public void SellItemCallBack()
    {
        StartCoroutine(SellItem());
    }

    public IEnumerator SellItem()
    {
        if(selectedItem != null)
        {
            ConfirmPopUp.instance.OpenPopupConfirm("Are you sure you want to sell this item?");
            while (ConfirmPopUp.instance.result == -1)
            {
                for (int i = 0; i < inventaire.listeItems.Length; i++)
                {
                    sellItemButtons[i].gameObject.GetComponent<Button>().interactable = false;
                }

                yield return null;


                if (ConfirmPopUp.instance.result == 1)
                {
                    Player.instance.gold += Mathf.FloorToInt(selectedItem.GetComponent<Items>().price * 0.5f);
                    inventaire.retirerItem(selectedItem);

                    ShowSellItem();
                }
                else if (ConfirmPopUp.instance.result == 0)
                {
                    break;
                }
            }

            /*
            for (int i = 0; i < inventaire.listeItems.Length; i++)
            {
                sellItemButtons[i].gameObject.GetComponent<Button>().interactable = true;
            }*/
        }

        goldText.text = Player.instance.gold.ToString() + "g";
    }

}
