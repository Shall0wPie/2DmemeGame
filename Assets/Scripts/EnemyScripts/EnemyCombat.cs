using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    [SerializeField] float maxHp = 200;
    [SerializeField] [Range(0f, 1f)] float dmgResistance = 0f;
    [SerializeField] [Range(0f, 1f)] float forceResistance = 0f;
    private Rigidbody2D rb;
    public EnemyAnimationControl anim;
    public float hp { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        hp = maxHp;
        rb = GetComponentInParent<Rigidbody2D>();
    }

    // Update is called once per frame
    public void ApplyHit(float dmg, Vector2 force)
    {
        dmg *= (1 - dmgResistance);
        force *= (1 - forceResistance);
        rb.velocity += force;
        hp -= dmg;
        Debug.Log("Hp: " + hp + " Dmg: " + dmg);
    }
}
