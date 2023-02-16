using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : CommonState
{

    [Header("³sÀ»ª¬ºA")]
    public bool ComboAttackAble;
    public int Combo;

    private void PlayerInitValueSet()
    {
        ComboAttackAble = false;
        Combo = 0;
    }

    void Start()
    {
        InitValueSet();

        PlayerInitValueSet();
    }


}
