using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    #region Singleton
    public static DialogManager instance { get; private set; }

    private void Start()
    {
        if (instance != null)
        {
            //Debug.Log("Dialog manager alredy exists");
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    #endregion

    public Text nameText;
    public Text dialogueText;

    public Animator anim;

    public bool isInDialogue { get; private set; }
    private bool isTyping = false;
    private string currentSentence;

    private Queue<string> sentences;

    void Awake()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        StartCoroutine(DisableControl());

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

    IEnumerator DisableControl()
    {
        PlayerControl playerControl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
        playerControl.enabled = false;
        yield return new WaitForSeconds(0.8f);
        playerControl.enabled = true;
    }
}
