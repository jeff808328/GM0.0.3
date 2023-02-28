using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetect : CommonDetect
{
    [Header("翻轉偵測")]
    public float FlipDetectLength;

    [Header("角色視野設定")]
    public LayerMask Player;

    public float ViewBoxHeight;
    public float ViewBoxWidth;
    public float ViewBoxHeightOffset;
    public float ViewBoxWidthOffset;

    public float ViewFocusLength; // 玩家進入視野後多少時間開始追蹤

    public Color ViewBoxColor;

    [Header("玩家距離計算")]
    public float MidMini; // 中間段最小值
    private float MidMiniOri;

    public float MidMax; // 中間段最大值
    private float MidMaxOri;

    public float RamdonValue; // 調整用亂數
    public float Buffer; // 中間段長度最小值

    private Vector3 SelfPos;
    private Vector3 MidMiniPos; // show中間段長度
    private Vector3 MidMaxPos;

    [Header("角色攻擊觸發設定")]
    public float AtkTriggerBoxHeight;
    public float AtkTriggerBoxWidth;
    public float AtkTriggerBoxHeightOffset;
    public float AtkTriggerBoxWidthOffset;

    public Color AtkTriggerBoxColor;

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
}
