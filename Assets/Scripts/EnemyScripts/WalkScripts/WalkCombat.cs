using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkCombat : EnemyCombat
{
    [SerializeField] private GameObject platforms;
    private new Rigidbody2D rigidbody;
    private float distance;
    Vector2 spawnpoint;


    protected override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            //animControl.PlaySummon();
            SummonWolf();
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

    public void SummonWolf()
    {
        Transform projectile = Prefabs.instance.projectileWolf;
        projectile.localScale = transform.lossyScale;
        projectile.GetComponent<Projectile>().caster = transform;

        Quaternion q = new Quaternion();
        distance = Vector2.Distance(target.position, transform.position);

        Vector2 dir = target.position - transform.position;
        q.SetFromToRotation(Vector2.left, dir);

        projectile = Instantiate(projectile, transform.position, q);
        projectile.GetComponent<Projectile>().SetVelocityDirection(dir);
    }
}
