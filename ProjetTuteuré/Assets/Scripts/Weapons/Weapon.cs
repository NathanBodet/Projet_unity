using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item {

    public int damage;
    public float strratio;
    public float agiratio;
    public float endratio;

    public void equip(GameObject player)
    {
        this.player = player;
    }
}
