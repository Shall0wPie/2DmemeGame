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
    private int selector;

    private void Start()
    {
        movement = GetComponent<PlayerMovement>();
        combat = GetComponentInChildren<PlayerCombat>();
        anim = GetComponentInChildren<PlayerAnimationControl>();
        stats = GetComponent<PlayerStats>();
        dialogues = GetComponent<DialogueControl>();

        dialogues.TriggerDialogue("HochuSkazt");

        if (SaveManager.instance != null && SaveManager.instance.lastPosition != Vector2.zero)
            transform.position = SaveManager.instance.lastPosition;
    }

    private void Update()
    {
        isOnGround = movement.IsOnGround();

        if (!DialogManager.instance.isInDialogue)
        {
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

            if (Input.mouseScrollDelta.y > 0)
            {
                selector++;
                if (Inventory.instance.slotsCount - 1 < selector)
                    selector = Inventory.instance.slotsCount - 1;
                Inventory.instance.SelectSlot(selector);
            }
            else if (Input.mouseScrollDelta.y < 0)
            {
                selector--;
                if (selector < 0)
                    selector = 0;
                Inventory.instance.SelectSlot(selector);
            }


            if (Input.GetKeyDown(KeyCode.G))
                Inventory.instance.DropItem();
            if (Input.GetKeyDown(KeyCode.Q))
                Inventory.instance.UseItem();
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("Fire1"))
                DialogManager.instance.DisplayNextSentence();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selector = 0;
            Inventory.instance.SelectSlot(selector);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            selector = 1;
            Inventory.instance.SelectSlot(selector);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            selector = 2;
            Inventory.instance.SelectSlot(selector);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            selector = 3;
            Inventory.instance.SelectSlot(selector);
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
        if (!DialogManager.instance.isInDialogue)
        {
            horizontalMove = Input.GetAxisRaw("Horizontal");
            movement.Move(horizontalMove * Time.fixedDeltaTime);
        }
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
