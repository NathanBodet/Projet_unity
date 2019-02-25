using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ItemButton : MonoBehaviour
{
    public Image buttonImage;
    public Text amountText;
    public int buttonValue;
    public GameObject itemReferenced;

    public void Press()
    {
        if (Shop.instance.shopMenu.activeInHierarchy)
        {
            if (Shop.instance.buyMenu.activeInHierarchy)
            {
                Shop.instance.SelectBuyItem(itemReferenced);
            } else if (Shop.instance.sellMenu.activeInHierarchy)
            {
                Shop.instance.SelectSellItem(itemReferenced);
            }
        }
    }

    public void setItemReferenced(GameObject item)
    {
        itemReferenced = item;
    }
}
