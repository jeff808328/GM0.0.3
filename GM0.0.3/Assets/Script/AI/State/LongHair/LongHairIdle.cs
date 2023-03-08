using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongHairIdle : LongHairBaseState
{
    public override void EnterState(LongHairFSM StateManager)
    {
        Debug.Log("In Idle");
    }

    public override void UpdateState(LongHairFSM StateManager)
    {
        HurtTrigger(StateManager);

        AttackTrigger(StateManager);

        SPAttackTrigger(StateManager);

        FlipTrigger(StateManager);

        SwitchAction(StateManager);
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
