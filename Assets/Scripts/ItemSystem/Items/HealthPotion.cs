using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HealthPotion", menuName = "Items/Potion", order = 1)]
public class HealthPotion : Item
{
    [SerializeField] private int health;
    private AudioSource audiosrc;
    
    public override void Use(Transform target)
    {
        audiosrc = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
        
        audiosrc.Play();
        target.GetComponentInChildren<PlayerCombat>().hp += health;
    }
}
