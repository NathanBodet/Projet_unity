using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : RangedWeapon {

	public void Fire()
    {
        for(int i =0; i < nbBalles; i++)
        {
            Debug.Log("pan");
            base.Fire();
        }
    }
}
