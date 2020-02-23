using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class WolkCheckPoint : MonoBehaviour
{
    private Animator wolkAnim;
    [SerializeField] private GameObject door;
    
    private void Start()
    {
        SaveManager.OnSaveEvent += TriggerWolk;
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
                door.SetActive(true);
                wolkAnim.SetInteger("Stage", 10);
                break;
        }
    }

    private void OnDestroy()
    {
        SaveManager.OnSaveEvent -= TriggerWolk;
    }
}