using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour {

    Rigidbody2D rb;
    public GameObject walls;

	// Use this for initialization
	void Start () {

        rb = GetComponent<Rigidbody2D>();
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update () {
		
	}
}
