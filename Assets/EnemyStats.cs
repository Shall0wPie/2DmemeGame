using System.Collections;
using System.Collections.Generic;
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
    [Range(0f, 25f)] public float aggroRange = 10f;
    public float deathDuration = 2f;

    [Space]
    [Header("Move stats")]
    public float speed = 40f;

    private void Start()
    {
        spawnPoint = transform.position;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, aggroRange);
    }
}
