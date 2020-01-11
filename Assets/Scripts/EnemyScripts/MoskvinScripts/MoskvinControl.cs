using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoskvinControl : EnemyControl
{
    private DialogManager dial;
    

    private void Start()
    {
        dial = GameObject.FindGameObjectWithTag("gameManager").GetComponent<DialogManager>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (dial.isInDialogue == false)
        {
           
            AnimateMove(target.position);
        }
    }
    public override void AnimateMove(Vector2 targetPos)
    {       

        if (targetPos.x > transform.position.x)
            anim.FacingRight(true);
        else
            anim.FacingRight(false);
    }
}
