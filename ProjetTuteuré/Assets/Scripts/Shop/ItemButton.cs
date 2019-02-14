﻿using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ItemButton : MonoBehaviour
{
    public Image buttonImage;
    public Text amountText;
    public int buttonValue;
    private GameObject itemReferenced;

    public void Press()
    {
        if (Shop.instance.shopMenu.activeInHierarchy)
        {
            if (Shop.instance.buyMenu.activeInHierarchy)
            {
                Shop.instance.SelectBuyItem(itemReferenced);
            }
        }
    }

    public void setItemReferenced(GameObject item)
    {
        itemReferenced = item;
    }
}
