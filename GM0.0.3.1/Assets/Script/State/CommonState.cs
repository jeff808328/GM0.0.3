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

    public int ActionLayerNow;

    public bool GroundTouching;
    public bool WallTouching;

    void Start()
    {
        InitValueSet();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InitValueSet()
    {
        ActionLayerNow = 0;

        GroundTouching = false;
        WallTouching = false;
    }

    public void InitComponmentSet()
    {

    }
}
