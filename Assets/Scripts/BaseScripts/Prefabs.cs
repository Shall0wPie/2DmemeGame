using UnityEngine;

public class Prefabs : MonoBehaviour
{
    public static Prefabs instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
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
