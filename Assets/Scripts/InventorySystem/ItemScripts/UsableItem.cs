using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UsableItem", menuName = "Items/UsableItem", order = 1)]
public class UsableItem : Item, IUsable
{
    public List<Effect> effects;
    public void Use(Transform target)
    {
        foreach (Effect effect in effects)
        {
            effect.ApplyEffect(target, this);
        }
    }
}