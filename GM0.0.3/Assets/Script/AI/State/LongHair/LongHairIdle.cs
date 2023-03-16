using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongHairIdle : LongHairBaseState
{
    private float StateStartTime;

    public override void EnterState(LongHairFSM StateManager)
    {
    //    Debug.Log(StateManager.gameObject.name + "In Idle");

        StateStartTime = Time.time;
    }

    public override void UpdateState(LongHairFSM StateManager)
    {

        if (Time.time > StateStartTime + StateManager.EnemyState.ReactionTime)
        {
            HurtTrigger(StateManager);

            AttackTrigger(StateManager);

            SPAttackTrigger(StateManager);

            FlipTrigger(StateManager);

            SwitchAction(StateManager);
        }

    }

    private void SwitchAction(LongHairFSM StateManager)
    {
        if (StateManager.EnemyState.PlayerDistanceIndex == 0)
        {
            StateManager.StateSwitch(StateManager.Walk);
        }
        else
        {
            StateManager.StateSwitch(StateManager.Dash);
        }
    }
}
