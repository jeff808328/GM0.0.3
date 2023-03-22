using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : CommonMove
{
    private EnemyState EnemyState;

    void Start()
    {
        InitComponmentSet();
        InitValueSet();

        EnemyState = this.GetComponent<EnemyState>();
        CommonState = this.GetComponent<EnemyState>();

        DashCDStartTime = Time.time - EnemyState.RollCD;
    }

    void Update()
    {
        if (Time.time < DashCDStartTime + EnemyState.RollCD)
        {
            EnemyState.RollAble = false;
        }
        else
        {
            EnemyState.RollAble = true;
        }


        if (EnemyState.MoveAble)
        {
            if (EnemyState.MoveDirection != 0)
            {
                Run(EnemyState.MoveDirection);
            }
            else
            {
                Brake();
            }

        }

        if (!EnemyState.GroundTouching)
        {
            EnemyState.ActionLayerNow = 1;
            GravityMax = -20;
            AddSpeedAdjust *= 0.5f;
        }
        else
        {
            AddSpeedAdjust = OriginAddSpeedAdjust;
            GravityMax = 0;
        }

        Gravity();

        FinalMoveSpeed = new Vector2(HorizonSpeed, VerticalSpeed);
        Rd.velocity = FinalMoveSpeed;
    }
}
