using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkBossIA : MonoBehaviour
{
    
    public int state, attackType,nbTentacles;
    public float attackTime, disparitionTime,waitTime, tentTime;
    public Vector2 startPos;
    bool attackTir1, aPrisPos;
    public GameObject tentaclePrefab;
    Vector3 pos;

    void Start()
    {
        state = -1;
        nbTentacles = 0;
        attackTir1 = true;
        waitTime = Time.time;
        tentTime = Time.time;
        startPos = new Vector2(gameObject.transform.position.x,gameObject.transform.position.y);
    }

    
    void Update()
    {
        if (!gameObject.GetComponent<Enemy>().isAlive)
        {
            gameObject.GetComponent<Animator>().SetBool("IsAlive", false);
            gameObject.GetComponent<Animator>().SetBool("hasToDisepear", true);
            gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
            return;
        }
        if (state == 0)//sorti
        {
            if (!attackTir1)
            {
                if(Time.time - tentTime > 0.2 && !aPrisPos)
                {
                    pos = new Vector3(GameObject.Find("Player").transform.position.x, GameObject.Find("Player").transform.position.y + 3.7f, 0);
                    aPrisPos = true;
                }
                if (Time.time - tentTime > 0.5)
                {
                    Instantiate(tentaclePrefab, pos, Quaternion.identity);
                    nbTentacles++;
                    tentTime = Time.time;
                    if (nbTentacles == 12)
                    {
                        attackTir1 = true;
                    }
                    aPrisPos = false;
                    
                }
                
                
            }
            if(Time.time - waitTime > 5)
            {
                gameObject.GetComponent<Animator>().SetBool("hasToAppear", false);
                gameObject.GetComponent<Animator>().SetBool("hasToDisepear", true);
                gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
                state = 1;
                waitTime = Time.time;

            }
        } else if (state == 1)//disparition et réapparition
        {
           if(Time.time - waitTime > 3)
            {
                int rand = Random.Range(0, 2);
                int rand2 = Random.Range(0, 3);
                gameObject.transform.position = new Vector3(startPos.x+(rand-1)*10,startPos.y-(rand2-1)*10, 0);
                gameObject.GetComponent<Animator>().SetBool("hasToDisepear", false);
                gameObject.GetComponent<Animator>().SetBool("hasToAppear", true);
                gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
                state = 0;
                pos = new Vector3(GameObject.Find("Player").transform.position.x, GameObject.Find("Player").transform.position.y + 3, 0);
                tentTime = Time.time;
                attackTir1 = false;
                waitTime = Time.time;
            }
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
