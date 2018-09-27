using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Actor {

    public static int TotalEnemies;
    public AutoWalk walker;
    public bool stopMovementWhenHit = true;

    public EnemyAI ai;

    protected override void Start()
    {
        base.Start();
    }

    public void RegisterEnemy()
    {
        TotalEnemies++;

    }

    protected override void Die()
    {
        base.Die();
        ai.enabled = false;
        walker.enabled = false;
        TotalEnemies--;
    }

    public void MoveTo(Vector3 targetPosition)
    {
        walker.MoveTo(targetPosition);
    }

    //determines whether the enemy can walk to the right (positive) or the left (negative)
    public void MoveToOffset(Vector3 targetPosition, Vector3 offset)
    {
        if (!walker.MoveTo(targetPosition + offset))
        {
            walker.MoveTo(targetPosition - offset);
        }
    }

    public void Wait()
    {
        walker.StopMovement();
    }

    public override void TakeDamage(float value, Vector3 hitVector)
    {
        if (stopMovementWhenHit)
        {
            walker.StopMovement();
        }
        base.TakeDamage(value, hitVector);
    }

    public override bool CanWalk()
    {
        return !animator.GetCurrentAnimatorStateInfo(0).IsName("Death");
    }
}
