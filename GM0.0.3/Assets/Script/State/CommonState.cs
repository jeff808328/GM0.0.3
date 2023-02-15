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

    [Header("攻擊設定")]
    public bool AttackAble;
    public bool AttackIng;
    public float AttackCD;
    public float AttackAniLength;

    [Header("翻滾設定")]
    public float RollCD;
    public float RollAniLength;
    public bool RollAble;
    public bool Rolling;

    [Header("移動設定")]
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
