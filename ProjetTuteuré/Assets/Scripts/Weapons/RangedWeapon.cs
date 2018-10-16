using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : Weapon {


    public int ammo;
    public float range;

    public float fireRate = 0.5f;
    private float nextFire = 0f;

    public GameObject ballPrefab;

    // attributs concernant le combat
    float switchCooldown = 0.0f;

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}

}
