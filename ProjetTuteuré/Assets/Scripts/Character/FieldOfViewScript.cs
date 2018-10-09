using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfViewScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter2D (Collision2D collision)
    {
        transform.parent.GetComponent<EnemyAI>().CollisionDetected(this);
        Debug.Log("je touche");
    }

}
