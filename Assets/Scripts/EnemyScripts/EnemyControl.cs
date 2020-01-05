using System.Collections;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    private Transform target;
    private EnemyStats stats;
    private new Rigidbody2D rigidbody;
    private EnemyCombat combat;
    private float timeStamp = 0;

    public EnemyAnimationControl anim;

    [SerializeField] private Collider2D stopCollider = null;
    [SerializeField] private float punchForce = 15f;
    [SerializeField] private float punchForceForY = 10f;
    [SerializeField] private float punchDmg = 25f;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        stats = GetComponent<EnemyStats>();
        rigidbody = GetComponent<Rigidbody2D>();
        combat = GetComponentInChildren<EnemyCombat>();
    }

   

    void FixedUpdate()
    {
        float distance = Vector2.Distance(target.position, transform.position);
        if (distance < stats.aggroRange && !stopCollider.OverlapPoint(target.position))
        {
            Vector2 xNormolized = new Vector2(target.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, xNormolized, stats.speed * Time.deltaTime);

            anim.PlayMove();
            
            if (target.position.x > transform.position.x)
                anim.FacingRight(true);
            else
                anim.FacingRight(false);
        }
        else
            anim.PlayStand();

        //Hit player
            if (timeStamp <= Time.time && distance < stats.attackRange)
            {
            Vector2 force = new Vector2(target.lossyScale.x * punchForce, target.lossyScale.y * punchForceForY);
            combat.Attack(punchDmg, force);
                timeStamp = Time.time + stats.attackCooldown;
            }
    }
}
