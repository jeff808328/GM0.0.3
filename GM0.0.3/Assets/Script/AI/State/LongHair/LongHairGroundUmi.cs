using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongHairGroundUmi : LongHairBaseState
{
    private float StateEnterTime;
    public override void EnterState(LongHairFSM StateManager)
    {
        StateEnterTime = Time.time;
        //   Debug.Log(StateManager.gameObject.name + " start ground umi attack state");

        StateManager.CallUmiAttack();

        StateManager.EnemyState.AttackMethodUsedTime[4]++;
    }

    public override void UpdateState(LongHairFSM StateManager)
    {
        if (!StateManager.EnemyState.AttackIng & Time.time > StateEnterTime + StateManager.EnemyState.ReactionTime)
        {
       //     Debug.Log(StateManager.gameObject.name + " end ground umi attack state");

            StateManager.StateSwitch(StateManager.Idle);
        }
    }
}
