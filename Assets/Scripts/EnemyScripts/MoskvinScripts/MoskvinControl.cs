using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoskvinControl : EnemyControl
{
    protected override void FixedUpdate()
    {
        if (DialogManager.instance.isInDialogue == false)
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
