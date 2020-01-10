using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveControl : MonoBehaviour
{
    private PlayerMovement movement;
    private PlayerCombat combat;

    [SerializeField] private bool isOnGround = true;
    private float punchTimeStamp = 0;
    private float jumpTimeStamp = 0;
    private int selector;
    public void Control()
    {
        isOnGround = movement.IsOnGround();
        if (jumpTimeStamp <= Time.time && Input.GetButton("Jump") && isOnGround)
        {
            movement.Jump();
            jumpTimeStamp = Time.time + movement.jumpCooldown;
        }


        if (punchTimeStamp <= Time.time && Input.GetButton("Fire1"))
        {
            combat.Punch();
            //anim.PlayAttack();
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
    }
}
