using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapTrigger : MonoBehaviour
{
    [SerializeField] [Range(0f, 2f)] private float damageCooldown;
    [SerializeField] private float damage;

    [Space]
    [SerializeField] private Collider2D falseGround;
    [SerializeField] private Collider2D spikesColl;
    [SerializeField] private Animator anim;
    [SerializeField] private LayerMask playerLayer;
    private PlayerCombat player;
    private bool isTriggerd = false;
    private float dmgTimeStamp;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerCombat>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isTriggerd && falseGround.IsTouchingLayers(playerLayer))
        {
            TriggerTrap();
        }
        if (isTriggerd && spikesColl.IsTouchingLayers(playerLayer) && !DialogManager.instance.isInDialogue)
        {
            if (dmgTimeStamp <= Time.time)
            {
                player.ApplyHit(damage, Vector2.up * 20, 0);
                dmgTimeStamp = Time.time + damageCooldown;
            }
        }
    }

    void TriggerTrap()
    {
        isTriggerd = true;
        anim.SetTrigger("TrapTrigger");
    }
}
