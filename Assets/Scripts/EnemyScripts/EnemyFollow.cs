using System.Collections;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    private Transform target;
    private EnemyStats stats;
    private new Rigidbody2D rigidbody;

    public EnemyAnimationControl anim;

    [SerializeField] private Collider2D stopCollider = null;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        stats = GetComponent<EnemyStats>();
        rigidbody = GetComponent<Rigidbody2D>();
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
            if (distance < stats.attackRange)
        {
            anim.PlayAttack();
        }



    }
}
