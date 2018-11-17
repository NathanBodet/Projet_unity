using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTimer : MonoBehaviour {

    private float duration;
    private bool isActive;

	public void EnableTimer(float duration)
    {
        this.duration = duration;
        isActive = true;
    }

	void Update () {
        if (isActive)
        {
            duration -= Time.deltaTime;
            if(duration < 0)
            {
                Destroy(gameObject);
            }
        }
	}
}
