﻿using System.Collections;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private BoxCollider2D punchArea;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private float punchForce = 10f;
    [SerializeField] private float punchDmg = 10f;
    [Range(0f, 2f)] public float punchCooldown;

    public bool isInvincible = false;
    public float hp;
    private Rigidbody2D rb;
    private PlayerStats stats;
    public PlayerAnimationControl PlayerAnim;
    private PlayerMovement movement;
    private ContactFilter2D contactFilter;
    private Collider2D[] colliders;


    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponentInParent<PlayerMovement>();
        contactFilter = new ContactFilter2D();
        contactFilter.layerMask = enemyLayer;
        contactFilter.useLayerMask = true;
        stats = GetComponentInParent<PlayerStats>();
        colliders = new Collider2D[100];
        rb = GetComponentInParent<Rigidbody2D>();
        hp = stats.maxHP;
    }

    // Update is called once per frame
    public void Punch()
    {
        // Quantity of collides overlaped by Player Hit collider
        int quantity = punchArea.OverlapCollider(contactFilter, colliders);

        // If there are any colliders Applies Force and Damage to collider Owner
        if (quantity != 0)
        {
            // Creates Force depending on character punchForce on the X axis only
            Vector2 force = new Vector2(transform.lossyScale.x * punchForce, 0);
            for (int i = 0; i < quantity; i++)
            {
                // Applies Damage and Force to every overlaped enemy collider
                colliders[i].GetComponentInChildren<EnemyCombat>().ApplyHit(punchDmg, force);
            }
        }
    }

    public void Shoot()
    {
        PlayerAnim.PlayAblility();
        Transform projectile = Prefabs.instance.projectileAnime;
        projectile.localScale = transform.lossyScale;
        projectile.GetComponent<Projectile>().caster = transform;
        Quaternion q = new Quaternion();

        Vector2 dir = new Vector2(0, 0) - (Vector2) transform.position;

        q.SetFromToRotation(Vector2.up, dir);
        projectile = Instantiate(projectile, transform.position, q);
        projectile.GetComponent<ProjectileAnime>().SetVelocityDirection(dir);
    }

    public void ApplyHit(float dmg, Vector2 force, float stunTime)
    {
        if (!isInvincible)
        {
            // Calculates dmg and force according to resistance
            dmg *= (1 - stats.dmgResistance);
            force *= (1 - stats.forceResistance);
            // Applies effects
            if (stunTime > 0)
                StartCoroutine(movement.Stun(stunTime));
            rb.velocity += force;
            hp -= dmg;
            //Debug.Log("Hp: " + hp + " Dmg: " + dmg);

            // If hp bellow or equal to zero Kills this Enemy
            if (hp <= 0 && stats.isAlive)
            {
                StartCoroutine(Kill());
            }
        }
    }


    public IEnumerator Kill()
    {
        // Plays anim
        GetComponentInParent<PlayerControl>().enabled = false;
        stats.isAlive = false;
        GetComponentInParent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        PlayerAnim.PlayDeath();

        // The rest of function will continue as deathDuration passes
        yield return new WaitForSeconds(stats.deathDuration);

        if (stats.allowRespawn)
        {
            LevelLoader.ReloadScene();
            PlayerAnim.PlayRespawn();
            GetComponentInParent<PlayerControl>().enabled = true;
            GetComponentInParent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            transform.parent.position = stats.spawnPoint;
            hp = stats.maxHP;
            //stats.isAlive = true;
        }
    }
}