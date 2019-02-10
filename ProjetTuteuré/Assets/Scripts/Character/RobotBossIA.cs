using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotBossIA : MonoBehaviour
{
    public int state,attackType;
    public float attackTime,attack0Time,jumpTime,startPos;

    // Start is called before the first frame update
    void Start()
    {
        state = 0;
        jumpTime = Time.time;
        startPos = gameObject.transform.position.y;
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
        if(state == 0)
        {
            if(Time.time - jumpTime > 1)
            {
                state = 1;
                GetComponent<Animator>().SetBool("hasFinishedJump", true);
                return;
            } else
            {
                Vector3 pos = new Vector3(gameObject.transform.position.x, startPos + 3-(Mathf.Pow(1 - (Time.time-jumpTime)*2, 2)*3),0);
                gameObject.transform.SetPositionAndRotation(pos, Quaternion.identity);
            }
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
