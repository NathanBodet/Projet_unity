using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponEquipper : MonoBehaviour {

    public static string activeWeaponType;

	void Start () {
        activeWeaponType = "Sword";
	}
	
	void Update () {
        if (Input.GetKeyDown("&"))
        {
            //TODO
        } else if (Input.GetKeyDown("é"))
        {
            //TODO
        }
	}
}
