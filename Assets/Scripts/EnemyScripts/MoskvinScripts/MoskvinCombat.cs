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
    [SerializeField] protected float tamponRange = 1f;

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

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, TpRange);
    }
}
