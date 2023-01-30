using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonState : MonoBehaviour
{
    // For control action layer and other state

    // three level of action layer

    // 3. Dash,Hurt

    // 2. Attack

    // 1. Run,Jump

    // 0. Idle

    [Header("SO")]
    public ChatacterData ChatacterData;

    [Header("��e�ʧ@����")]
    public int ActionLayerNow;

    [Header("�� & �a�O�I���P�w")]
    public bool GroundTouching;
    public bool WallTouching;

    [Header("�L�ĳ]�w")]
    public bool IsUnbreak;
    public float UnbreakLength;

    [Header("�����]�w")]
    public bool AttackAble;
    public bool AttackIng;
    public float AttackCD;
    public float AttackAniLength;

    protected void InitValueSet()
    {
        ActionLayerNow = 0;

        GroundTouching = false;
        WallTouching = false;

        IsUnbreak = false;
    }

    public void InitComponmentSet()
    {

    }
}
