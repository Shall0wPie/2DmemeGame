using System;
using System.Collections;
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
    [SerializeField] private Vector2 gravity;

    private bool stun;
    private new Rigidbody2D rigidbody;
    RaycastHit2D hit;

    private Vector2 prev;

    protected void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!IsOnGround())
            rigidbody.velocity += gravity;
    }

    public void Move(float horizontalMove)
    {
        if (stun)
        {
            prev.x = 0;
            return;
        }

        Vector2 velocity = Vector2.zero;
        float maxVel = Mathf.Abs(horizontalMove * runSpeed);

        if (IsOnSlope())
        {
            float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);

            if (jumpForce > rigidbody.velocity.y)
            {
                velocity.x = Mathf.Cos(slopeAngle * Mathf.Deg2Rad) * horizontalMove * runSpeed;
                if (hit.normal.x > 0)
                    horizontalMove *= -1;
                velocity.y = Mathf.Sin(slopeAngle * Mathf.Deg2Rad) * horizontalMove * runSpeed;

                velocity.x = Mathf.Clamp(velocity.x - rigidbody.velocity.x, -runSpeed, runSpeed);
                velocity.y = Mathf.Clamp(velocity.y - rigidbody.velocity.y, -runSpeed, runSpeed);
            }
        }
        else
        {
            if (horizontalMove != 0)
            {
                velocity.x = Mathf.Clamp(horizontalMove * runSpeed - rigidbody.velocity.x, -maxVel, maxVel);
                prev.x = Mathf.Clamp(rigidbody.velocity.x, -maxVel, maxVel);
            }
            else
            {
                velocity.x -= prev.x;
                prev.x = 0;
            }
        }

        rigidbody.velocity += velocity;
    }

    public void Jump()
    {
        Vector2 fumpVelocity = new Vector2(rigidbody.velocity.x, jumpForce);
        rigidbody.velocity = fumpVelocity;
    }

    public IEnumerator Stun(float time)
    {
        stun = true;
        yield return new WaitForSeconds(time);
        stun = false;
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