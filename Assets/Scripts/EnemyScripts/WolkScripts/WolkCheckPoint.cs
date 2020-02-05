using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolkCheckPoint : MonoBehaviour
{
    private Animator wolkAnim;
    
    private void Start()
    {
        CheckPoint.onSave += TriggerWolk;
        wolkAnim = GetComponentInChildren<Animator>();
    }

    private void TriggerWolk(int pointNumber)
    {
        switch (pointNumber)
        {
            case 0:
                wolkAnim.SetInteger("Stage", 40);
                break;
            case 1:
                wolkAnim.SetInteger("Stage", 30);
                break;
            case 2:
                wolkAnim.SetInteger("Stage", 20);
                break;
            case 3:
                wolkAnim.SetInteger("Stage", 10);
                break;
        }
    }

    private void OnDestroy()
    {
        CheckPoint.onSave -= TriggerWolk;
    }
}