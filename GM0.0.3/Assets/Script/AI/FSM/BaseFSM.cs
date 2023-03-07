using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseFSM : MonoBehaviour
{
    protected EnemyState EnemyState;
    protected EnemyDetect EnemyDetect;
    protected EnemyMove EnemyMove;
    protected EnemyAttack EnemyAttack;
    protected EnemyAnimation EnemyAnimation;
    protected EnemyHP EnemyHP;

    protected float LastFlipTime;

    protected void BaseInitSet()
    {
        EnemyState = this.GetComponent<EnemyState>();   
        EnemyDetect = this.GetComponent<EnemyDetect>();
        EnemyMove = this.GetComponent<EnemyMove>();
        EnemyAttack = this.GetComponent<EnemyAttack>();
        EnemyAnimation = this.GetComponent<EnemyAnimation>();
        EnemyHP = this.GetComponent<EnemyHP>();

        LastFlipTime = Time.time - EnemyState.FlipCD;
    }
}
