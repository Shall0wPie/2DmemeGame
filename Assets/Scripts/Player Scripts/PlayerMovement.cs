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
    [SerializeField] private Collider2D groundFriction;
    [SerializeField] private float rayLength;
    [SerializeField] private Vector2 gravity;

    private PhysicsMaterial2D frMat;
    private PhysicsMaterial2D smMat;
    private bool stun;
    private new Rigidbody2D rigidbody;
    private RaycastHit2D hit;

    private Vector2 prev;

    protected void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        
        frMat = new PhysicsMaterial2D();
        smMat = new PhysicsMaterial2D();
        frMat.friction = 1;
        smMat.friction = 0;
        groundFriction.sharedMaterial = smMat;
    }

    private void Update()
    {
        if (!IsOnGround() && rigidbody.velocity.y > -100)
            rigidbody.velocity += gravity;
    }

    public void Move(float horizontalMove)
    {
        // if (Input.GetMouseButtonDown(1))
        // { 
        //     rigidbody.AddForce(new Vector2(100, 0), ForceMode2D.Impulse);
        //     StartCoroutine(Stun(0.5f));
        // }

        if (stun)
        {
            prev = Vector2.zero;
            return;
        }

        hit = SurfaceCheck();
        Vector2 velocity = Vector2.zero;
        float maxVel = Mathf.Abs(horizontalMove * runSpeed);
        float slopeAngle = Vector2.Angle(SurfaceCheck().normal, Vector2.up);
        
        if (IsOnSlope())
            groundFriction.sharedMaterial = frMat;
        else
            groundFriction.sharedMaterial = smMat;


        if (rigidbody.velocity.y < jumpForce)
        {
            if (horizontalMove != 0)
            {
                velocity.x = horizontalMove * runSpeed;
                velocity.x *= Mathf.Cos(slopeAngle * Mathf.Deg2Rad);
                velocity.x = Mathf.Clamp(velocity.x - rigidbody.velocity.x, -maxVel, maxVel);
                prev.x = Mathf.Clamp(rigidbody.velocity.x, -maxVel, maxVel);

                if (IsOnGround())
                {
                    if (hit.normal.x > 0)
                        horizontalMove *= -1;

                    velocity.y = Mathf.Sin(slopeAngle * Mathf.Deg2Rad) * horizontalMove * runSpeed;
                    velocity.y = Mathf.Clamp(velocity.y - rigidbody.velocity.y, -runSpeed, runSpeed);
                    prev.y = Mathf.Clamp(rigidbody.velocity.y, -maxVel, maxVel);
                }
            }
            else
            {
                velocity -= prev;
                prev = Vector2.zero;
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
        // if (!IsOnGround())
        //     return false;

        hit = SurfaceCheck();

        if (hit && hit.normal != Vector2.up)
            return true;

        return false;
    }

    RaycastHit2D SurfaceCheck()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, rayLength, groundLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, Vector3.down * rayLength);
    }
}