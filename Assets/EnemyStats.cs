using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [Header("Combat stats")]
    public float maxHP = 10f;
    [Range(0f, 1f)] public float dmgResistance = 0f;
    [Range(0f, 1f)] public float forceResistance = 0f;
    public float deathDuration = 2f;
    [Space]
    [Header("Move stats")]
    public float speed = 40f;
}
