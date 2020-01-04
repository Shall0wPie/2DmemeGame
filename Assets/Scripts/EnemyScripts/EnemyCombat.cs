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

    // Update is called once per frame
    public void ApplyHit(float dmg, Vector2 force)
    {
        dmg *= (1 - stats.dmgResistance);
        force *= (1 - stats.forceResistance);
        rb.velocity += force;
        hp -= dmg;
        Debug.Log("Hp: " + hp + " Dmg: " + dmg);
        anim.PlayHit();

        if (hp <= 0)
        {
            StartCoroutine(Kill());
        }
    }

    public IEnumerator Kill()
    {
        anim.PlayDeth();

        GetComponentInParent<EnemyFollow>().enabled = false;
        rb.freezeRotation = false;
        transform.parent.Rotate(0, 0, -90 * transform.parent.lossyScale.x);
        var colliders = GetComponentsInParent<Collider2D>();
        foreach(Collider2D single in colliders)
        {
            single.isTrigger = true;
        }

        yield return new WaitForSeconds(stats.deathDuration);
        
        Destroy(transform.parent.gameObject);
    }
}
