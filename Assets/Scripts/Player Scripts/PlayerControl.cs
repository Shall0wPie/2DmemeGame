using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    public float horizontalMove { get; private set; }
    public bool attack { get; private set; }
    public PlayerMovement movement;
    public PlayerCombat combat;

    private bool jump = false;
    private float punchTimeStamp = 0;

    void FixedUpdate()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");
        jump = Input.GetButton("Jump");
        attack = false;

        movement.Move(horizontalMove * Time.fixedDeltaTime, jump);

        if (punchTimeStamp <= Time.time && Input.GetButton("Fire1"))
        {
            attack = true;
            combat.Punch();
            punchTimeStamp = Time.time + combat.punchCooldown;
        }

        if (Input.GetKeyDown(KeyCode.G))
            Inventory.instance.DropItem(0);
    }
}
