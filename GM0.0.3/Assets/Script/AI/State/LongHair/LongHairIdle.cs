using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongHairIdle : LongHairBaseState
{
    private float StateStartTime;

    public override void EnterState(LongHairFSM StateManager)
    {
      //      Debug.Log(StateManager.gameObject.name + "In Idle");

        StateStartTime = Time.time;
    }

    public override void UpdateState(LongHairFSM StateManager)
    {
        FlipTrigger(StateManager);

        HurtTrigger(StateManager);


        if (Time.time > StateStartTime + StateManager.EnemyState.ReactionTime)
        {
            AttackTrigger(StateManager);

            SPAttackTrigger(StateManager);

            SwitchAction(StateManager);
        }

    }

    private void SwitchAction(LongHairFSM StateManager)
    {
        if (StateManager.EnemyState.PlayerDistanceIndex < 2)
        {
            StateManager.StateSwitch(StateManager.Walk);
        }
        else if(StateManager.EnemyState.RollAble)
        {
            StateManager.StateSwitch(StateManager.Dash);
        }
    }
}
