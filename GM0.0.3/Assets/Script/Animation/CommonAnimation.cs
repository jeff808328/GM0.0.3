using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonAnimation : MonoBehaviour
{
    private Animator Animator;
    protected CommonState CommonState;

    protected void InitComponentSet()
    {
        Animator = GetComponentInChildren<Animator>();
    }

    protected void CommonUpdate()
    {
        if (CommonState.GroundTouching)
            Animator.SetBool("Moving", CommonState.Moveing);

        if (!CommonState.GroundTouching & !CommonState.Hurting)
            Animator.SetTrigger("Jump");
    }

}
