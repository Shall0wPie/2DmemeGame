using System.Collections;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    private Transform target;
    private new Rigidbody2D rigidbody;

    public EnemyAnimationControl anim;

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
        if (agroArea.OverlapPoint(target.position) && !stopCollider.OverlapPoint(target.position))
        {
            Vector2 xNormolized = new Vector2(target.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, xNormolized, speed * Time.deltaTime);

            anim.PlayMove();
            if (target.position.x > transform.position.x)
                anim.FacingRight(true);
            else
                anim.FacingRight(false);
        }
        else
            anim.PlayStand();
    }
}
