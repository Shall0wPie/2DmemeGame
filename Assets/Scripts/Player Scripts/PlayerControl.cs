using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    public float horizontalMove { get; private set; }
    public PlayerMovement movement;
    public PlayerCombat combat;
    public PlayerAnimationControl anim;

    [SerializeField] private bool isOnGround = true;
    private float punchTimeStamp = 0;
    private float shotTimeStamp = 0;
    private float jumpTimeStamp = 0;

    private void Update()
    {
        isOnGround = movement.IsOnGround();
        if (jumpTimeStamp <= Time.time && Input.GetButton("Jump") && isOnGround)
        {
            movement.Jump();
            jumpTimeStamp = Time.time + movement.jumpCooldown;
        }
        anim.PlayJump(!isOnGround);

        AnimateMove();

        if (punchTimeStamp <= Time.time && Input.GetButton("Fire1"))
        {
            combat.Punch();
            anim.PlayAttack();
            punchTimeStamp = Time.time + combat.punchCooldown;
        }

        if (punchTimeStamp <= Time.time && Input.GetKeyDown(KeyCode.Q))
        {
            combat.Shoot();
            punchTimeStamp = Time.time + combat.shotCooldown;
        }

        if (Input.GetKeyDown(KeyCode.G))
            Inventory.instance.DropItem();
        if (Input.GetKeyDown(KeyCode.E))
            Inventory.instance.UseItem();

        if (Input.GetKeyDown(KeyCode.Alpha1))
            Inventory.instance.SelectItem(0);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            Inventory.instance.SelectItem(1);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            Inventory.instance.SelectItem(2);
        if (Input.GetKeyDown(KeyCode.Alpha4))
            Inventory.instance.SelectItem(3);
    }

    void FixedUpdate()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");
        movement.Move(horizontalMove * Time.fixedDeltaTime);
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
