using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    #region Singltone

    public static SaveManager instance { get; private set; }

    void Start()
    {
        if (instance != null)
        {
            Debug.Log("Save manager alredy exists");
            Destroy(gameObject);
        }
        else
        {
            save = new Save();
            checkPoint = -1;
            instance = this;
            DontDestroyOnLoad(gameObject);
            SaveGame();
        }
    }

    #endregion


    public Vector2 lastPosition { get; private set; }
    public int checkPoint { get; private set; }


    private static Save save;
    private static string saveString;

    private void Awake()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            SaveGame();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadGame();
        }
    }

    #region OldSaveSystem

    public void SaveAll(int pointNum)
    {
        SavePosition(pointNum);
    }

    public void SavePosition(int pointNum)
    {
        lastPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        checkPoint = pointNum;
    }

    #endregion

    #region NewSaveSystem

    public static void SaveGame()
    {
        Debug.Log("SaveGame");
        save = new Save();
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
        Debug.Log("LoadGame");
        Debug.Log(saveString);
        if (saveString == null)
            return;
        JsonUtility.FromJsonOverwrite(saveString, save);

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = save.pos;
        player.GetComponentInChildren<PlayerCombat>().hp = save.hp;

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

[Serializable]
public class Save
{
    public Vector3 pos;
    public float hp;
    public List<ItemSlot> mainContainer;
    public List<ItemSlot> quickContainer;
}

[Serializable]
public class Obj
{
    public string name = "NAME";
    public Sprite icon;
    public int size = 2;
}