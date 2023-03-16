using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongHairMutipleThron : LongHairBaseState
{
    public override void EnterState(LongHairFSM StateManager)
    {
     //   Debug.Log(StateManager.gameObject.name + " start boss skill state");

        StateManager.EnemyAttack.MutipleThronAttack();
    } 

    public override void UpdateState(LongHairFSM StateManager)
    {
        if(!StateManager.EnemyState.AttackIng)
        {
      //      Debug.Log(StateManager.gameObject.name + " end boss skill state");

            StateManager.StateSwitch(StateManager.Idle);
        }
    }
}
