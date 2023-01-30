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

    [Header("當前動作階級")]
    public int ActionLayerNow;

    [Header("牆 & 地板碰撞判定")]
    public bool GroundTouching;
    public bool WallTouching;

    [Header("無敵設定")]
    public bool IsUnbreak;
    public float UnbreakLength;

    [Header("攻擊設定")]
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
