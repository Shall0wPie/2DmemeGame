using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prefabs : MonoBehaviour
{
    public static Prefabs instance { get; private set; }

    private void Start()
    {
        if (instance != null)
        {
            Debug.Log("Prefabs already exists");
            Destroy(gameObject);
        }
        else
            instance = this;
    }

    public Transform item;
    public Transform point;
    public Transform projectileAnime;
    public Transform projectileTampon;
}
