using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    //public bool HorizontalMovement;
    [SerializeField] private float moveSpeed;
    private bool moveForward;
    private Rigidbody2D rb;
    private Collider2D body;
    private Vector2 currentDest;
    private Vector2[] pointsStatic;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        body = GetComponent<Collider2D>();
        Transform[] points = GetComponentsInChildren<Transform>();

        pointsStatic = new Vector2[2];
        pointsStatic[0] = points[1].position;
        pointsStatic[1] = points[2].position;

        transform.position = pointsStatic[0];
        currentDest = pointsStatic[1];
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Vector2.Distance(transform.position, currentDest) <= 1)
        {
            PeekNext();
        }
        transform.position = Vector2.MoveTowards(transform.position, currentDest, moveSpeed);
    }

    void PeekNext()
    {
        if (currentDest == pointsStatic[0])
            currentDest = pointsStatic[1];
        else
            currentDest = pointsStatic[0];
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "Player")
            other.transform.SetParent(transform);
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.collider.tag == "Player")
            other.transform.parent = null;
    }

    private void OnDrawGizmos()
    {
        Transform[] points = GetComponentsInChildren<Transform>();
        Gizmos.DrawWireSphere(points[1].position, 3);
        Gizmos.DrawWireSphere(points[2].position, 3);
    }
}