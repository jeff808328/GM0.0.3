using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LongHairBaseState
{
    public abstract void EnterState(LongHairFSM StateManager);

    public abstract void UpdateState(LongHairFSM StateManager);

    protected void HurtTrigger(LongHairFSM StateManger)
    {
        if (StateManger.EnemyState.Hurting)
        {
            StateManger.StateSwitch(StateManger.Hurt);
        }
    }

    protected void AttackTrigger(LongHairFSM StateManager)
    {

        Debug.Log("attack trigger");


        if (StateManager.EnemyState.PlayerInAttakRange)
            StateManager.CallBrake(0.01f);

        //if (StateManager.EnemyState.HPOri * 0.25f * (3 - StateManager.EnemyState.AttackMethodUsedTime[0]) > StateManager.EnemyState.HP)
        //{
        //    StateManager.StateSwitch(StateManager.MultipleThron);
        //}

        //if (StateManager.EnemyState.PlayerInAttakRange & StateManager.EnemyState.AttackAble)
        //{
        //    StateManager.StateSwitch(StateManager.Attack);
        //}

        //if (StateManager.EnemyState.PlayerDistanceIndex == 2 & StateManager.EnemyState.AttackAble & StateManager.EnemyState.AttackMethodUsedTime[5] < 2)
        //{
        //    StateManager.StateSwitch(StateManager.SingleThron);
        //}


    }

    protected void SPAttackTrigger(LongHairFSM StateManager)
    {
        if (Time.time > StateManager.LastUmiAttackTime + StateManager.UmiCD)
        {
            if (StateManager.EnemyState.PlayerInSpAttackRange)
            {
                StateManager.StateSwitch(StateManager.GroundUmi);
            }

            if (StateManager.EnemyState.PlayerNearing & !StateManager.EnemyState.PlayerOnGround)
            {
                StateManager.StateSwitch(StateManager.AirUmi);
            }
        }
    }

    protected void FlipTrigger(LongHairFSM StateManager)
    {
        if (!StateManager.EnemyState.PlayerInView & StateManager.LastFlipTime + StateManager.EnemyState.FlipCD < Time.time)
        {
            StateManager.CallBrake(0.01f);

            StateManager.EnemyState.MoveDirection *= -1;

            StateManager.LastFlipTime = Time.time;

            Debug.Log("Flip");
        }

        if (StateManager.EnemyState.NearingWall & StateManager.LastFlipTime + StateManager.EnemyState.FlipCD < Time.time)
        {
            StateManager.CallBrake(0.01f);

            StateManager.EnemyState.MoveDirection *= -1;

            StateManager.LastFlipTime = Time.time;

            Debug.Log("Flip");
        }
    }

}
