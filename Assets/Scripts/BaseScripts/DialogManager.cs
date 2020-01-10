using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    #region Singleton
    public static DialogManager instance;

    private void Awake()
    {
        if (instance != null)
            Debug.Log("Dialogue Manager already exists");
        else
            instance = this;
    }
    #endregion

    public Text nameText;
    public Text dialogueText;

    public Animator anim;

    public bool isInDialogue { get; private set; }
    private bool isTyping = false;
    private string currentSentence;

    private Queue<string> sentences;

    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        sentences.Clear();
        nameText.text = dialogue.actorName;
        anim.SetBool("IsOpen", true);
        isInDialogue = true;

        foreach (string sentece in dialogue.sentence)
        {
            sentences.Enqueue(sentece);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0 && !isTyping)
        {
            anim.SetBool("IsOpen", false);
            isInDialogue = false;
            return;
        }

        if (isTyping)
        {
            dialogueText.text = currentSentence;
            StopAllCoroutines();
            isTyping = false;
        }
        else
        {
            currentSentence = sentences.Dequeue();
            StartCoroutine(Type(currentSentence));
        }
    }

    IEnumerator Type(string sentence)
    {
        isTyping = true;
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.02f);
        }
        isTyping = false;
    }
}
