using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolkControl : EnemyControl
{
    protected override void FixedUpdate()
    {
        if (DialogManager.instance.isInDialogue == false && !anim.anim.GetBool("IsOnFly"))
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
