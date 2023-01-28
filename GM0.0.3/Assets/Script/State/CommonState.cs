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

    public ChatacterData ChatacterData;

    public int ActionLayerNow;

    public bool GroundTouching;
    public bool WallTouching;

    public bool IsInvincible;
    public float lnvincibleLength;

    protected void InitValueSet()
    {
        ActionLayerNow = 0;

        GroundTouching = false;
        WallTouching = false;

        IsInvincible = false;
    }

    public void InitComponmentSet()
    {

    }
}
