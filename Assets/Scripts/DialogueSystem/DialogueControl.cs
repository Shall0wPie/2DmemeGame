using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DialogueControl : MonoBehaviour
{
    [SerializeField] private Dialogue[] dialogue;

    public void TriggerDialogue(string dialogueName)
    {
        for (int i = 0; i < dialogue.Length; i++)
        {
            if (dialogueName == dialogue[i].dialogueName)
            {
                DialogManager.instance.StartDialogue(dialogue[i]);
            }
        }
    }
}
