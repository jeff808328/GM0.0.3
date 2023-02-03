using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : CommonState
{
    [Header("翻滾設定")]
    public float RollCD;
    public float RollAniLength;
    public bool RollAble;
    public bool Rolling;

    [Header("跳躍設定")]
    public int MaxJumpTime;
    public int JumpTime;
    public bool JumpAble;



    private void PlayerInitValueSet()
    {
        RollAble = true;
        Rolling = false;

        MaxJumpTime = CharacterData.AirJumpTimes;
        JumpAble = true;
    }

    void Start()
    {
        InitValueSet();

        PlayerInitValueSet();
    }


}
