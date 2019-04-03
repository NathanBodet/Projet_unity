using System.Collections;
using UnityEngine;

public class AliseAI : MonoBehaviour
{

    private int state, choice;

    public Enemy enemy;

    public GameObject lightBall, lightStrokePrefab;

    private bool isAttackFinished;


    // Start is called before the first frame update
    void Start()
    {

        enemy.animator.SetFloat("Health", enemy.currentHealth);

        state = 1;

        isAttackFinished = true;

    }

    // Update is called once per frame
    void Update()
    {
        enemy.animator.SetFloat("Health", enemy.currentHealth);
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
                ChooseAttack();
            }

        }

    }

    private IEnumerator Die()
    {
        yield return new WaitForSeconds(8.5f);
        enemy.animator.SetTrigger("Explosion");
        yield return new WaitForSeconds(5f);
        Destroy(transform.parent.gameObject);
    }

    private IEnumerator Attack1()
    {
        Instantiate(lightBall, new Vector3(gameObject.transform.position.x - 3, gameObject.transform.position.y + 1.3f, 0), Quaternion.identity).GetComponent<Rigidbody2D>().velocity = new Vector2(-2, 0);
        yield return new WaitForSeconds(3f);
        isAttackFinished = true;

    }

    private IEnumerator Attack2()
    {
    
        for(int i = 0; i < 5; i++)
        {
            Instantiate(lightStrokePrefab, new Vector3(GameObject.FindGameObjectWithTag("Player").transform.position.x, GameObject.FindGameObjectWithTag("Player").transform.position.y + 3.5f, 0), Quaternion.identity);
            yield return new WaitForSeconds(1f);
        }

        isAttackFinished = true;
    }

    private void ChooseAttack()
    {
        choice = Random.Range(0, 3);
        enemy.animator.SetInteger("Choice", choice);
        enemy.animator.SetBool("IsAttackFinished", true);

        switch (choice)
        {
            case 0:
                isAttackFinished = false;
                enemy.animator.SetBool("IsAttackFinished", false);
                StartCoroutine(Attack1());
                break;
            case 1:
                isAttackFinished = false;
                enemy.animator.SetBool("IsAttackFinished", false);
                StartCoroutine(Attack2());
                break;
        }
    }

}
