using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    public float horizontalMove { get; private set; }
    public bool attack { get; private set; }
    public PlayerMovement movement;
    public PlayerCombat combat;

    private bool jump = false;
    private float timeStamp = 0;

    void FixedUpdate()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");
        jump = Input.GetButton("Jump");
        attack = false;

        movement.Move(horizontalMove * Time.fixedDeltaTime, jump);

        if (timeStamp <= Time.time && Input.GetButton("Fire1"))
        {
            attack = true;
            combat.Punch();
            timeStamp = Time.time + combat.punchCooldown;
        }
    }
}
