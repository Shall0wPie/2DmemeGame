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
            //Debug.Log("Save manager alredy exists");
            Destroy(gameObject);
        }
        else
        {
            checkPoint = -1;
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    #endregion


    public Vector2 lastPosition { get; private set; }
    public int checkPoint { get; private set; }


    [SerializeField] private InventorySystem inventory;
    private Save save;
    private string saveString;

    private void OnLevelWasLoaded(int level)
    {
        if (level == 3)
            Destroy(gameObject);
    }

    private void Awake()
    {
        save = new Save();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            SaveGame();
            saveString = JsonUtility.ToJson(save);
            Debug.Log(saveString);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            JsonUtility.FromJsonOverwrite(saveString, save);
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

    public void SaveGame()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        save.pos = player.transform.position;
        save.hp = player.GetComponentInChildren<PlayerCombat>().hp;
        //save.slots = inventory.slots;
        save.slots = new List<InventorySlot>();
        foreach (InventorySlot slot in inventory.slots)
        {
            save.slots.Add(slot);
        }
    }

    public void LoadGame()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = save.pos;
        player.GetComponentInChildren<PlayerCombat>().hp = save.hp;
        foreach (InventorySlot slot in inventory.slots)
        {
            slot.DestroySlot();
        }
        foreach (InventorySlot slot in save.slots)
        {
            inventory.slots.Add(slot);
        }
        
    }

    #endregion
}

[Serializable]
public class Save
{
    public Vector3 pos;
    public float hp;
    public List<InventorySlot> slots;
}

[Serializable]
public class Obj
{
    public string name = "NAME";
    public Sprite icon;
    public int size = 2;
}