using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    public GameObject keeper;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (keeper == null)
            Destroy(gameObject);
    }
}
