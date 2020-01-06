using System.Collections;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    private Transform target;
    private EnemyStats stats;
    private EnemyCombat combat;
    private float timeStamp = 0;

    public EnemyAnimationControl anim;

    [SerializeField] private float punchDmg = 25f;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        stats = GetComponent<EnemyStats>();
        combat = GetComponentInChildren<EnemyCombat>();
    }



    void FixedUpdate()
    {
        float distanceFormSpot = Vector2.Distance(target.position, stats.aggroPoint.position);

        if (distanceFormSpot < stats.aggroRange)
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

        float distance = Vector2.Distance(target.position, transform.position);
        //Hit player
        if (timeStamp <= Time.time && distance < stats.attackRange)
        {
            StartCoroutine(combat.Attack(punchDmg, stats.punchForce));
            timeStamp = Time.time + stats.attackCooldown;
        }
    }

    void AnimateMove(Vector2 targetPos)
    {
        anim.PlayMove();

        if (targetPos.x > transform.position.x)
            anim.FacingRight(true);
        else
            anim.FacingRight(false);
    }
}
