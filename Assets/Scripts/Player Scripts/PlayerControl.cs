using System;
using System.Collections;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float horizontalMove { get; private set; }
    private PlayerMovement movement;
    private PlayerCombat combat;
    private PlayerAnimationControl anim;
    private PlayerStats stats;
    private DialogueControl dialogues;

    [SerializeField] private bool isOnGround = true;
    private float punchTimeStamp = 0;
    private float jumpTimeStamp = 0;

    private void Start()
    {
        movement = GetComponent<PlayerMovement>();
        combat = GetComponentInChildren<PlayerCombat>();
        anim = GetComponentInChildren<PlayerAnimationControl>();
        stats = GetComponent<PlayerStats>();
        dialogues = GetComponent<DialogueControl>();
        
        SaveManager.LoadGame();
    }

    private void Update()
    {
        isOnGround = movement.IsOnGround();

        if (!DialogManager.instance.isInDialogue)
        {
            horizontalMove = Input.GetAxisRaw("Horizontal");

            if (jumpTimeStamp <= Time.time && Input.GetButton("Jump") && isOnGround)
            {
                movement.Jump();
                jumpTimeStamp = Time.time + movement.jumpCooldown;
            }

            if (punchTimeStamp <= Time.time && Input.GetButton("Fire1"))
            {
                combat.Punch();
                anim.PlayAttack();
                punchTimeStamp = Time.time + combat.punchCooldown;
            }

            
        }
        else
        {
            horizontalMove = 0;
        }

        

        // DELETE THIS
        //if (Input.GetKey(KeyCode.Q))
        //    combat.Shoot();


        anim.PlayJump(!isOnGround);
        AnimateMove();
        if (transform.position.y < -10 && stats.isAlive)
        {
            StartCoroutine(combat.Kill());
        }
    }

    void FixedUpdate()
    {
        movement.Move(horizontalMove * Time.deltaTime);
    }

    void AnimateMove()
    {
        if (horizontalMove > 0)
        {
            anim.FacingRight(false);
            anim.PlayMove();
        }
        else if (horizontalMove < 0)
        {
            anim.FacingRight(true);
            anim.PlayMove();
        }
        else
            anim.PlayStand();
    }
}