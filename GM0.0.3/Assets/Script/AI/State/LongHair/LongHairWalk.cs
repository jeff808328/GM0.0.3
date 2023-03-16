using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongHairWalk : LongHairBaseState
{
    public override void EnterState(LongHairFSM StateManager)
    {
      //  Debug.Log(StateManager.gameObject.name + " is in walk state");
    }

    public override void UpdateState(LongHairFSM StateManager)
    {
        if (StateManager.EnemyState.PlayerInView)
            StateManager.EnemyState.MoveAble = true;
        else
            StateManager.EnemyState.MoveAble = false;


        HurtTrigger(StateManager);

        AttackTrigger(StateManager);

        SPAttackTrigger(StateManager);

        FlipTrigger(StateManager);
    }
}
