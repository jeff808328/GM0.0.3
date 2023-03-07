using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : CommonState
{
    [Header("���a���A")]
    public bool PlayerInView;
    public bool PlayerInAttakRange;
    public bool PlayerInSpAttackRange;

    public bool PlayerOnGround;

    public int PlayerDistanceIndex;
    public bool PlayerNearing;

    [Header("�ۦ��Ѽ�")]
    public int[] AttackMethodUsedTime;
    public float[] SpAttackAniLength;

    [Header("��ʱ���")]
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
