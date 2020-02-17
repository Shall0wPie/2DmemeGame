using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimaCombat : EnemyCombat
{
    [SerializeField] private float rollDuration;

    private bool isInCharge = false;

    protected override void Update()
    {
        float spotDistance = Vector2.Distance(target.position, stats.aggroPoint.position);
        float distance = Vector2.Distance(target.position, transform.position);
        //Hit player
        if (!isInCharge && (spotDistance < stats.aggroRange || distance < attackRange))
        {
            Charge();
        }
    }

    private void Charge()
    {
        Vector2 scale = transform.localScale;
        if (target.position.x > transform.position.x)
            scale.x = -1;
        else
            scale.x = 1;

        transform.localScale = scale;

        animControl.PlayAttack();
        isInCharge = true;
    }

    public void StarRolling()
    {
        StartCoroutine(Roll());
    }

    public void StopRolling()
    {
        isInCharge = false;
    }

    private IEnumerator Roll()
    {
        animControl.anim.SetBool("IsRolling", true);

        float duration = Time.time + rollDuration;
        float direction;

        if (target.position.x > transform.position.x)
            direction = 1;
        else
            direction = -1;

        Vector2 velocity = new Vector2(stats.speed * direction, 0);
        rb.velocity = velocity;

        while (duration > Time.time)
        {
            if (stats.bodyCollider.OverlapPoint(target.position))
            {
                Vector2 force = new Vector2(punchForce.x * -transform.lossyScale.x, punchForce.y);
                target.GetComponentInChildren<PlayerCombat>().ApplyHit(punchDmg, force, stunDuration);
            }
            yield return null;
        }
        yield return new WaitForSeconds(rollDuration);

        //rb.velocity = Vector2.zero;

        animControl.anim.SetBool("IsRolling", false);
    }
}
