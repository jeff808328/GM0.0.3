using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongHairFight : LongHairBaseState
{
    private float StateEnterTime;

    public override void EnterState(LongHairFSM StateManager)
    {
        FlipTrigger(StateManager);

        StateEnterTime = Time.time;

        //     Debug.Log(StateManager.gameObject.name + " start attack state");

        if (StateManager.EnemyState.AttackAble & Time.time > StateManager.EnemyState.LastAttackTime + StateManager.EnemyState.AttackCD)
            StateManager.EnemyAttack.ComboAttack();

        StateManager.EnemyState.LastAttackTime = Time.time;

        StateManager.EnemyState.AttackMethodUsedTime[StateManager.EnemyState.Combo]++;

        StateManager.EnemyState.AttackMethodUsedTime[5] = 0;
    }

    public override void UpdateState(LongHairFSM StateManager)
    {
        if (!StateManager.EnemyState.AttackIng & Time.time > StateEnterTime + StateManager.EnemyState.ReactionTime)
        {
            //    Debug.Log(StateManager.gameObject.name + " end attack state");

            StateManager.StateSwitch(StateManager.Idle);
        }
    }
}
