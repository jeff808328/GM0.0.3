using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : CommonAttack
{
    protected EnemyState EnemyState;
    protected EnemyAnimation EnemyAnimation;

    protected void EnemyInitSet()
    { 
        CommonState = this.GetComponent<EnemyState>();
        EnemyState = this.GetComponent<EnemyState>();

        CommonMove = this.GetComponent<CommonMove>();
        EnemyAnimation = this.GetComponent<EnemyAnimation>();

        AttackStartTime = 0;
        CDStartTime = Time.time - EnemyState.AttackCD;
    }

    public void CommonAttack()
    {
        AttackStartTime = Time.time;

        EnemyState.ActionIndex++;

        EnemyAnimation.Animator.SetTrigger("Atk" + EnemyState.ActionIndex.ToString());

        StartCoroutine(Attack(true));
    }
}
