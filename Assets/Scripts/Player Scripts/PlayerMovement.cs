using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private new Rigidbody2D rigidbody;
    [SerializeField] private float downRayLength = 1.8f;

    [SerializeField] private float runSpeed = 0f;
    [SerializeField] private float jumpForce = 0f;
    [Range(0, 0.3f)] [SerializeField] public float smoothTime = 0.05f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Collider2D groundCollider;
    public bool isOnGround = false;

    protected void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    public void Move(float horizontalMove, bool jump)
    {
        isOnGround = GroundCheck();

        // Handles horizontal move
        Vector2 targetVelocity = new Vector2(horizontalMove * runSpeed, 0);
        transform.position += (Vector3)targetVelocity;
        // Handles jump move
        if (jump && isOnGround)
        {
            targetVelocity = new Vector2(rigidbody.velocity.x, jumpForce);
            rigidbody.velocity = targetVelocity;

            isOnGround = false;
        }
    }

    // Checks if player is on ground
    bool GroundCheck()
    {
        return groundCollider.IsTouchingLayers(groundLayer);
    }
}
