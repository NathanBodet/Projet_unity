using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingLightning : MonoBehaviour
{

    public GameObject flammeProjectile, player;
    public float airTime;
    public bool hasExploded;

    // Start is called before the first frame update
    void Start()
    {
        hasExploded = false;
        airTime = Time.time;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - airTime > 5)
        {
            if (!hasExploded)
            {
                explode();
                return;
            }
        }
        Vector3 targetPos = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPos, 2 * Time.deltaTime);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            player.GetComponent<Player>().TakeDamage(10, Vector3.zero, 50, true);
            if (!hasExploded)
            {
                explode();
            }
        }
    }


    private void explode()
    {
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;

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
        GameObject flame5 = Instantiate(flammeProjectile, gameObject.transform.position, Quaternion.identity);
        flame5.GetComponent<Projectile>().isFriendly = false;
        flame5.GetComponent<Rigidbody2D>().velocity = new Vector2(8, 8);
        GameObject flame6 = Instantiate(flammeProjectile, gameObject.transform.position, Quaternion.identity);
        flame6.GetComponent<Projectile>().isFriendly = false;
        flame6.GetComponent<Rigidbody2D>().velocity = new Vector2(-8, 8);
        GameObject flame7 = Instantiate(flammeProjectile, gameObject.transform.position, Quaternion.identity);
        flame7.GetComponent<Projectile>().isFriendly = false;
        flame7.GetComponent<Rigidbody2D>().velocity = new Vector2(-8, -8);
        GameObject flame8 = Instantiate(flammeProjectile, gameObject.transform.position, Quaternion.identity);
        flame8.GetComponent<Projectile>().isFriendly = false;
        flame8.GetComponent<Rigidbody2D>().velocity = new Vector2(8, -8);
        Destroy(gameObject);
    }
}
