using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    public float jumpCooldown;

    [SerializeField] private float runSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Collider2D groundCollider;
    [SerializeField] private float rayLength;

    private new Rigidbody2D rigidbody;
    RaycastHit2D hit;

    private Vector2 prev;

    protected void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Move(float horizontalMove)
    {
        Vector2 velocity = Vector2.zero;
        if (IsOnSlope())
        {
            float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);

            if (jumpForce > velocity.y)
            {
                velocity.x = Mathf.Cos(slopeAngle * Mathf.Deg2Rad) * horizontalMove * runSpeed;
                if (hit.normal.x > 0)
                    horizontalMove *= -1;
                velocity.y = Mathf.Sin(slopeAngle * Mathf.Deg2Rad) * horizontalMove * runSpeed;
            }
        }
        else
        {
            velocity.x = horizontalMove * runSpeed;
        }

        transform.position += (Vector3)velocity;
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

    bool IsOnSlope()
    {
        if (!IsOnGround())
            return false;
        hit = Physics2D.Raycast(transform.position, Vector2.down, rayLength, groundLayer);
        if (hit)
        {
            if (hit.normal != Vector2.up)
                return true;
        }

        return false;
    }
}