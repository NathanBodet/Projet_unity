using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyDistanceIA : MonoBehaviour
{

    public enum EnemyAction
    {
        None,
        Wait,
        Attack,
        Chase,
        Roam,
        Flee
    }

    public class DecisionWeight
    {
        public int weight;
        public EnemyAction action;
        public DecisionWeight(int weight, EnemyAction action)
        {
            this.weight = weight;
            this.action = action;
        }
    }

    Enemy enemy;
    GameObject player;
    public GameObject projectile;

    public float attackReachMin, attackReachMax, attackCooldown, projectileSpeed;
    private float tempsDerniereAttaque ;

    public PlayerDetector playerDetector;
    List<DecisionWeight> weights;
    public EnemyAction currentAction = EnemyAction.None;

    private float decisionDuration;

    void Start()
    {
        weights = new List<DecisionWeight>();
        enemy = GetComponent<Enemy>();
        GetComponent<Enemy>().maxHealth = GameObject.Find("GameManager").GetComponent<GameManager>().numeroNiveau * 16 + 150;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Chase()
    {
        enemy.speed = 5f;
        enemy.animator.SetBool("IsMoving", true);
        Vector3 direction = player.transform.position - transform.position;

        enemy.animator.SetFloat("DirectionX", direction.x);
        enemy.animator.SetFloat("DirectionY", direction.y);

        direction.Normalize();

        enemy.rigidBody.velocity = direction * enemy.speed;

        decisionDuration = Random.Range(0.2f, 0.4f);
    }

    private void Flee()
    {
        enemy.speed = 5f;
        enemy.animator.SetBool("IsMoving", true);
        Vector3 direction = transform.position - player.transform.position;

        enemy.animator.SetFloat("DirectionX", direction.x);
        enemy.animator.SetFloat("DirectionY", direction.y);

        direction.Normalize();
        enemy.rigidBody.velocity = direction * enemy.speed;

        decisionDuration = Random.Range(0.2f, 0.4f);
    }

    private void Wait()
    {
        decisionDuration = Random.Range(0.2f, 0.5f);
        enemy.StopMovement();
    }

    private void Attack()
    {
        enemy.Attack(projectile);
        decisionDuration = Random.Range(0f, 0.5f);
    }

    private void Roam()
    {
        float randomDegree = Random.Range(0, 360);
        Vector2 offset = new Vector2(Mathf.Sin(randomDegree), Mathf.Cos(randomDegree));
        float distance = Random.Range(30, 70);
        offset *= distance;
        Vector3 directionVector = new Vector3(offset.x, offset.y, 0);
        enemy.speed = 2f;
        enemy.animator.SetFloat("DirectionX", directionVector.x);
        enemy.animator.SetFloat("DirectionY", directionVector.y);
        enemy.animator.SetBool("IsMoving", true);
        directionVector.Normalize();

        enemy.rigidBody.velocity = directionVector * enemy.speed;

        decisionDuration = Random.Range(0.3f, 0.6f);
    }

    private void DecideWithWeights(int attack, int wait, int chase, int move, int flee)
    {
        weights.Clear();

        if (attack > 0)
        {
            weights.Add(new DecisionWeight(attack, EnemyAction.Attack));
        }
        if (chase > 0)
        {
            weights.Add(new DecisionWeight(chase, EnemyAction.Chase));
        }
        if (wait > 0)
        {
            weights.Add(new DecisionWeight(wait, EnemyAction.Wait));
        }
        if (move > 0)
        {
            weights.Add(new DecisionWeight(move, EnemyAction.Roam));
        }
        if (flee > 0)
        {
            weights.Add(new DecisionWeight(flee, EnemyAction.Flee));
        }

        int total = attack + chase + wait + move + flee;
        int intDecision = Random.Range(0, total - 1);

        foreach (DecisionWeight weight in weights)
        {
            //substract the value of each possible EnemyAction weight in the weights list from the random index value until <= 0
            intDecision -= weight.weight;
            if (intDecision <= 0)
            {
                SetDecision(weight.action);
                break;
            }
        }
    }

    private void SetDecision(EnemyAction action)
    {
        currentAction = action;

        switch (action)
        {
            case EnemyAction.Attack:
                
                if (Time.time - tempsDerniereAttaque > attackCooldown)
                {
                    Attack();
                    tempsDerniereAttaque = Time.time;
                } else
                {
                    SetDecision(EnemyAction.Chase);
                }
                break;
            case EnemyAction.Chase:
                Chase();
                break;
            case EnemyAction.Roam:
                Roam();
                break;
            case EnemyAction.Wait:
                Wait();
                break;
            case EnemyAction.Flee:
                Flee();
                break;
        }
    }

    void Update()
    {

        //On récupère la direction dans laquelle est le player, pour plus tard
        Vector3 direction = player.transform.position - transform.position;
        direction.Normalize();

        //calculates the distance between the hero and enemy
        //no need the actual distance, only the squared distance, because the square root operation is expensive and unnecessary
        float sqrDistance = Vector3.SqrMagnitude(player.transform.position - transform.position);
        //sets true when the distance between the hero and enemy falls between attackReachMin and attackReachMax
        bool canReach = attackReachMin < sqrDistance && sqrDistance < attackReachMax ;
        //Debug.Log(sqrDistance);
        //Debug.Log(attackReachMax);
        //Debug.Log(attackReachMin);
        if (canReach && currentAction == EnemyAction.Chase)
        {
            SetDecision(EnemyAction.Wait);
        }

        if (decisionDuration > 0.0f)
        {
            decisionDuration -= Time.deltaTime;
        }
        else
        {
            //Si le player est loin OU si le player est prêt mais qu'il y a un mur qui bloque, on passe en mode ROAM
            //Comportement très temporaire, mais le test sera utilse plus tard dans le path finding !
            /*
             Debug.Log((playerDetector.playerIsNearby && Physics2D.Raycast(transform.position, direction,
              (float)System.Math.Sqrt(System.Math.Pow(player.transform.position.x - transform.position.x, 2)
              + System.Math.Pow(player.transform.position.y - transform.position.y, 2)
             ), 1 << 0)));
               */
            if (!playerDetector.playerIsNearby || (playerDetector.playerIsNearby && Physics2D.Raycast(transform.position, direction,
                (float)System.Math.Sqrt(System.Math.Pow(player.transform.position.x - transform.position.x, 2)
                + System.Math.Pow(player.transform.position.y - transform.position.y, 2)
               ), 1 << 0)))
            {
                DecideWithWeights(0, 30, 0, 70,0);
            }
            else
            {
                //Debug.Log(canReach);

                if (canReach)
                {
                    DecideWithWeights(100, 0, 0, 0,0);
                }
                else
                {
                    if (sqrDistance > attackReachMax)
                    {
                        DecideWithWeights(0, 0, 100, 0, 0);
                    }
                    else if (sqrDistance < attackReachMin)
                    {
                        DecideWithWeights(0, 0, 0, 0, 100);
                    }
                }
            }
        }
    }
}
