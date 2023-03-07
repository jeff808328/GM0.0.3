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
    }

    // Update is called once per frame
    void Update()
    {
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
