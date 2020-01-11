using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootTable : MonoBehaviour
{
    [SerializeField] private Loot[] loot;

    private void OnDestroy()
    {
        foreach (Loot item in loot)
        {
            float roll = Random.Range(0f, 1f);

            if (roll <= item.DropChance)
            {
                Transform newTr = Item.SpawnItem(item.Item, transform.position);
                Vector2 vel = new Vector2(Random.Range(-10, 10), Random.Range(0, 5));
                newTr.GetComponent<Rigidbody2D>().velocity = vel;
            }
        }
    }
}
