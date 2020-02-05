using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public delegate void OnSave(int pointNumber);
    public static OnSave onSave;
    
    [SerializeField] private int pointNumber;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && SaveManager.instance.checkPoint != pointNumber)
        {
            SaveManager.instance.SaveAll(pointNumber);
            onSave(pointNumber);
        }
    }
}
