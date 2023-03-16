using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongHairGroundUmi : LongHairBaseState
{
    public override void EnterState(LongHairFSM StateManager)
    {
     //   Debug.Log(StateManager.gameObject.name + " start ground umi attack state");

        StateManager.EnemyAttack.UmiAttack();

        StateManager.EnemyState.AttackMethodUsedTime[4]++;
    }

    public override void UpdateState(LongHairFSM StateManager)
    {
        if (!StateManager.EnemyState.AttackIng)
        {
       //     Debug.Log(StateManager.gameObject.name + " end ground umi attack state");

            StateManager.StateSwitch(StateManager.Idle);
        }
    }
}
