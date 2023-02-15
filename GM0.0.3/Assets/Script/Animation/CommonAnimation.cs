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
    }

}
