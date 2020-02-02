using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfCombat : EnemyCombat
{
    
    protected override void Update()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        Vector2 xToTarget = new Vector2(target.position.x, transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, xToTarget, stats.speed * Time.deltaTime);

        StartCoroutine(Run());
    }

    public virtual IEnumerator Run()
    {

        animControl.PlayDeath();

        while (true)
        {
            //force for enemy punch
            yield return null;
            //break;
            if (animControl.renderer.sprite.name.Equals("frame_4_delay-0.1s"))
            {
                //Vector2 force = new Vector2(punchForce.x * -transform.lossyScale.x, punchForce.y);
                //target.GetComponentInChildren<PlayerCombat>().ApplyHit(punchDmg, force);
                break;
            }

            //if (!animControl.anim.GetCurrentAnimatorStateInfo(0).IsName("WolfRun"))
              //  break;
        }
    }
}


   

