using System.Collections;
using UnityEngine;

public abstract class EnemyCombat : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected Transform target;
    protected EnemyStats stats;
    public EnemyAnimationControl animControl;

    public float hp { get; protected set; }
    [SerializeField] protected float punchDmg = 25f;
    [SerializeField] protected Vector2 punchForce;

    [SerializeField] protected float attackCooldown = 1f;
    [SerializeField] [Range(0f, 10f)] public float attackRange;
    protected float attackTimeStamp = 0;


    protected virtual void Start()
    {
        stats = GetComponentInParent<EnemyStats>();
        hp = stats.maxHP;
        rb = GetComponentInParent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    protected virtual void Update()
    {
        float distance = Vector2.Distance(target.position, transform.position);
        //Hit player
        if (attackTimeStamp <= Time.time && distance < attackRange)
        {
            StartCoroutine(Attack());
            attackTimeStamp = Time.time + attackCooldown;
        }
    }
    //COROUTINE ATTACK
    public virtual IEnumerator Attack()
    {
        float distance;
        animControl.PlayAttack();
        
        while (true)
        {
            //force for enemy punch
            yield return null;
            distance = Vector2.Distance(target.position, transform.position);
            if ((animControl.renderer.sprite.name.Equals("PashtetAttack3")) && (distance < attackRange))
            {
                Vector2 force = new Vector2(punchForce.x * -transform.lossyScale.x, punchForce.y);
                target.GetComponentInChildren<PlayerCombat>().ApplyHit(punchDmg, force);
                break;
            }

            if (!animControl.anim.GetCurrentAnimatorStateInfo(0).IsName("PashtetAttacking"))
                break;
        }
    }

    // Applis Force and Damage to this Enemy
    public virtual void ApplyHit(float dmg, Vector2 force)
    {
        // Calculates dmg and force according to resistance
        dmg *= (1 - stats.dmgResistance);
        force *= (1 - stats.forceResistance);
        // Applies effects
        rb.velocity += force;
        hp -= dmg;
        // Sprites
        StartCoroutine(animControl.Hitted(animControl.renderer));
        // If hp bellow or equal to zero Kills this Enemy
        if (hp <= 0)
        {
            // Coriutine is function that lasts for some time (not only one Game circle)
            StopAllCoroutines();
            StartCoroutine(Kill());
        }
    }

    // Kill function plays Death anim and disable stuff lol
    public virtual IEnumerator Kill()
    {
        // Plays anim
        animControl.PlayDeath();

        // Sets body collider as Triggers to avoid any collisions
        stats.bodyCollider.isTrigger = true;
        // Disables Follow script
        GetComponentInParent<EnemyControl>().enabled = false;

        // Rotates model by 90 degrees
        transform.parent.Rotate(0, 0, -90 * transform.parent.lossyScale.x);

        // The rest of function will continue as deathDuration passes
        yield return new WaitForSeconds(stats.deathDuration);

        // Respawns Enemy in its Spawn Point
        if (stats.allowRespawn)
            Respawn();
        // Destroys parent object (the entire Enemy object)
        else
            Destroy(transform.parent.gameObject);
    }

    public virtual void Respawn()
    {
        // Revert Kill effects
        animControl.PlayRespawn();
        hp = stats.maxHP;
        rb.velocity = Vector2.zero;
        stats.bodyCollider.isTrigger = false;
        GetComponentInParent<EnemyControl>().enabled = true;
        transform.parent.Rotate(0, 0, 90 * transform.parent.lossyScale.x);

        transform.parent.position = stats.spawnPoint;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
