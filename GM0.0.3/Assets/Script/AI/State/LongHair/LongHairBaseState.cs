using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LongHairBaseState
{
    public abstract void EnterState(LongHairFSM StateManager);

    public abstract void UpdateState(LongHairFSM StateManager);

    protected void HurtTrigger(LongHairFSM StateManger)
    {
        if(StateManger.EnemyState.Hurting)
        {
            StateManger.StateSwitch(StateManger.Hurt);
        }
    }

    protected void AttackTrigger(LongHairFSM StateManager)
    {
        if(StateManager.EnemyState.HPOri % StateManager.EnemyState.HP == 0)
        {
            StateManager.EnemyState.HP--;

            StateManager.StateSwitch(StateManager.MultipleThron);
        }

        if (StateManager.EnemyState.PlayerInAttakRange & StateManager.EnemyState.AttackAble)
        {
            StateManager.StateSwitch(StateManager.Attack);
        }

        if(StateManager.EnemyState.PlayerDistanceIndex == 2 & StateManager.EnemyState.AttackAble)
        {
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
            StateManager.EnemyState.MoveDirection *= -1;

            StateManager.LastFlipTime = Time.time;
        }
    }

}
