using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongHairHurt : LongHairBaseState
{
    public override void EnterState(LongHairFSM StateManager)
    {

    }

    public override void UpdateState(LongHairFSM StateManager)
    {
        if(!StateManager.EnemyState.Hurting)
        {
            StateManager.StateSwitch(StateManager.Idle);
        }
    }
}
