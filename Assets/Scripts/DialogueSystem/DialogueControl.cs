using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DialogueControl : MonoBehaviour
{
    [SerializeField] private Dialogue[] dialogue;
    private DialogManager dialogManager;

    private void Start()
    {
        dialogManager = DialogManager.instance;
    }

    public void TriggerDialogue(string dialogueName)
    {
        for (int i = 0; i < dialogue.Length; i++)
        {
            if (dialogueName == dialogue[i].dialogueName)
            {
                dialogManager.StartDialogue(dialogue[i]);
            }
        }
    }
}
