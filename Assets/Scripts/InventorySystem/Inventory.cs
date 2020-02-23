using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<Container> containers;

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
            GameObject newItem = Instantiate(SceneItemTemplate, transform.position, Quaternion.identity);
            newItem.GetComponentInChildren<ItemPickup>().Init(item);
            newItem.GetComponent<Rigidbody2D>().velocity = velocity;
        }
    }
}