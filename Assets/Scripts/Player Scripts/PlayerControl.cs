using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    public float horizontalMove { get; private set; }
    public bool attack { get; private set; }
    public PlayerMovement movement;
    public PlayerCombat combat;

    private bool jump = false;
    private float punchTimeStamp = 0;

    private void Update()
    {
        if (punchTimeStamp <= Time.time && Input.GetButton("Fire1"))
        {
            attack = true;
            combat.Punch();
            punchTimeStamp = Time.time + combat.punchCooldown;
        }

        if (Input.GetKeyDown(KeyCode.G))
            Inventory.instance.DropItem();

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
        jump = Input.GetButton("Jump");
        attack = false;

        movement.Move(horizontalMove * Time.fixedDeltaTime, jump);
    }
}
