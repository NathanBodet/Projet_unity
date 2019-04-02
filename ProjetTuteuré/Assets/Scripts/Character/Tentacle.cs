using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tentacle : MonoBehaviour
{

    public float time;
    public bool aDisparu;
    // Start is called before the first frame update
    void Start()
    {
        time = Time.time;
        aDisparu = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - time > 1.3125 && !aDisparu)
        {
            gameObject.GetComponent<Animator>().SetBool("disappear", true);
            aDisparu = true;
            time = Time.time;
        }
        if(aDisparu && Time.time - time > 1.3125)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.GetComponent<Player>().TakeDamage(10,new Vector3(0,0,0),0,true);
        }
    }
}
