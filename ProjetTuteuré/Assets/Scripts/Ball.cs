using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    private Rigidbody2D rigidBody;

	void Start () {
        rigidBody = GetComponent<Rigidbody2D>();
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
