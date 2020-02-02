using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkCombat : EnemyCombat
{
    private float distance;
    Vector2 spawnpoint;
    private new Rigidbody2D rigidbody;
    public GameObject wolf;

    protected override void Update()
    {              
        if (Input.GetKeyDown(KeyCode.Z))
        {
            //animControl.PlaySummon();
            StartCoroutine(SummonWolf());
            
        }
    }

    public IEnumerator SummonWolf()
    {
        


        while (true)
        {
            //force for enemy punch
            spawnpoint = new Vector2(transform.position.x, transform.position.y);
            yield return null;
            Instantiate(wolf, spawnpoint, Quaternion.identity);


            //distance = Vector2.Distance(target.position, transform.position);
            
            //    Vector2 dir = target.position - transform.position;
            //    q.SetFromToRotation(Vector2.up, dir);
            //    projectile = Instantiate(projectile, transform.position, q);
            //    projectile.GetComponent<Projectile>().SetVelocityDirection(dir);
                break;
           
        }
    }
}
