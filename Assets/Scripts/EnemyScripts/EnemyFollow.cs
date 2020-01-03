using System.Collections;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    private Transform target;
    private new Rigidbody2D rigidbody;

    [SerializeField] public float speed = 40f;
    [SerializeField] private Collider2D stopCollider = null;
    [SerializeField] private Collider2D agroArea = null;
    [SerializeField][Range(0, 5f)] private float stopRadius = 2f;
    

    private Vector2 currentVelocity = Vector2.zero;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Debug.DrawRay(transform.position, Vector2.right * stopRadius);
        if (agroArea.OverlapPoint(target.position) && !stopCollider.OverlapPoint(target.position))
        {
            float x;
            if (target.position.x - stopRadius > transform.position.x)
                x = 1f;
            else if (target.position.x + stopRadius < transform.position.x)
                x = -1f;
            else
                x = 0;

            rigidbody.velocity = new Vector2(x * speed * Time.deltaTime, rigidbody.velocity.y);
        }
        else
            rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);
    }
}
