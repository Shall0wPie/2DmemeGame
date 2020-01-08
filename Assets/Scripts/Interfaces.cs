using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IAttack
{
    float distance { get; set; }
    EnemyAnimationControl animation { get; set; }


}
 
interface IApplyHit
{

}
 
interface IKill
{

}

interface IRespawn
{

}

interface IMove
{
    Transform pos { get; set; }

}
 
interface IAnimation
{

}
