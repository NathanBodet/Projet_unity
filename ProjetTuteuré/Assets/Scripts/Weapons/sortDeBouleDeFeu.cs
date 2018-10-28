using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sortDeBouleDeFeu : RangedWeapon {

    public GameObject projectilePrefab;

	// Use this for initialization
	void Start () {
        damage = 25;
        range = 10;
        projectileSpeed = 5f;
		
	}

    public void Fire()
    {
        base.Fire(projectilePrefab);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
