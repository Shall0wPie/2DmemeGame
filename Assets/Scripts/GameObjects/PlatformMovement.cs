using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    public bool HorizontalMovement;
    public float moveSpeed;
    private bool moveForward;
    private Transform[] points;
    Vector3[] pointsStatic;
    // Start is called before the first frame update
    void Start()
    {
        
        points = GetComponentsInChildren<Transform>();
        pointsStatic = new Vector3[points.Length];
        for (int i = 0; i < points.Length; i++)
        {
            if (points[i].position != Vector3.zero)
            {
                pointsStatic[i] = points[i].position;
            }
        }
        if (HorizontalMovement == true)
        {
            if (pointsStatic[1].x < pointsStatic[2].x)
                moveForward = true;
            if (pointsStatic[1].x > pointsStatic[2].x)
                moveForward = false;
        }
        if (HorizontalMovement == false)
        {
            if (pointsStatic[1].y < pointsStatic[2].y)
                moveForward = true;
            if (pointsStatic[1].y > pointsStatic[2].y)
                moveForward = false;
        }
        Vector3 odds = pointsStatic[2] - pointsStatic[1];
        if (moveForward == true)
        {
            pointsStatic[1] = transform.position;
            pointsStatic[2] = transform.position + odds;
        }
        if (moveForward == false)
        {
            pointsStatic[2] = transform.position;
            pointsStatic[1] = transform.position + odds;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(transform.position);
        Debug.Log(pointsStatic[2]);
        Debug.Log(moveForward);
        if (HorizontalMovement == true)
        {
            if (transform.position.x > pointsStatic[2].x)
                moveForward = false;
            if (transform.position.x < pointsStatic[1].x)
                moveForward = true;
        }
        if (HorizontalMovement == false)
        {
            if (transform.position.y > pointsStatic[2].y)
                moveForward = false;
            if (transform.position.y < pointsStatic[1].y)
                moveForward = true;
        }

        if (moveForward==true)
        {
            if (HorizontalMovement==true)
                transform.position = new Vector2(transform.position.x + moveSpeed * Time.deltaTime, transform.position.y);
            if (HorizontalMovement==false)
                transform.position = new Vector2(transform.position.x, transform.position.y + moveSpeed * Time.deltaTime);

        }
        if (moveForward==false)
        {
            if (HorizontalMovement==true)
                transform.position = new Vector2(transform.position.x - moveSpeed * Time.deltaTime, transform.position.y);
            if (HorizontalMovement==false)
                transform.position = new Vector2(transform.position.x, transform.position.y - moveSpeed * Time.deltaTime);

        }
    }
}
