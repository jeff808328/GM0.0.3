using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonState : MonoBehaviour
{
    // ����ʧ@���ť�
    // three level of action layer
    // 3. Dash,Hurt
    // 2. Attack
    // 1. Run,Jump
    // 0. Idle

    // ���A�����,���ѦU�}���Ѧ�
    // �ѼƶȤ��\�~���ק�

    [Header("SO")]
    public ChatacterData CharacterData;

    [Header("��e�ʧ@����")]
    public int ActionLayerNow;

    [Header("�s�����A���")]
    public bool Aliving;    

    [Header("�� & �a�O�I���P�w")]
    public bool GroundTouching;
    public bool WallTouching;

    [Header("�L�ĳ]�w")]
    public bool IsUnbreakable;
    public float UnbreakableLength;

    [Header("���˳]�w")]
    public bool Hurting;
    public float LightHurtAniLength;
    public float HeavyHurtAniLength;
    public float DieAniLength;

    [Header("�����]�w")]
    public bool AttackAble;
    public bool AttackIng;
    public float AttackCD;
    [HideInInspector] public float AttackCDOri;
    public float[] AttackAniLength;

    [Header("�s�����A")]
    public int Combo;
    public bool ComboIng;

    [Header("½�u�]�w")]
    public float RollCD;
    public float RollAniLength;
    public bool RollAble;
    public bool Rolling;

    [Header("���ʳ]�w")]
    public bool MoveAble;
    public bool Moveing;
    public bool Locking;

    [Header("���D�]�w")]
    public int MaxJumpTime;
    public int JumpTime;
    public bool JumpAble;
    public bool Jumping;
    public float RaiseAniLength;
    public float FallAniLength;

    protected void InitValueSet()
    {
        ActionLayerNow = 0;

        GroundTouching = false;
        WallTouching = false;

        IsUnbreakable = false;

        AttackAble = true;
        AttackIng = false;
        AttackCDOri = AttackCD;
    //    LastAttackTime = Time.time - AttackCDOri;

        RollAble = true;
        Rolling = false;

        MoveAble = true;
        Moveing = false;

        MaxJumpTime = CharacterData.AirJumpTimes;
        JumpTime = 0;
        JumpAble = true;

        Combo = 0;
        ComboIng = false;
    }

}
