using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongHairWalk : LongHairBaseState
{
    public override void EnterState(LongHairFSM StateManager)
    {
        Debug.Log(StateManager.gameObject.name + " is in walk state");
    }

    public override void UpdateState(LongHairFSM StateManager)
    {
     
        HurtTrigger(StateManager);

        AttackTrigger(StateManager);

        SPAttackTrigger(StateManager);

        FlipTrigger(StateManager);

        if (StateManager.EnemyState.PlayerDistanceIndex > 1 && StateManager.EnemyState.RollAble)
        {
            StateManager.StateSwitch(StateManager.Dash);
        }
    }
}
