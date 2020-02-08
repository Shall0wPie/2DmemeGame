using System;
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

    public bool isInDialogue { get; private set; }
    public Text nameText;
    public Text dialogueText;
    public Animator anim;

    private bool isControllEnabled = true;
    private bool isTyping = false;
    private string currentSentence;
    private PlayerStats playerStats;

    private Queue<string> sentences;

    void Awake()
    {
        sentences = new Queue<string>();
    }

    private void Update()
    {
        if (playerStats == null)
            playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        
        if (isControllEnabled && (Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("Fire1")))
            DisplayNextSentence();

        if (!playerStats.isAlive && isInDialogue)
        {
            StopDialogue();
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerCombat>().isInvincible = true;
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
            StopDialogue();
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
        isControllEnabled = false;
        yield return new WaitForSeconds(0.8f);
        isControllEnabled = true;
    }

    void StopDialogue()
    {
        anim.SetBool("IsOpen", false);
        isInDialogue = false;
        StopAllCoroutines();
        isTyping = false;
        GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerCombat>().isInvincible = false;
    }
}