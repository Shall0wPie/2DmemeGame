using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class MaximCombat : EnemyCombat
{
    AudioSource audioMaxim;
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
    public override void ApplyHit(float dmg, Vector2 force)
    {
        // Calculates dmg and force according to resistance
        dmg *= (1 - stats.dmgResistance);
        force *= (1 - stats.forceResistance);
        // Applies effects
        rb.velocity += force;
        hp -= dmg;
        // Sprites
        StartCoroutine(animControl.Hitted(animControl.renderer));
        // If hp bellow or equal to zero Kills this Enemy
        if (hp <= 0 && stats.isAlive)
        {
            audioMaxim = GetComponentInParent<AudioSource>();
            audioMaxim.Play();
            // Coriutine is function that lasts for some time (not only one Game circle)
            StopAllCoroutines();
            StartCoroutine(Kill());
        }
    }
}