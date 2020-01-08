using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private new Rigidbody2D rigidbody;

    [SerializeField] private float runSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Collider2D groundCollider;
    public float jumpCooldown;



    protected void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    public void Move(float horizontalMove)
    {
        Vector2 targetVelocity = new Vector2(horizontalMove * runSpeed, 0);
        rigidbody.position += targetVelocity;

    }

    public void Jump()
    {
        Vector2 fumpVelocity = new Vector2(rigidbody.velocity.x, jumpForce);
        rigidbody.velocity = fumpVelocity;
    }

    // Checks if player is on ground
    public bool IsOnGround()
    {
        return groundCollider.IsTouchingLayers(groundLayer);
    }
}
