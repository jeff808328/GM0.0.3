using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongHairAirUmi : LongHairBaseState
{
    private float StateEnterTime;

    public override void EnterState(LongHairFSM StateManager)
    {
        StateEnterTime = Time.time;

        Debug.Log(StateManager.gameObject.name + " start air umi attack state");

        StateManager.CallJump();
        StateManager.CallUmiAttack();
       // StateManager.EnemyAttack.UmiAttack();

        StateManager.EnemyState.AttackMethodUsedTime[3]++;
    }

    public override void UpdateState(LongHairFSM StateManager)
    {
        if (!StateManager.EnemyState.AttackIng & Time.time > StateEnterTime + StateManager.EnemyState.ReactionTime)
        {
            Debug.Log(StateManager.gameObject.name + " end air umi attack state");

            StateManager.StateSwitch(StateManager.Idle);
        }
    }
}

