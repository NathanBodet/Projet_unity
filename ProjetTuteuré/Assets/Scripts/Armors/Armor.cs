using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor : Item
{
    public int strength;
    public int agility;
    public int endurance;

    public void equip(GameObject player)
    {
        this.player = player;
    }
}
