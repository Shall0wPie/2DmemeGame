using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HealEffect", menuName = "Effects/HealEffect", order = 1)]
public class HealEffect : Effect
{
    [SerializeField] private int health;

    public override void ApplyEffect(Transform target, Item item)
    {
        AudioSource audioSrc = target.GetComponent<AudioSource>();
        if (audioSrc != null)
            audioSrc.Play();
        target.GetComponentInChildren<PlayerCombat>().hp += health;
    }
}
