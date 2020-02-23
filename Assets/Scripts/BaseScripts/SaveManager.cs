using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


[Serializable]
public class Save
{
    public Vector3 pos;
    public float hp;
    public int checkPoint;
    public List<ItemSlot> mainContainer;
    public List<ItemSlot> quickContainer;
}

public class SaveManager : MonoBehaviour
{
    public delegate void OnSave(int checkPoint);
    public static event OnSave OnSaveEvent;
    
    public static int checkPoint
    {
        get => _checkPoint;
        private set => _checkPoint = value;
    }
    
    private static int _checkPoint;
    private static Save save;
    private static string saveString;


    private void Start()
    {
        _checkPoint = -1;
    }

    #region SaveLoad

    public static void SaveGame(int newCheckPoint)
    {
        OnSaveEvent?.Invoke(newCheckPoint);
        
        save = new Save();

        _checkPoint = newCheckPoint;
        save.checkPoint = _checkPoint;
        
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        save.pos = player.transform.position;
        save.hp = player.GetComponentInChildren<PlayerCombat>().hp;

        List<Container> containers = player.GetComponentInChildren<Inventory>().containers;

        save.quickContainer = new List<ItemSlot>();
        foreach (ItemSlot slot in containers[0].itemSlots)
        {
            save.quickContainer.Add(slot);
        }

        save.mainContainer = new List<ItemSlot>();
        foreach (ItemSlot slot in containers[1].itemSlots)
        {
            save.mainContainer.Add(slot);
        }


        saveString = JsonUtility.ToJson(save);
    }

    public static void LoadGame()
    {
        if (saveString == null)
            return;
        
        
        JsonUtility.FromJsonOverwrite(saveString, save);

        _checkPoint = save.checkPoint;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = save.pos;
        player.GetComponentInChildren<PlayerCombat>().hp = save.hp;
        
        MainCamera.instance.transform.position = MainCamera.instance.offset + save.pos;

        List<Container> containers = player.GetComponentInChildren<Inventory>().containers;

        foreach (ItemSlot slot in save.quickContainer)
        {
            for (int i = 0; i < slot.count; i++)
            {
                containers[0].AddToContainer(slot.item);
            }
        }

        foreach (ItemSlot slot in save.mainContainer)
        {
            for (int i = 0; i < slot.count; i++)
            {
                containers[1].AddToContainer(slot.item);
            }
        }
    }

    #endregion
}
