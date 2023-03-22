using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongHairSingleThron : LongHairBaseState
{
    public override void EnterState(LongHairFSM StateManager)
    {
        FlipTrigger(StateManager);

     //   Debug.Log(StateManager.gameObject.name + " start thron attack state");
      //  StateManager.EnemyAttack.ThronAttack();

        StateManager.EnemyState.AttackMethodUsedTime[5]++;
    }

    public override void UpdateState(LongHairFSM StateManager)
    {
        if (!StateManager.EnemyState.AttackIng)
        {
      //      Debug.Log(StateManager.gameObject.name + " end thron attack state");

            StateManager.StateSwitch(StateManager.Idle);
        }
    }
}
