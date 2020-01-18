using System.Collections;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    protected Transform target;
    protected EnemyStats stats;
    protected EnemyCombat combat;

    public EnemyAnimationControl anim;


    protected virtual void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        stats = GetComponent<EnemyStats>();
        combat = GetComponentInChildren<EnemyCombat>();
    }

    protected virtual void FixedUpdate()
    {
        float distanceFromSpot = Vector2.Distance(target.position, stats.aggroPoint.position);

        if (distanceFromSpot < stats.aggroRange)
        {
            Vector2 xToTarget = new Vector2(target.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, xToTarget, stats.speed * Time.deltaTime);

            AnimateMove(target.position);
        }
        else if (!stats.bodyCollider.OverlapPoint(stats.aggroPoint.position))
        {
            Vector2 xToSpot = new Vector2(stats.aggroPoint.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, xToSpot, stats.speed * Time.deltaTime);

            AnimateMove(stats.aggroPoint.position);
        }
        else
            anim.PlayStand();        
    }

    public virtual void AnimateMove(Vector2 targetPos)
    {
        anim.PlayMove();

        if (targetPos.x > transform.position.x)
            anim.FacingRight(true);
        else
            anim.FacingRight(false);
    }
}
