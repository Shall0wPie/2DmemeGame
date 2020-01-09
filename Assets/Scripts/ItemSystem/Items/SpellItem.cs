using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpellItem", menuName = "Items/SpellItem", order = 1)]
public class SpellItem : Item
{
    public override void Use(Transform caster)
    {
        caster.GetComponentInChildren<PlayerAnimationControl>().PlayAblility();
        Transform projectile = Prefabs.instance.projectile;
        projectile.localScale = caster.lossyScale;
        Instantiate(projectile, caster.position, Quaternion.identity);
    }
}
