using UnityEngine;

public class EnemyAnimationControl : MonoBehaviour
{
    public Animator anim;
    private Rigidbody2D rb;
    private EnemyFollow enemyFollow;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();
        enemyFollow = GetComponentInParent<EnemyFollow>();
    }

    public void PlayMove()
    {
        anim.SetFloat("Speed", 1f);
    }

    public void PlayStand()
    {
        anim.SetFloat("Speed", 0f);
    }

    public void PlayHit()
    {
        anim.SetTrigger("Hit");
    }

    public void PlayDeth()
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
