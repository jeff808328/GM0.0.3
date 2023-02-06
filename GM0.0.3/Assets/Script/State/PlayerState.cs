using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : CommonState
{
    [Header("∏ı≈D≥]©w")]
    public int MaxJumpTime;
    public int JumpTime;
    public bool JumpAble;

    private void PlayerInitValueSet()
    {
        MaxJumpTime = CharacterData.AirJumpTimes;
        JumpTime = 0;
        JumpAble = true;
    }

    void Start()
    {
        InitValueSet();

        PlayerInitValueSet();
    }


}
