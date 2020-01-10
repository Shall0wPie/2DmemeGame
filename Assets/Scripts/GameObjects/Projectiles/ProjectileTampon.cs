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
            collision.GetComponentInChildren<PlayerCombat>().ApplyHit(damage, strikeForce);
            Destroy(gameObject);
        }
    }
}
