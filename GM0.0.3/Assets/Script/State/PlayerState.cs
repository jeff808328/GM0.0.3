using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : CommonState
{
    [Header("¸õÅD³]©w")]
    public int MaxJumpTime;
    public int JumpTime;
    public bool JumpAble;

    [Header("³sÀ»ª¬ºA")]
    public bool ComboAttackAble;
    public int Combo;

    private void PlayerInitValueSet()
    {
        MaxJumpTime = CharacterData.AirJumpTimes;
        JumpTime = 0;
        JumpAble = true;

        ComboAttackAble = false;
        Combo = 0;
    }

    void Start()
    {
        InitValueSet();

        PlayerInitValueSet();
    }


}
