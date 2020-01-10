using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    [SerializeField] protected float damage;
    [SerializeField] protected Vector2 strikeForce;
    [SerializeField] protected float speed;
    [SerializeField] protected float flyDistance;
    protected Rigidbody2D rb;
    protected Transform caster;

    protected virtual void Start()
    {
        caster = GetComponentInParent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        Vector2 velosity = new Vector2(speed * transform.localScale.x, 0);
        rb.velocity = velosity;
    }

    protected virtual void CheckForOutrange()
    {
        float distance = Vector2.Distance(transform.position, caster.position);
        if (distance > flyDistance)
            Destroy(gameObject);
    }    
}
