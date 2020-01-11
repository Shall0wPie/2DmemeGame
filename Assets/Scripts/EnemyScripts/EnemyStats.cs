using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    // List of Enemy stats
    [Header("World stats")]
    public Vector2 spawnPoint;
    public bool allowRespawn = false;
    public Collider2D bodyCollider;

    [Space]
    [Header("Combat stats")]
    public float maxHP = 10f;
    [Range(0f, 1f)] public float dmgResistance = 0f;
    [Range(0f, 1f)] public float forceResistance = 0f;
    public Transform aggroPoint;
    [Range(0f, 50f)] public float aggroRange = 10f;
    public float deathDuration = 2f;

    [Space]
    [Header("Move stats")]
    public float speed = 40f;

    private void Start()
    {
        spawnPoint = transform.position;

        if (aggroPoint == null)
        {
            //aggroPoint = GameObject.FindGameObjectWithTag("AggroPoints").transform;
            aggroPoint = Instantiate(Prefabs.instance.point, spawnPoint, Quaternion.identity, GameObject.FindGameObjectWithTag("AggroPoints").transform);
            //aggroPoint.position = spawnPoint;
        }
    }

    private void OnDrawGizmos()
    {
        // Draw aggro point and its range
        Gizmos.color = Color.green;
        if (aggroPoint != null)
            Gizmos.DrawWireSphere(aggroPoint.position, aggroRange);
        else if (spawnPoint != Vector2.zero)
            Gizmos.DrawWireSphere(spawnPoint, aggroRange);
        else
            Gizmos.DrawWireSphere(transform.position, aggroRange);
    }
}
