using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapScript : MonoBehaviour {

    float damageCooldown = 0.5f;
    float lastDamageTime = 0f;
    public bool isDiscovered = false;
    

    // Use this for initialization
    void Start () {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (!isDiscovered)
            {
                isDiscovered = true;
                gameObject.GetComponent<SpriteRenderer>().enabled = true;
            }
            if (Time.time > lastDamageTime + damageCooldown)
            {
                collision.gameObject.GetComponent<Player>().TakeDamage(10);
                lastDamageTime = Time.time;
            }
        }
    }
}
