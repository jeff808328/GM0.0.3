using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonAnimation : MonoBehaviour
{
    [HideInInspector] public Animator Animator;
    protected CommonState CommonState;

    protected void InitComponentSet()
    {
        Animator = GetComponentInChildren<Animator>();
    }

    protected void CommonUpdate()
    {

        Animator.SetBool("GroundTouching", CommonState.GroundTouching);


        Animator.SetBool("Rolling", CommonState.Rolling);

        if (CommonState.GroundTouching)
            Animator.SetBool("Moving", CommonState.Moveing);
    }

}
