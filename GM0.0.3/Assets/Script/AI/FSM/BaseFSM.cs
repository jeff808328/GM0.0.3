using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseFSM : MonoBehaviour
{
    [HideInInspector] public EnemyState EnemyState;
    [HideInInspector] public EnemyDetect EnemyDetect;
    [HideInInspector] public EnemyMove EnemyMove;
    [HideInInspector] public EnemyAnimation EnemyAnimation;
    [HideInInspector] public EnemyHP EnemyHP;

    [HideInInspector] public float LastFlipTime;

    protected void BaseInitSet()
    {
        EnemyState = this.GetComponent<EnemyState>();   
        EnemyDetect = this.GetComponent<EnemyDetect>();
        EnemyMove = this.GetComponent<EnemyMove>();
        EnemyAnimation = this.GetComponent<EnemyAnimation>();
        EnemyHP = this.GetComponent<EnemyHP>();

        LastFlipTime = Time.time - EnemyState.FlipCD;
    }
}
