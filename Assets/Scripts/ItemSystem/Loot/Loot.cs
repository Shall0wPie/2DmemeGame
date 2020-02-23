using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Loot
{
    [SerializeField] private AssetItem item;
    [SerializeField] private float dropChance;

    public AssetItem Item { get => item; }
    public float DropChance { get => dropChance; }
}
