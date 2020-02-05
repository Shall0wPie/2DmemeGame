using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfAfkControl : StateMachineBehaviour
{
    [SerializeField] [Range(0, 10f)] private float jumpCooldown;
    
    private float jumpTimeStemp = 0;
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
        if (combat.GetHpInPercents() < 0.6 && anim.GetInteger("Stage") == 2)
        {
            anim.SetBool("IsWolfed", false);
            anim.SetInteger("Stage", 3);
            combat.StartCoroutine(TpToBalcony(combat.tpPoints[0], "FirstWolfDefeat"));
            return;
        }
        
        if (combat.GetHpInPercents() < 0.3 && anim.GetInteger("Stage") == 4)
        {
            anim.SetInteger("Stage", 6);
            combat.StartCoroutine(TpToBalcony(combat.tpPoints[1], "SecondWolfDefeat"));
                
            Rigidbody2D rb = combat.GetComponentInParent<Rigidbody2D>();
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.velocity = Vector2.zero;
            return;
        }

        if (anim.GetInteger("Stage") == 6 && !DialogManager.instance.isInDialogue)
        {
            anim.SetInteger("Stage", 5);
            anim.SetBool("IsCast", true);
        }
        
        if (jumpTimeStemp < Time.time && !DialogManager.instance.isInDialogue && !anim.GetBool("IsCast"))
        {
            jumpTimeStemp = Time.time + jumpCooldown;
            anim.SetTrigger("Attack");
        }

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        anim.ResetTrigger("Attack");
    }

    private IEnumerator TpToBalcony(Transform to,string dialogueName)
    {
        dialogue.TriggerDialogue(dialogueName);
        while (DialogManager.instance.isInDialogue)
        {
            yield return null;
        }

        combat.transform.parent.position = to.position;
        yield return null;
    }
}