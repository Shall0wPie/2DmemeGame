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
        if (Input.GetKeyDown(KeyCode.X))
        {
            animControl.PlayTransformationToWolf();
            

        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            animControl.PlayAttack();

        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            animControl.PlayTransformationFromWolf();

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

    public IEnumerator SummonWolf()
    {
        Transform projectile = Prefabs.instance.projectileWolf;
        projectile.localScale = transform.lossyScale;
        projectile.GetComponent<Projectile>().caster = transform;

        Quaternion q = new Quaternion();


        while (true)
        {
            //force for enemy punch
            yield return null;
            distance = Vector2.Distance(target.position, transform.position);
            
            Vector2 dir = target.position - transform.position;
            
            
            projectile = Instantiate(projectile, transform.position, q);
            projectile.GetComponent<Projectile>().SetVelocityDirection(dir);
            break;

        }
    }
}
