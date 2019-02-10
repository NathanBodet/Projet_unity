using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotBossIA : MonoBehaviour
{
    public int state,attackType;
    public float attackTime,attack0Time;

    // Start is called before the first frame update
    void Start()
    {
        state = 1;
        GetComponent<Animator>().SetBool("hasFinishedJump", true);

    }

    // Update is called once per frame
    void Update()
    {
        if (!gameObject.GetComponent<Enemy>().isAlive)
        {
            gameObject.GetComponent<Animator>().SetBool("hasDied", true);
            gameObject.GetComponent<EdgeCollider2D>().enabled = false;
            return;
        }
        if(state == 1)
        {
            if(Time.time - attackTime > 5f)
            {
                attackTime = Time.time;
                attackType = (int)Random.Range(0, 3);
            }
            switch (attackType)
            {
                case 0:
                    break;
                    
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            collision.gameObject.GetComponent<Player>().TakeDamage(10,Vector3.zero,10,false);
        }
    }
}
