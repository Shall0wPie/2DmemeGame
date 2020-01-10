using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prefabs : MonoBehaviour
{
    public static Prefabs instance { get; private set; }

    private void Awake()
    {
        instance = this;
    }

    public Transform item;
    public Transform point;
    public Transform projectileAnime;
    public Transform projectileTampon;
}
