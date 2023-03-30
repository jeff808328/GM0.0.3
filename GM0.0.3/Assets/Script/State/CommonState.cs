using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonState : MonoBehaviour
{
    // 控制動作階級用
    // three level of action layer
    // 3. Dash,Hurt
    // 2. Attack
    // 1. Run,Jump
    // 0. Idle

    // 狀態控制用,提供各腳本參考
    // 參數僅允許外部修改

    [Header("SO")]
    public ChatacterData CharacterData;

    [Header("當前動作階級")]
    public int ActionLayerNow;

    [Header("存活狀態顯示")]
    public bool Aliving;    

    [Header("牆 & 地板碰撞判定")]
    public bool GroundTouching;
    public bool WallTouching;

    [Header("無敵設定")]
    public bool IsUnbreakable;
    public float UnbreakableLength;

    [Header("受傷設定")]
    public bool Hurting;
    public float LightHurtAniLength;
    public float HeavyHurtAniLength;
    public float DieAniLength;

    [Header("攻擊設定")]
    public bool AttackAble;
    public bool AttackIng;
    public float AttackCD;
    [HideInInspector] public float AttackCDOri;
    public float[] AttackAniLength;

    [Header("連擊狀態")]
    public int Combo;
    public bool ComboIng;

    [Header("翻滾設定")]
    public float RollCD;
    public float RollAniLength;
    public bool RollAble;
    public bool Rolling;

    [Header("移動設定")]
    public bool MoveAble;
    public bool Moveing;
    public bool Locking;

    [Header("跳躍設定")]
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
