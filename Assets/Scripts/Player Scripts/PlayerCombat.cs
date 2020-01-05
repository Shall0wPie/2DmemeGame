using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private BoxCollider2D punchArea;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private float punchForce = 10f;
    [SerializeField] private float punchDmg = 10f;
    [Range(0f, 2f)] public float punchCooldown;

    private ContactFilter2D contactFilter;
    private Collider2D[] colliders;

    // Start is called before the first frame update
    void Start()
    {
        contactFilter = new ContactFilter2D();
        contactFilter.layerMask = enemyLayer;
        contactFilter.useLayerMask = true;

        colliders = new Collider2D[100];
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
}
