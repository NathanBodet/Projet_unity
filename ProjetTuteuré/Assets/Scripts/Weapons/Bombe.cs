using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bombe : MonoBehaviour
{
    public GameObject flammeProjectile;
    public float airTime;
    public bool hasExploded,isCross;

    // Start is called before the first frame update
    void Start()
    {
        hasExploded = false;
        airTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(!hasExploded && Time.time - airTime > 1)
        {

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
        if (isCross)
        {
            GameObject flame1 = Instantiate(flammeProjectile, gameObject.transform.position, Quaternion.identity);
            flame1.GetComponent<Projectile>().isFriendly = false;
            flame1.GetComponent<Rigidbody2D>().velocity = new Vector2(12, 0);
            GameObject flame2 = Instantiate(flammeProjectile, gameObject.transform.position, Quaternion.identity);
            flame2.GetComponent<Projectile>().isFriendly = false;
            flame2.GetComponent<Rigidbody2D>().velocity = new Vector2(-12, 0);
            GameObject flame3 = Instantiate(flammeProjectile, gameObject.transform.position, Quaternion.identity);
            flame3.GetComponent<Projectile>().isFriendly = false;
            flame3.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -12);
            GameObject flame4 = Instantiate(flammeProjectile, gameObject.transform.position, Quaternion.identity);
            flame4.GetComponent<Projectile>().isFriendly = false;
            flame4.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 12);
            Destroy(gameObject);
        } else
        {
            GameObject flame1 = Instantiate(flammeProjectile, gameObject.transform.position, Quaternion.identity);
            flame1.GetComponent<Projectile>().isFriendly = false;
            flame1.GetComponent<Rigidbody2D>().velocity = new Vector2(8, 8);
            GameObject flame2 = Instantiate(flammeProjectile, gameObject.transform.position, Quaternion.identity);
            flame2.GetComponent<Projectile>().isFriendly = false;
            flame2.GetComponent<Rigidbody2D>().velocity = new Vector2(-8, 8);
            GameObject flame3 = Instantiate(flammeProjectile, gameObject.transform.position, Quaternion.identity);
            flame3.GetComponent<Projectile>().isFriendly = false;
            flame3.GetComponent<Rigidbody2D>().velocity = new Vector2(-8, -8);
            GameObject flame4 = Instantiate(flammeProjectile, gameObject.transform.position, Quaternion.identity);
            flame4.GetComponent<Projectile>().isFriendly = false;
            flame4.GetComponent<Rigidbody2D>().velocity = new Vector2(8, -8);
        }
        
    }
}
