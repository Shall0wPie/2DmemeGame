using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAnime : Projectile
{
    void Update()
    {
        CheckForOutrange();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Vector2 force = new Vector2(strikeForce.x * caster.lossyScale.x, strikeForce.y);
            collision.GetComponentInChildren<EnemyCombat>().ApplyHit(damage, force);
            Destroy(gameObject);
        }
    }
}
