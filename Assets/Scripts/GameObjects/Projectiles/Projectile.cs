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
    public Transform caster;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Vector2 velosity = new Vector2(speed * transform.localScale.x, 0);
        rb.velocity = velosity;
    }

    protected virtual void CheckForOutrange()
    {
        if (caster != null)
        {
            float distance = Vector2.Distance(transform.position, caster.position);
            if (distance > flyDistance)
                Destroy(gameObject);
        }
        else
            Destroy(gameObject);
    }

    public void SetVelocityDirection(Vector2 dir)
    {
        rb.velocity = dir.normalized * speed;
    }
}
