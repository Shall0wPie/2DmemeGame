using System.Collections;
using UnityEngine;

public class EnemyAnimationControl : MonoBehaviour
{
    private Rigidbody2D rb;
    private EnemyControl enemyFollow;
    public Animator anim;
    public SpriteRenderer renderer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();
        enemyFollow = GetComponentInParent<EnemyControl>();
        if (anim == null)
            anim = GetComponent<Animator>();
        if (renderer == null)
            renderer = GetComponent<SpriteRenderer>();
    }

    public void PlayMove()
    {
        anim.SetFloat("Speed", 1f);
    }

    public void PlayStand()
    {
        anim.SetFloat("Speed", 0f);
    }

    public void PlayDeth()
    {
        anim.SetBool("IsDead", true);
    }

    public void PlayRespawn()
    {
        anim.SetBool("IsDead", false);
    }

    public void PlayAttack()
    {
        anim.SetTrigger("Attack"); 
    }

    public IEnumerator Hitted(SpriteRenderer Anim)
    {
        Anim.color = new Color(0.95f, 0.76f, 0.79f);
        yield return new WaitForSeconds(0.3f);
        Anim.color = new Color(1, 1, 1);
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
