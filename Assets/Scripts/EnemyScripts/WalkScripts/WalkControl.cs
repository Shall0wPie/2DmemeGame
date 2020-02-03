using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkControl : EnemyControl
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
            anim.FacingRight(false);
        else
            anim.FacingRight(true);
    }
}
