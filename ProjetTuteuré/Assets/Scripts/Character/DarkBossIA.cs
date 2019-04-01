using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkBossIA : MonoBehaviour
{
    
    public int state, attackType;
    public float attackTime, disparitionTime, startPos, flammeTime;
    bool attackTir1, attackTir2, attackTir3;

    void Start()
    {
        state = -1;
        startPos = gameObject.transform.position.y;
    }

    
    void Update()
    {
        if (!gameObject.GetComponent<Enemy>().isAlive)
        {
            gameObject.GetComponent<Animator>().SetBool("IsAlive", false);
            gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
            return;
        }
        if (state == 0)
        {
        }
        if (state == 1)
        {
           
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            collision.gameObject.GetComponent<Player>().TakeDamage(10, Vector3.zero, 10, false);
        }
    }
}
