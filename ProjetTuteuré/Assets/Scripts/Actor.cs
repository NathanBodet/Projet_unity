using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Actor : MonoBehaviour {

    public float speed;

    public Animator animator;
    public Rigidbody2D rigidBody;

    public bool isAlive = true;
    public float maxLife = 100f;
    public float currentLife = 100f;

    protected virtual void Start () {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
    }
	
	void Update () {
	}

}
