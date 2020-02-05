using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolkAfkControl : StateMachineBehaviour
{
    private Animator anim;
    private WolkCombat combat;


    private void OnEnable()
    {
        combat = GameObject.Find("Wolk").GetComponentInChildren<WolkCombat>();
        anim = GameObject.Find("Wolk").GetComponentInChildren<Animator>();
    }
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (anim.GetInteger("Stage") == 0)
        {
            anim.SetBool("IsCast", true);
        }
        
        if (anim.GetInteger("Stage") == 1 && !DialogManager.instance.isInDialogue)
        {
            combat.GetComponentInParent<DialogueControl>().TriggerDialogue("WolfPower");
            anim.SetBool("IsWolfed", true);
        }

        if (anim.GetInteger("Stage") == 2 && !DialogManager.instance.isInDialogue)
        {
            anim.SetBool("IsCast", true);
        }
        
        if (anim.GetInteger("Stage") == 3 && !DialogManager.instance.isInDialogue)
        {
            combat.GetComponentInParent<DialogueControl>().TriggerDialogue("WolfPower2");
            anim.SetBool("IsWolfed", true);
        }
        
        if (anim.GetInteger("Stage") == 4 && !DialogManager.instance.isInDialogue)
        {
            anim.SetBool("IsCast", true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
