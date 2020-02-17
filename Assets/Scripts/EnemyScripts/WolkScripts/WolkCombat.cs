using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolkCombat : EnemyCombat
{
    public Transform[] dawgPoints;
    public Transform[] tpPoints;

    [SerializeField] private GameObject platforms;
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpHeight;
    private float distance;
    private Vector2 spawnpoint;


    protected override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            //animControl.PlaySummon();
            animControl.anim.SetTrigger("Summon");
        }

        // + Platform controll
        if (Input.GetKeyDown(KeyCode.X))
        {
            animControl.PlayTransformationToWolf();

            Animator[] anims = platforms.GetComponentsInChildren<Animator>();
            foreach (Animator anim in anims)
                anim.SetBool("IsUp", false);
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            animControl.PlayAttack();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            animControl.PlayTransformationFromWolf();

            Animator[] anims = platforms.GetComponentsInChildren<Animator>();
            foreach (Animator anim in anims)
                anim.SetBool("IsUp", true);
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            animControl.PlayFlightRotation();
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            animControl.PlayFlightBackRotation();
        }
    }

    public override IEnumerator Attack()
    {
        BoxCollider2D targetCollider = target.GetComponent<PlayerStats>().bodyCollider;
        Vector2 dir = target.position - transform.position;
        dir.Normalize();
        dir.y += jumpHeight;
        rb.velocity = dir.normalized * jumpForce;

        while (animControl.anim.GetBool("IsOnFly"))
        {
            if (stats.bodyCollider.IsTouching(targetCollider))
            {
                target.GetComponentInChildren<PlayerCombat>().ApplyHit(punchDmg, rb.velocity, stunDuration);
                break;
            }
            yield return null;
        }
        yield return null;
    }
    public override IEnumerator Kill()
    {
        stats.isAlive = false;
        // Plays anim
        animControl.PlayDeath();

        LootTable lootTable = GetComponent<LootTable>();
        if (lootTable != null)
        {
            lootTable.SpawnLoot();
        }

        // Sets body collider as Triggers to avoid any collisions
        stats.bodyCollider.isTrigger = true;
        // Disables Follow script
        EnemyControl control = GetComponentInParent<EnemyControl>();
        if (control != null)
            control.enabled = false;
        
        GetComponentInParent<DialogueControl>().TriggerDialogue("Imposible");

        // The rest of function will continue as deathDuration passes
        yield return new WaitForSeconds(stats.deathDuration);

        // Respawns Enemy in its Spawn Point
        if (stats.allowRespawn)
            Respawn();
        else
            Destroy(transform.parent.gameObject);
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < dawgPoints.Length; i++)
        {
            Gizmos.DrawWireSphere(dawgPoints[i].position, 3);

            for (int k = i; k < dawgPoints.Length; k++)
            {
                if (k < 5 && i < 5)
                    Gizmos.color = Color.red;
                else
                    Gizmos.color = Color.white;
                Gizmos.DrawLine(dawgPoints[i].position, dawgPoints[k].position);
            }
        }
    }
}