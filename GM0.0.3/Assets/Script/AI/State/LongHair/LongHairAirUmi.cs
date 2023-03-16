using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongHairAirUmi : LongHairBaseState
{
    public override void EnterState(LongHairFSM StateManager)
    {
     //   Debug.Log(StateManager.gameObject.name + " start air umi attack state");

        StateManager.EnemyMove.Jump();
        StateManager.EnemyAttack.UmiAttack();

        StateManager.EnemyState.AttackMethodUsedTime[3]++;
    }

    public override void UpdateState(LongHairFSM StateManager)
    {
        if (!StateManager.EnemyState.AttackIng)
        {
       //     Debug.Log(StateManager.gameObject.name + " end air umi attack state");

            StateManager.StateSwitch(StateManager.Idle);
        }
    }
}

