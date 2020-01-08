using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private Vector2 strikeForce;
    [SerializeField] private float speed;
    [SerializeField] private float flyDistance;
    Rigidbody2D rb;
    Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();

        Vector2 velosity = new Vector2(speed * transform.localScale.x, 0);
        rb.velocity = velosity;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.position);
        if (distance > flyDistance)
            Destroy(gameObject);
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
