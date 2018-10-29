using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item {

    public int damage;

    public GameObject player;

    // Use this for initialization
    protected virtual void Start () {
		
	}
	
	// Update is called once per frame
	protected virtual void Update () {
		
	}

    public void equip(GameObject player)
    {
        this.player = player;
    }
}
