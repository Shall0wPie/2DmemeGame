using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private Transform player;
    public Vector3 offset;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void FixedUpdate()
    {
        transform.position = player.position + offset;
    }
}
