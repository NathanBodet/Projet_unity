using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    private Rigidbody2D rigidBody;

	void Start () {
        rigidBody = GetComponent<Rigidbody2D>();
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Enemy") {
            collision.collider.GetComponentInParent<Enemy>().TakeDamage(5);
            rigidBody.velocity = Vector2.zero;
        }
        Destroy(gameObject);
    }


}
