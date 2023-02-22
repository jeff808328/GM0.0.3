using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : CommonState
{

    [Header("³sÀ»ª¬ºA")]
    public int Combo;
    public bool ComboIng;

    private void PlayerInitValueSet()
    {
        Combo = 0;

        ComboIng = false;
    }

    void Start()
    {
        InitValueSet();

        PlayerInitValueSet();
    }


}
