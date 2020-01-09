using UnityEngine;

public class PlayerAnimationControl : MonoBehaviour
{
    public Animator anim;
    private Rigidbody2D rb;
    private bool facingRight = true;
    private bool firstAttack = true;

    private void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();
    }

    public void PlayMove()
    {
        anim.SetFloat("Horizontal Speed", 1f);
    }

    public void PlayStand()
    {
        anim.SetFloat("Horizontal Speed", 0f);
    }

    public void PlayJump(bool jump)
    {
        anim.SetBool("Jump", jump);
    }

    public void PlayAttack()
    {
        if (firstAttack)
        {
            firstAttack = !firstAttack;
            anim.SetTrigger("Attack");
        }
        else if (!firstAttack)
        {
            firstAttack = !firstAttack;
            anim.SetTrigger("Attack2");
        }
    }

    public void PlayAblility()
    {
        anim.SetTrigger("UseAbility");
    }
    public void PlayDeath()
    {
        
        anim.SetBool("IsDead", true);
    }

    public void PlayRespawn()
    {
        anim.SetBool("IsDead", false);
    }

    public void FacingRight(bool isFacingRight)
    {
        Vector2 scale = rb.transform.localScale;

        if (isFacingRight)
            scale.x = -1;
        else
            scale.x = 1;

        rb.transform.localScale = scale;
    }
}
