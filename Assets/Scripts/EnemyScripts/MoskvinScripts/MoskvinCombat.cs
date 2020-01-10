using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoskvinCombat : EnemyCombat
{
    public Transform[] point;
    public Transform moskva;
    int i = 0;
    // Start is called before the first frame update
    void Start()
    {
        moskva = GameObject.FindGameObjectWithTag("Moskvin").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Teleport();
    }

    void Teleport()
    { 
        if ((Input.GetKeyDown(KeyCode.Z))&&(i<point.Length))
        {
            
                moskva.position = point[i].position;
            i++;
        }

        if (Input.GetKeyDown(KeyCode.X))
        {

            animControl.PlayAttack(); 
        }
    }
}
