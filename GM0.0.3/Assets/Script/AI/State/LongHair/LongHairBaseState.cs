using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LongHairBaseState
{
    public abstract void EnterState(LongHariFSM StateManager);

    public abstract void UpdateState(LongHariFSM StateManager);

    protected void AttackTrigger()
    {

    }

    protected void FlipTrigger()
    {

    }

    protected void SuddenlyActionTrigger()
    {

    }
}
