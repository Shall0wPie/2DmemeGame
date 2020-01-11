using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class MaximCombat : EnemyCombat
{
    public override IEnumerator Attack()
    {
        float distance;
        animControl.PlayAttack();

        while (true)
        {
            //force for enemy punch
            yield return null;
            distance = Vector2.Distance(target.position, transform.position);
            
            if ((animControl.renderer.sprite.name.Equals("MaximAttack")) && (distance < attackRange))
            {
                Vector2 force = new Vector2(punchForce.x * -transform.lossyScale.x, punchForce.y);
                target.GetComponentInChildren<PlayerCombat>().ApplyHit(punchDmg, force);
                break;
            }

            if (!animControl.anim.GetCurrentAnimatorStateInfo(0).IsName("MaximAttack"))
                break;
        }
    }
}