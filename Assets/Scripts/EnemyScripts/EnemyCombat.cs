using System.Collections;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    private Rigidbody2D rb;
    private Transform target;
    private EnemyStats stats;
    public PlayerMovement targetmov;
    public EnemyAnimationControl anim;
    private PlayerStats playerStats;
    public float hp { get; private set; }

    [SerializeField] private Collider2D stopCollider = null;

    // Start is called before the first frame update
    void Start()
    {
        stats = GetComponentInParent<EnemyStats>();
        hp = stats.maxHP;
        rb = GetComponentInParent<Rigidbody2D>();
        playerStats = GetComponentInParent<PlayerStats>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    public void Attack(float dmg, Vector2 force)
    {
        //dmg *= (1 - playerStats.dmgResistance);
        //force *= (1 - playerStats.forceResistance);
        //rb for player
        //targetmov.GetComponentInChildren<PlayerMovement>().smoothTime = 5f;
        force = new Vector2(force.x * -transform.lossyScale.x, force.y);
        target.GetComponentInChildren<PlayerCombat>().ApplyHit(dmg, force);
        //targetmov.GetComponentInChildren<PlayerMovement>().smoothTime = 0.05f;
        //zdes hz kak hp
        anim.PlayAttack();
    }

    // Applis Force and Damage to this Enemy
    public void ApplyHit(float dmg, Vector2 force)
    {
        // Calculates dmg and force according to resistance
        dmg *= (1 - stats.dmgResistance);
        force *= (1 - stats.forceResistance);
        // Applies effects
        rb.velocity += force;
        hp -= dmg;
        //Debug.Log("Hp: " + hp + " Dmg: " + dmg);

        // Plays hit animation
        anim.PlayHit();

        // If hp bellow or equal to zero Kills this Enemy
        if (hp <= 0)
        {
            // Coriutine is function that lasts for some time (not only one Game circle)
            StartCoroutine(Kill());
        }
    }

    // Kill function plays Death anim and disable stuff lol
    public IEnumerator Kill()
    {
        // Plays anim
        anim.PlayDeth();

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

    public void Respawn()
    {
        // Revert Kill effects
        anim.PlayRespawn();
        hp = stats.maxHP;
        rb.velocity = Vector2.zero;
        stats.bodyCollider.isTrigger = false;
        GetComponentInParent<EnemyControl>().enabled = true;
        transform.parent.Rotate(0, 0, 90 * transform.parent.lossyScale.x);

        transform.parent.position = stats.spawnPoint;
    }
}
