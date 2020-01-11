using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PashtetCombat : EnemyCombat
{
    public override IEnumerator Attack(float dmg, Vector2 force)
    {
        float distance;
        animControl.PlayAttack();

        while (true)
        {
            //force for enemy punch
            yield return null;
            distance = Vector2.Distance(target.position, transform.position);

            if ((animControl.renderer.sprite.name.Equals("PashtetAttack3")) && (distance < stats.attackRange))
            {
                force = new Vector2(force.x * -transform.lossyScale.x, force.y);
                target.GetComponentInChildren<PlayerCombat>().ApplyHit(dmg, force);
                break;
            }

            if (!animControl.anim.GetCurrentAnimatorStateInfo(0).IsName("PashtetAttacking"))
                break;
        }
    }

    public override IEnumerator Kill()
    {
        // Plays anim
        animControl.PlayDeath();

        // Sets body collider as Triggers to avoid any collisions
        stats.bodyCollider.isTrigger = true;
        // Disables Follow script
        GetComponentInParent<EnemyControl>().enabled = false;

        // Rotates model by 90 degrees
        transform.parent.Rotate(0, 0, -90 * transform.parent.lossyScale.x);

        // The rest of function will continue as deathDuration passes
        yield return new WaitForSeconds(stats.deathDuration);

        // Respawns Enemy in its Spawn Point
        if (stats.allowRespawn)
            Respawn();
        // Destroys parent object (the entire Enemy object)
        else
            Destroy(transform.parent.gameObject);
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
        if (hp <= 0)
        {
            // Coriutine is function that lasts for some time (not only one Game circle)
            StartCoroutine(Kill());
            DropItems();
        }
    }

    public override void Respawn()
    {
        // Revert Kill effects
        animControl.PlayRespawn();
        hp = stats.maxHP;
        rb.velocity = Vector2.zero;
        stats.bodyCollider.isTrigger = false;
        GetComponentInParent<EnemyControl>().enabled = true;
        transform.parent.Rotate(0, 0, 90 * transform.parent.lossyScale.x);

        transform.parent.position = stats.spawnPoint;
    }

    private void DropItems()
    {
        LootTable table = GetComponent<LootTable>();
        Destroy(table);
    }
}
