using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongHairDash : LongHairBaseState
{
    public override void EnterState(LongHairFSM StateManager)
    {
      //  Debug.Log(StateManager.gameObject.name + " is in dash state");
    }

    public override void UpdateState(LongHairFSM StateManager)
    {
        HurtTrigger(StateManager);

        AttackTrigger(StateManager);

        SPAttackTrigger(StateManager);

        if(!StateManager.EnemyState.Rolling)
        {
            StateManager.StateSwitch(StateManager.Idle);
        }
    }
}
