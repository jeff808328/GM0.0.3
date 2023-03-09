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
    [HideInInspector] public int[] AttackMethodHitTime;
    public float[] SpAttackAniLength;

    [Header("��ʱ���")]
    public float ReactionTime;
    public float FlipCD;
    public int MoveDirection;
    public bool NearingWall;
    

    [HideInInspector] public float HP;
    [HideInInspector] public float HPOri;


    void Start()
    {
        InitValueSet();

        MoveDirection = (int)transform.localScale.x;
    }
   
    void Update()
    {
        
    }
}
