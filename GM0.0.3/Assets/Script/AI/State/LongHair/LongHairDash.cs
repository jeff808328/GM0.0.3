using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongHairDash : LongHairBaseState
{
    private float StateStartTime;

    public override void EnterState(LongHairFSM StateManager)
    {
        Debug.Log(StateManager.gameObject.name + " is in dash state");

        StateManager.EnemyAnimation.Animator.SetTrigger("Dash");

        StateManager.CallDash();

        StateStartTime = Time.time;
    }

    public override void UpdateState(LongHairFSM StateManager)
    {
        AttackTrigger(StateManager);

        SPAttackTrigger(StateManager);

        if (!StateManager.EnemyState.Rolling & Time.time > StateStartTime + 0.1f) 
        {
            StateManager.StateSwitch(StateManager.Idle);
        }

        HurtTrigger(StateManager);
    }
}
