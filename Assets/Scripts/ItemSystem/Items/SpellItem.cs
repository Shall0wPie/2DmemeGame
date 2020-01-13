using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpellItem", menuName = "Items/SpellItem", order = 1)]
public class SpellItem : Item
{
    private AudioSource audiosrc;
    public override void Use(Transform caster)
    {
        caster.GetComponentInChildren<PlayerAnimationControl>().PlayAblility();
        audiosrc = GameObject.FindGameObjectWithTag("ItemSpell").GetComponent<AudioSource>();    
            audiosrc.Play();
        Transform projectile = Prefabs.instance.projectileAnime;
        projectile.GetComponent<Projectile>().caster = caster;
        projectile.localScale = caster.lossyScale;
        Instantiate(projectile, caster.position, Quaternion.identity);
    }
}
