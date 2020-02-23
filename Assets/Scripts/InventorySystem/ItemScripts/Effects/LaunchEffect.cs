using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LaunchEffect", menuName = "Effects/LaunchEffect", order = 1)]
public class LaunchEffect : Effect
{
    [SerializeField] private Transform projectile;

    public override void ApplyEffect(Transform target, Item item)
    {
        target.GetComponentInChildren<PlayerAnimationControl>().PlayAblility();
        // audiosrc = GameObject.FindGameObjectWithTag("ItemSpell").GetComponent<AudioSource>();    
        // audiosrc.Play();
        Transform newProjectile = projectile;
        newProjectile.GetComponent<Projectile>().caster = target;
        newProjectile.localScale = target.lossyScale;
        newProjectile = Instantiate(newProjectile, target.position, Quaternion.identity);
        newProjectile.GetComponent<AudioSource>().Play();
    }
}
