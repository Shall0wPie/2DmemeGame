using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsPopup : MonoBehaviour
{
    //сделать окно активным
    public void Open()
    {
        gameObject.SetActive(true);
    }

    //сделать окно неактивным
    public void Close()
    {
        gameObject.SetActive(false);
    }
}
