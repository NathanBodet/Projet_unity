using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemShop : MonoBehaviour
{
    [Header("Item type")]
    public bool isItem;
    public bool isWeapon;
    public bool isArmour;

    [Header("Item Details")]
    public string itemName;
    public string description;
    public int value;
    public Sprite itemSprite;

    [Header("Item Details")]
    public int amountToChange;
    public bool affectHP, affectStr, affectStam, affectAgi;

    [Header("Weapon/Armor Details")]
    public int weaponStrength;
    public int armorStrength;

    //TODO
    public void Use()
    {

    }
}
