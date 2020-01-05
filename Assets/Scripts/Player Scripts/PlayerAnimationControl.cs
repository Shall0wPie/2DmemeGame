using UnityEngine;

public class PlayerAnimationControl : MonoBehaviour
{
    public Animator anim;
    private PlayerControl pc;
    private bool facingRight = true;
    private bool firstAttack = true;

    private void Start()
    {
        pc = GetComponentInParent<PlayerControl>();
    }

    void FixedUpdate()
    {
        if (Mathf.Abs(pc.horizontalMove) >= 0.01f)
            anim.SetFloat("Horizontal Speed", 1f);
        else
            anim.SetFloat("Horizontal Speed", 0f);

        if (/*Mathf.Abs(rb.velocity.y) > 0.1f &&*/ !pc.movement.isOnGround)
            anim.SetFloat("Vertical Speed", 1f);
        else
            anim.SetFloat("Vertical Speed", 0f);

        // Model Flip (depends on move direction)
        if (pc.horizontalMove < 0 && facingRight)
        {
            Flip();
        }
        else if (pc.horizontalMove > 0 && !facingRight)
        {
            Flip();
        }

        PlayAttack();
    }

    // Flips the model
    void Flip()
    {
        facingRight = !facingRight;

        Vector2 scale = pc.transform.localScale;
        scale.x *= -1;
        pc.transform.localScale = scale;
    }

    void PlayAttack()
    {
        if (pc.attack && firstAttack)
        {
            firstAttack = !firstAttack;
            anim.SetTrigger("Attack");
        }
        else if (pc.attack && !firstAttack)
        {
            firstAttack = !firstAttack;
            anim.SetTrigger("Attack2");
        }
    }
}
