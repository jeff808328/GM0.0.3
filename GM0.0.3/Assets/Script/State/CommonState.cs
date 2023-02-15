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

    [Header("�����]�w")]
    public bool AttackAble;
    public bool AttackIng;
    public float AttackCD;
    public float AttackAniLength;

    [Header("½�u�]�w")]
    public float RollCD;
    public float RollAniLength;
    public bool RollAble;
    public bool Rolling;

    [Header("���ʳ]�w")]
    public bool MoveAble;
    public bool Moveing;
    public float RunAniLength;

    protected void InitValueSet()
    {
        ActionLayerNow = 0;

        GroundTouching = false;
        WallTouching = false;

        IsUnbreakable = false;

        AttackAble = true;
        AttackIng = false;

        RollAble = true;
        Rolling = false;

        MoveAble = true;
        Moveing = false;
    }

    public void InitComponmentSet()
    {

    }

    public IEnumerator Roll()
    {
        yield return new WaitForSecondsRealtime(RollCD);
    }

    public IEnumerator Attack()
    {
        yield return new WaitForSecondsRealtime(AttackCD);
    }
}
