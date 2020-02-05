using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWolf : Projectile
{
    private void Start()
    {
        Vector3 scale = transform.localScale;
        Vector3 rot = transform.eulerAngles;

        scale.x = 1f;
        if ((rot.z >= 90f && rot.z < 180f) || (rot.z >= 180 && rot.z < 270))
        {
            rot.z -= 180f;
            scale.x = -1f;
        }

        transform.localScale = scale;
        transform.eulerAngles = rot;
    }

    void Update()
    {
        CheckForOutrange();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Vector2 force = rb.velocity;
            collision.GetComponentInChildren<PlayerCombat>().ApplyHit(damage, force);
            Destroy(gameObject);
        }
    }
}
