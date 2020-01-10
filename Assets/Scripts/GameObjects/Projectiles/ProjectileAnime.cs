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
            collision.GetComponentInChildren<EnemyCombat>().ApplyHit(damage, strikeForce);
            Destroy(gameObject);
        }
    }
}
