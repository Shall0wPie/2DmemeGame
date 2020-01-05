using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float downRayLength = 1.8f;
    private Vector2 currentVelocity = Vector2.zero;
    private new Rigidbody2D rigidbody;
    public Rigidbody2D rigidbody1;

    [SerializeField] private float runSpeed = 900f;
    [SerializeField] private float jumpForce = 40f;
    [Range(0, 0.3f)] [SerializeField] public float smoothTime = 0.05f;
    [SerializeField] private LayerMask groundLayer;
    public bool isOnGround = false;

    protected void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody1 = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    public void Move(float horizontalMove, bool jump)
    {
        isOnGround = GroundCheck();

        // Handles horizontal move
        Vector2 targetVelocity = new Vector2(horizontalMove * runSpeed, rigidbody.velocity.y);
        rigidbody.velocity = Vector2.SmoothDamp(rigidbody.velocity, targetVelocity, ref currentVelocity, smoothTime);
        
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
        Debug.DrawRay(transform.position, Vector2.down * downRayLength, Color.green);
        RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, Vector2.down, downRayLength, groundLayer);
        
        foreach(RaycastHit2D single in hit)
        {
            if (!single.collider.isTrigger)
                return true;
        }

        return false;
    }
}
