using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private BoxCollider2D punchArea;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private float punchForce = 10f;
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
        int quantity = punchArea.OverlapCollider(contactFilter, colliders);

        if (quantity != 0)
        {
            Vector2 force = new Vector2(transform.lossyScale.x * punchForce, 0);
            for (int i = 0; i < quantity; i++)
            {
                colliders[i].attachedRigidbody.AddForce(force);
            }
        }
    }
}
