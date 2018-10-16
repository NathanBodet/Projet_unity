using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : RangedWeapon {

    private Rigidbody2D rigidBody;
    float originCoordX, originCoordY;

    void Start () {
        this.damage = 5;
        this.range = 10f;
        rigidBody = GetComponent<Rigidbody2D>();
        originCoordX = transform.position.x;
        originCoordY = transform.position.y;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Enemy")
        {
            collision.collider.GetComponentInParent<Enemy>().TakeDamage(damage);
            rigidBody.velocity = Vector2.zero;
        }
        Destroy(gameObject);
    }
    private void Update()
    {
        if ((float)System.Math.Sqrt(System.Math.Pow(originCoordX - transform.position.x, 2) + System.Math.Pow(originCoordY - transform.position.y, 2)) > range)
        {
            Destroy(gameObject);
        }
    }



}
