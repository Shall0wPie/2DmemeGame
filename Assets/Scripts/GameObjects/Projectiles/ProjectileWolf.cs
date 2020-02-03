using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWolf : Projectile
{
    private void Start()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
    // Start is called before the first frame update
    void Update()
    {
        CheckForOutrange();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        
        if (collision.tag == "Player")
        {
            Vector2 force = new Vector2(strikeForce.x * -caster.lossyScale.x, strikeForce.y);
            collision.GetComponentInChildren<PlayerCombat>().ApplyHit(damage, force);
            Destroy(gameObject);
        }
    }
}
