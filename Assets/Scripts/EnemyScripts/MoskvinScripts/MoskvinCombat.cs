using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoskvinCombat : EnemyCombat
{
    public Transform[] point;
    private float distance;
    [Range(0f, 10f)] public float DelayTp;
    [Range(0f, 10f)] public float TpRange;
    private bool isInRange = false;
    private int prevPoint = -1;

    private float tamponTimeStamp = 0;
    [SerializeField] protected float tamponCooldown = 1f;
    [SerializeField] [Range(0f, 100f)] protected float tamponRange = 1f;

    protected override void FixedUpdate()
    {
        if ((DialogManager.instance.isInDialogue == false))
        {
            distance = Vector2.Distance(target.position, transform.position);
            if (distance < TpRange && !isInRange)
            {
                int pos = Random.Range(0, 7);

                while (pos == prevPoint)
                {
                    pos = Random.Range(0, 7);
                }
                prevPoint = pos;
                StartCoroutine(ITeleport(pos));
            }

            if (tamponTimeStamp <= Time.time && distance < tamponRange)
            {
                ShootTampon();
                tamponTimeStamp = Time.time + tamponCooldown;
            }
        }
    }

    private IEnumerator ITeleport(int pos)
    {
        isInRange = true;
        yield return new WaitForSeconds(DelayTp);
        
        transform.parent.position = point[pos].position;
        isInRange = false;
    }

    public void ShootTampon()
    { 
        Transform projectile = Prefabs.instance.projectileTampon;
        projectile.localScale = transform.lossyScale;
        projectile.GetComponent<Projectile>().caster = transform;
        Quaternion q = new Quaternion();

        Vector2 dir = target.position - transform.position;

        q.SetFromToRotation(Vector2.up, dir);
        projectile = Instantiate(projectile, transform.position, q);
        projectile.GetComponent<Projectile>().SetVelocityDirection(dir);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, TpRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, tamponRange);
    }
}
