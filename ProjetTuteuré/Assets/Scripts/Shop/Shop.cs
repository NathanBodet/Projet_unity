using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{

    public static Shop instance;

    public GameObject shopMenu;
    public GameObject buyMenu;
    public GameObject sellMenu;

    public Text goldText;

    public GameObject[] itemsForSale;

    public ItemButton[] buyItemButtons;
    public ItemButton[] sellItemButtons;

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
        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log("Clic");
            OpenShop();
        }
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

        //TODO: parcours des items et display
    }

    public void OpenSellMenu()
    {
        sellMenu.SetActive(true);
        buyMenu.SetActive(false);
    }
}
