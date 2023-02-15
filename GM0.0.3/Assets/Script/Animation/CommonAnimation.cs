using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonAnimation : MonoBehaviour
{
    private Animator Animator;
    public string[] AnimationName;

    void Start()
    {
        Animator = GetComponentInChildren<Animator>();

        PlayAnimation(0, 0, 1.3f);
    }



    public void PlayAnimation(int AnimationIndex, float TransDuration, float AnimationLength)
    {
        Animator.CrossFade(AnimationName[AnimationIndex], TransDuration, 0, AnimationLength);
    }
}
