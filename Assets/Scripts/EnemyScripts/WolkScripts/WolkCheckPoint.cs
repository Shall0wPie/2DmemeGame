using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolkCheckPoint : MonoBehaviour
{
    private void Start()
    {
        CheckPoint.onSave += TriggerWolk;
    }

    public static void TriggerWolk(int pointNumber)
    {
        Animator wolkAnim = GameObject.Find("Wolk").GetComponentInChildren<Animator>();
        switch (pointNumber)
        {
            case 0:
                wolkAnim.SetInteger("Stage", -3);
                break;
            case 1:
                wolkAnim.SetInteger("Stage", -2);
                break;
            case 2:
                wolkAnim.SetInteger("Stage", -2);
                break;
            case 3:
                wolkAnim.SetInteger("Stage", -1);
                break;
        }
    }
}