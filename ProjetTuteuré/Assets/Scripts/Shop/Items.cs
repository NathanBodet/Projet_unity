using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Items : MonoBehaviour  
{

    [Header("Item type")]
    public bool isItem;
    public bool isWeapon;
    public bool isArmour;

    [Header("Item Details")]
    public string itemName;
    public string description;
    public int price;
    public bool affectHP, affectStr, affectStam, affectAgi;
    public int amountToChange;

    [Header("Weapon/Armor Details")]
    public int weaponStrength;
    public int armorStrength;

    /*public void Use(Player player)
    {
        if (isItem)
        {
            if (affectHP)
            {
                player.currentHealth += amountToChange;
                if(player.currentHealth > player.maxHealth)
                {
                    player.currentHealth = player.maxHealth;
                }
            }
        }

        //if weapon : equip

        //if armor : equip

        //remove
    }*/

}
