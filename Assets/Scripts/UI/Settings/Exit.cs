using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        //при нажатии левой кнопкой мыши закрываем игру
        if (Input.GetMouseButton(0))
        {
            Application.Quit();
        }
    }
}
