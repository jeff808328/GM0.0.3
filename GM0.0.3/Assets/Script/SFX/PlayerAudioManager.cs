using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioManager : CommonAudioManager
{
    public AudioClip Jump;
    public AudioClip[] Walk;
    public AudioClip Dash;

    public void CallJump()
    {
        AudioSource.clip = Jump;

        AudioSource.Play();
    }

    public void CallWalk(int Index)
    {
        AudioSource.clip = Walk[Index];

        AudioSource.Play();
    }

    public void CallDash()
    {
        AudioSource.clip = Dash;

        AudioSource.Play();
    }

}
