using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LongHairBaseState
{
    public abstract void EnterState(LongHairFSM StateManager);

    public abstract void UpdateState(LongHairFSM StateManager);

    protected void HurtTrigger(LongHairFSM StateManger)
    {
        if (StateManger.EnemyState.Hurting & !StateManger.EnemyState.IsUnbreakable)
        {
            StateManger.StateSwitch(StateManger.Hurt);
        }
    }

    protected void AttackTrigger(LongHairFSM StateManager)
    {
        //  Debug.Log("attack trigger");

        if (StateManager.EnemyState.PlayerInAttakRange)
        {
            //  Debug.Log("player in attack range");

            StateManager.EnemyState.MoveAble = false;


            if (StateManager.EnemyState.AttackAble &
                    Time.time > StateManager.EnemyAttack.CDStartTime + StateManager.EnemyState.AttackCD &
                         !StateManager.EnemyState.AttackIng)
            {
                StateManager.EnemyState.AttackMethodUsedTime[5] = 0;
                StateManager.StateSwitch(StateManager.Attack);
            }
        }
        else
        {
            StateManager.EnemyState.MoveAble = true;
        }

        if (StateManager.EnemyState.HPOri * 0.25f * (3 - StateManager.EnemyState.AttackMethodUsedTime[0]) > StateManager.EnemyState.HP)
        {
            StateManager.StateSwitch(StateManager.MultipleThron);
        }

        if (StateManager.EnemyState.PlayerDistanceIndex == 2 & StateManager.EnemyState.AttackAble & StateManager.EnemyState.AttackMethodUsedTime[5] < 2)
        {
          //  Debug.Log("call Thron");
            StateManager.StateSwitch(StateManager.SingleThron);
        }

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
            StateManager.CallBrake(0.1f);

            StateManager.EnemyState.MoveDirection *= -1;

            // StateManager.LastFlipTime = Time.time;

        //    Debug.Log("Flip because player not in view");
        }

        else if (StateManager.EnemyState.NearingWall & StateManager.LastFlipTime + StateManager.EnemyState.FlipCD * 3 < Time.time)
        {
            StateManager.EnemyState.MoveDirection *= -1;

            StateManager.LastFlipTime = Time.time;

       //     Debug.Log("Flip because nearing wall");
        }


    }
}
