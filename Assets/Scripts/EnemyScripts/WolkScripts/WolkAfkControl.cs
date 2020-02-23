using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolkAfkControl : StateMachineBehaviour
{
    private Animator anim;
    private WolkCombat combat;
    private DialogueControl dialogue;


    private void OnEnable()
    {
        combat = GameObject.Find("Wolk").GetComponentInChildren<WolkCombat>();
        anim = GameObject.Find("Wolk").GetComponentInChildren<Animator>();
        dialogue = GameObject.Find("Wolk").GetComponent<DialogueControl>();
    }

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        switch (anim.GetInteger("Stage"))
        {
            case 40:
                combat.StartCoroutine(TeleportToTalk("FirstDialogue", combat.tpPoints[4]));
                anim.SetInteger("Stage", 0);
                break;
            case 30:
                combat.StartCoroutine(TeleportToTalk("SecondDialogue", combat.tpPoints[3]));
                anim.SetInteger("Stage", 0);
                break;
            case 20:
                combat.StartCoroutine(TeleportToTalk("SecondDialogue", combat.tpPoints[2]));
                anim.SetInteger("Stage", 0);
                break;
            case 10:
                combat.StartCoroutine(TeleportToTalk("ThirdDialogue", combat.tpPoints[0]));
                anim.SetInteger("Stage", 0);
                combat.isInvincible = false;
                break;
        }

        if (anim.GetInteger("Stage") == 0 && SaveManager.checkPoint == 3 && !DialogManager.instance.isInDialogue)
        {
            anim.SetInteger("Stage", 1);
        }

        if (anim.GetInteger("Stage") == 1)
        {
            combat.isInvincible = false;
            anim.SetBool("IsCast", true);
        }

        if (anim.GetInteger("Stage") == 2 && !DialogManager.instance.isInDialogue)
        {
            dialogue.TriggerDialogue("WolfPower");
            anim.SetBool("IsWolfed", true);
        }

        if (anim.GetInteger("Stage") == 3 && !DialogManager.instance.isInDialogue)
        {
            anim.SetBool("IsCast", true);
        }

        if (anim.GetInteger("Stage") == 4 && !DialogManager.instance.isInDialogue)
        {
            dialogue.TriggerDialogue("WolfPower2");
            anim.SetBool("IsWolfed", true);
        }

        if (anim.GetInteger("Stage") == 5 && !DialogManager.instance.isInDialogue)
        {
            anim.SetBool("IsCast", true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    IEnumerator TeleportToTalk(string dialogueName, Transform to)
    {
        dialogue.TriggerDialogue(dialogueName);
        combat.transform.parent.position = to.position;

        while (DialogManager.instance.isInDialogue)
        {
            yield return null;
        }

        yield return new WaitForSeconds(0.2f);
        combat.transform.parent.position = combat.tpPoints[0].position;
        if (dialogueName == "ThirdDialogue")
            anim.SetInteger("Stage", 1);
    }
}