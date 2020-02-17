using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PashtetCombat : EnemyCombat
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

            if ((animControl.renderer.sprite.name.Equals("PashtetAttack3")) && (distance < attackRange))
            {
                Vector2 force = new Vector2(punchForce.x * -transform.lossyScale.x, punchForce.y);
                target.GetComponentInChildren<PlayerCombat>().ApplyHit(punchDmg, force, stunDuration);
                break;
            }

            if (!animControl.anim.GetCurrentAnimatorStateInfo(0).IsName("PashtetAttacking"))
                break;
        }
    }

    private void DropItems()
    {
        LootTable table = GetComponent<LootTable>();
        Destroy(table);
    }
}
