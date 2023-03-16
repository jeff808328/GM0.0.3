using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongHairHurt : LongHairBaseState
{
    private float StateStartTime;

    public override void EnterState(LongHairFSM StateManager)
    {
        //   Debug.Log(StateManager.gameObject.name + " start hurt state");

        StateStartTime = Time.time;

        // 注意 可能要協助關閉一些狀態
    }

    public override void UpdateState(LongHairFSM StateManager)
    {
        if(!StateManager.EnemyState.Hurting & Time.time > StateStartTime + 0.1f)
        {
       //     Debug.Log(StateManager.gameObject.name + " end hurt state");

            StateManager.StateSwitch(StateManager.Idle);
        }
    }
}
