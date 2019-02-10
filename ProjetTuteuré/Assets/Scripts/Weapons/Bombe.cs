using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bombe : MonoBehaviour
{
    public GameObject flammeProjectile;
    public float airTime;
    public bool hasExploded;

    // Start is called before the first frame update
    void Start()
    {
        hasExploded = false;
        airTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(!hasExploded && Time.time - airTime > 1.5)
        {
            Debug.Log("bah");
            hasExploded = true;
            explode(); 
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!hasExploded)
        {
            hasExploded = true;
            explode();

        }
    }

    private void explode()
    {
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;

        GameObject flame1 = Instantiate(flammeProjectile, gameObject.transform.position, Quaternion.identity);
        flame1.transform.Rotate(0, 0, 45);
        flame1.GetComponent<Projectile>().isFriendly = false;
        flame1.GetComponent<Rigidbody2D>().velocity = new Vector2(5, 5);
        GameObject flame2 = Instantiate(flammeProjectile, gameObject.transform.position, Quaternion.identity);
        flame2.transform.Rotate(0, 0, 135);
        flame2.GetComponent<Projectile>().isFriendly = false;
        flame2.GetComponent<Rigidbody2D>().velocity = new Vector2(-5, 5);
        GameObject flame3 = Instantiate(flammeProjectile, gameObject.transform.position, Quaternion.identity);
        flame3.transform.Rotate(0, 0, 225);
        flame3.GetComponent<Projectile>().isFriendly = false;
        flame3.GetComponent<Rigidbody2D>().velocity = new Vector2(-5, -5);
        GameObject flame4 = Instantiate(flammeProjectile, gameObject.transform.position, Quaternion.identity);
        flame4.transform.Rotate(0, 0, 315);
        flame4.GetComponent<Projectile>().isFriendly = false;
        flame4.GetComponent<Rigidbody2D>().velocity = new Vector2(5, -5);
        Destroy(gameObject);
    }
}
