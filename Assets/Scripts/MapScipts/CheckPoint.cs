using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private int pointNumber;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && SaveManager.checkPoint != pointNumber)
        {
            SaveManager.SaveGame(pointNumber);
        }
    }
}