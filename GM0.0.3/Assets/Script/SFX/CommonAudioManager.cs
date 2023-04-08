using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonAudioManager : MonoBehaviour
{
    public AudioClip Death;
    public AudioClip[] Attack;
    public AudioClip[] Hurt; // 0,light 1,heavy

    [HideInInspector] public AudioSource AudioSource;

    void Start()
    {
        AudioSource = this.gameObject.GetComponent<AudioSource>();
    }

    public void CallDeath()
    {
        AudioSource.clip = Death;

        AudioSource.Play();
    }

    public void CallAttack(int Index)
    {
        AudioSource.clip = Attack[Index];

        AudioSource.Play();
    }


    public void CallHurt(int Index)
    {
        AudioSource.clip = Hurt[Index];

        AudioSource.Play();
    }
}
