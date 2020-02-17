using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTampon : Projectile
{
    void Update()
    {
        CheckForOutrange();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Vector2 force = new Vector2(strikeForce.x * -caster.lossyScale.x, strikeForce.y);
            collision.GetComponentInChildren<PlayerCombat>().ApplyHit(damage, force, 0);
            Destroy(gameObject);
        }
    }
}
