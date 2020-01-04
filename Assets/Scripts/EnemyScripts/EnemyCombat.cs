using System.Collections;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{

    private Rigidbody2D rb;
    private EnemyStats stats;
    public EnemyAnimationControl anim;
    public float hp { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        stats = GetComponentInParent<EnemyStats>();
        hp = stats.maxHP;
        rb = GetComponentInParent<Rigidbody2D>();
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

        // Disables Follow script
        GetComponentInParent<EnemyFollow>().enabled = false;

        // Rotate model by 90 degrees
        transform.parent.Rotate(0, 0, -90 * transform.parent.lossyScale.x);
        // Set all colliders as Triggers to avoid any collisions
        var colliders = GetComponentsInParent<Collider2D>();
        foreach(Collider2D single in colliders)
        {
            single.isTrigger = true;
        }

        // The rest of function will continue as deathDuration passes
        yield return new WaitForSeconds(stats.deathDuration);
        
        // Destroys parent object (the entire Enemy object)
        Destroy(transform.parent.gameObject);
    }
}
