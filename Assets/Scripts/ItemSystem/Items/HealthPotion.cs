using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "HealthPotion", menuName = "Items/Potion", order = 1)]
public class HealthPotion : Item
{
    [SerializeField] private int health;

    public override void Use(Transform target)
    {
        quantity--;
        target.GetComponentInChildren<PlayerCombat>().hp += health;
    }
}
