using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private BoxCollider2D punchArea;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private float punchForce = 10f;
    [SerializeField] private float punchDmg = 10f;
    [Range(0f, 2f)] public float punchCooldown;

    public float hp { get; private set; }
    private Rigidbody2D rb;
    private PlayerStats stats;
    private ContactFilter2D contactFilter;
    private Collider2D[] colliders;

    // Start is called before the first frame update
    void Start()
    {
        contactFilter = new ContactFilter2D();
        contactFilter.layerMask = enemyLayer;
        contactFilter.useLayerMask = true;
        stats = GetComponentInParent<PlayerStats>();
        colliders = new Collider2D[100];
        rb = GetComponentInParent<Rigidbody2D>();

    }

    // Update is called once per frame
    public void Punch()
    {
        // Quantity of collides overlaped by Player Hit collider
        int quantity = punchArea.OverlapCollider(contactFilter, colliders);

        // If there are any colliders Applies Force and Damage to collider Owner
        if (quantity != 0)
        {
            // Creates Force depending on character punchForce on the X axis only
            Vector2 force = new Vector2(transform.lossyScale.x * punchForce, 0);
            for (int i = 0; i < quantity; i++)
            {
                // Applies Damage and Force to every overlaped enemy collider
                colliders[i].GetComponentInChildren<EnemyCombat>().ApplyHit(punchDmg, force);
            }
        }
    }

    public void ApplyHit(float dmg, Vector2 force)
    {
        // Calculates dmg and force according to resistance
        dmg *= (1 - stats.dmgResistance);
        force *= (1 - stats.forceResistance);
        // Applies effects

        rb.velocity += force;
        hp -= dmg;
        Debug.Log("Hp: " + hp + " Dmg: " + dmg);

        // Plays hit animation
        //anim.PlayHit();

        // If hp bellow or equal to zero Kills this Enemy
        //if (hp <= 0)
        {
            // Coriutine is function that lasts for some time (not only one Game circle)
            //StartCoroutine(Kill());
        }
    }
}
