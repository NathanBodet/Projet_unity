using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AliseAI : MonoBehaviour
{

    private int state, choice;

    private float timeAttack1;
    private float dureeAttack1 = 4f;

    public Enemy enemy;

    public GameObject lightBall, lightStrokePrefab;


    private bool isAttackFinished;

    // Start is called before the first frame update
    void Start()
    {
        
        GetComponent<Animator>().SetFloat("Health", enemy.currentHealth);

        state = 1;

        timeAttack1 = Time.time;

        isAttackFinished = true;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Animator>().SetFloat("Health", enemy.currentHealth);
        if (enemy.currentHealth <= 600)
        {
            state = 2;
        }

        if (!enemy.isAlive)
        {
            StartCoroutine(Die());
        }


        if (state == 1)
        {
            if (isAttackFinished)
            {
                GetComponent<Animator>().SetBool("IsAttackFinished", true);
                choice = Random.Range(1, 4);
               
                GetComponent<Animator>().SetInteger("Choice", choice);

                isAttackFinished = false;

                switch (choice)
                {
                    case 1:
                        Attack1();
                        break;
                    case 2:
                        StartCoroutine(Attack2());
                        break;
                    case 3:
                        
                        isAttackFinished = true;
                        break;
                }
            }

        } else if(state == 2)
        {
            
            //attaques 2
        }


    }

    private IEnumerator Die()
    {
        GetComponent<Animator>().SetBool("IsAlive", enemy.isAlive);
        yield return new WaitForSeconds(10);
        Destroy(gameObject);
    }

    private void Attack1()
    {
        
        if (Time.time - timeAttack1 > dureeAttack1)
        {
            timeAttack1 = Time.time;
            Instantiate(lightBall, new Vector3(gameObject.transform.position.x - 3, gameObject.transform.position.y + 1.3f, 0), Quaternion.identity).GetComponent<Rigidbody2D>().velocity = new Vector2(-2, 0);
        }
        isAttackFinished = true;
    }

    private IEnumerator Attack2()
    {
        
        for(int i = 0; i < 12; i++)
        {
            Instantiate(lightStrokePrefab, new Vector3(GameObject.FindGameObjectWithTag("Player").transform.position.x, GameObject.FindGameObjectWithTag("Player").transform.position.y + 3.7f, 0), Quaternion.identity);
            yield return new WaitForSeconds(1);

        }
        isAttackFinished = true;
    }


}
