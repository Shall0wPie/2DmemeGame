using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobAnimationControl : MonoBehaviour
{
    public Animator anim;
    private Rigidbody2D rb;
    private EnemyFollow enemyFollow;
    private bool facingRight = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();
        enemyFollow = GetComponentInParent<EnemyFollow>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Mathf.Abs(rb.velocity.x) >= enemyFollow.speed * Time.deltaTime * 0.8f)
            anim.SetFloat("Speed", 1f);
        else
            anim.SetFloat("Speed", 0f);

        // Model Flip (depends on move direction)
        if (rb.velocity.x < 0 && facingRight)
        {
            Flip();
        }
        else if (rb.velocity.x > 0 && !facingRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        facingRight = !facingRight;

        Vector2 scale = rb.transform.localScale;
        scale.x *= -1;
        rb.transform.localScale = scale;
    }
}
