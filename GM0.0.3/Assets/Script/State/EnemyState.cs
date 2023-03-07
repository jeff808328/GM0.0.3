using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : CommonState
{
    [Header("玩家狀態")]
    public bool PlayerInView;
    public bool PlayerInAttakRange;
    public bool PlayerInSpAttackRange;

    public bool PlayerOnGround;

    public int PlayerDistanceIndex;
    public bool PlayerNearing;

    [Header("招式參數")]
    public int[] AttackMethodUsedTime;
    public float[] SpAttackAniLength;

    [Header("行動控制")]
    public float ReactionTime;
    public float FlipCD;
    public int MoveDirection;
    public bool NearingWall;
    public int ActionIndex;

    void Start()
    {
        InitValueSet();

        MoveDirection = (int)transform.localScale.x;
    }
   
    void Update()
    {
        
    }
}
