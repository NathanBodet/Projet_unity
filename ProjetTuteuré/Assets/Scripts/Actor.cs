using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Actor : MonoBehaviour {

    public float speed;

    public Animator animator;
    public Rigidbody2D rigidBody;
    public SpriteRenderer spriteRenderer;

    public bool isAlive = true;
    public float maxLife = 100f;
    public float currentLife = 100f;

    protected virtual void Start () {
        currentLife = maxLife;
        isAlive = true;
        //animator alive à rajouter
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
	
	void Update () {
	}

    public void Attack()
    {
        animator.SetTrigger("Attacking");
    }

    public virtual void DidHitObject(Collider collider, Vector3 hitPoint, Vector3 hitVector)
    {
        Actor actor = collider.GetComponent<Actor>();
        if(actor != null && actor.CanBeHit() && collider.tag != gameObject.tag)
        {
            if(collider.attachedRigidbody != null)
            {
                HitActor(actor, hitPoint, hitVector);
            }
        }
    }

    protected virtual void HitActor(Actor actor, Vector3 hitPoint, Vector3 hitVector)
    {
        actor.TakeDamage(10, hitVector);
    }

    protected virtual void Die()
    {
        isAlive = false;
        //animator alive à rajouter
        StartCoroutine(DeathFlicker());
    }

    protected virtual void SetOpacity(float value)
    {
        Color color = spriteRenderer.color;
        color.a = value;
        spriteRenderer.color = color;
    }

    private IEnumerator DeathFlicker()
    {
        for(int i = 0; i < 5; i++)
        {
            SetOpacity(0.5f);
            yield return new WaitForSeconds(0.1f);
            SetOpacity(1.0f);
            yield return new WaitForSeconds(0.1f);
        }
    }

    public virtual void TakeDamage(float value, Vector3 hitVector)
    {
        currentLife -= value;

        if(isAlive && currentLife <= 0)
        {
            Die();
        }
        //else rajouter animator ishurt
    }

    public bool CanBeHit()
    {
        return isAlive;
    }

    public virtual bool CanWalk()
    {
        return true;
    }

}
