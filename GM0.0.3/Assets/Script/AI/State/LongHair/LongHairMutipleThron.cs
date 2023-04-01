using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongHairMutipleThron : LongHairBaseState
{
    private float StateStartTime;
    public override void EnterState(LongHairFSM StateManager)
    {
        //   Debug.Log(StateManager.gameObject.name + " start boss skill state");

        StateStartTime = Time.time;

        StateManager.EnemyAttack.MutipleThronAttack();
    }

    public override void UpdateState(LongHairFSM StateManager)
    {
        if (!StateManager.EnemyState.AttackIng & Time.time > StateStartTime + 0.1f)
        {
            //      Debug.Log(StateManager.gameObject.name + " end boss skill state");

            StateManager.StateSwitch(StateManager.Idle);
        }
    }
}
