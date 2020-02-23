using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] public List<Container> containers;

    [Space] [Header("Templates")]
    [SerializeField] private GameObject SceneItemTemplate;

    // Finds free scpace in all containers and adds item to it 
    public bool AddItem(Item item)
    {
        foreach (Container container in containers)
        {
            if (container.AddToContainer(item))
            {
                return true;
            }
        }

        return false;
    }

    // Drops items on the ground launched with given velocity
    public void DropItem(Item item, int dropAmount, Vector2 velocity)
    {
        for (int i = 0; i < dropAmount; i++)
        {
            Item.SpawnItem(item, transform.position, velocity);
        }
    }
}