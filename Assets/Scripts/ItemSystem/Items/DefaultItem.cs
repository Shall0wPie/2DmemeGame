using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DefaultItem", menuName = "Items/DefaultItem", order = 1)]
public class DefaultItem : Item
{
    // Start is called before the first frame update
    public override void Use(Transform target) { }
}
