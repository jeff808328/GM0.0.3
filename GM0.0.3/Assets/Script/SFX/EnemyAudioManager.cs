using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudioManager : CommonAudioManager
{
    public AudioClip[] SpAttack;

    public void CallSpAtk(int Index)
    {
        AudioSource.clip = SpAttack[Index];

        AudioSource.Play();
    }

}
