using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongHairAttack : CommonAttack
{
    private EnemyState EnemyState;

    [Header("普通攻擊設定")]
    public float Atk3PreCast;

    [Header("地刺攻擊設定")]
    public float ThronBackSwing;
    public GameObject Thron;

    [Header("海膽攻擊設定")]
    public float UmiAttackRadious;
    public float UmiAttackBoxHeightOffset;
    public float UmiAttackBoxWidthOffset;
    public GameObject Umi;

    private Vector2 UmiAttackBoxPos;
    private Vector2 UmiAttackBoxSize;

    private void EnemyInitSet()
    {
        CommonState = this.GetComponent<EnemyState>();
        EnemyState = this.GetComponent<EnemyState>();

        CommonMove = this.GetComponent<CommonMove>();
        CommonAnimation = this.GetComponent<EnemyAnimation>();

        AttackStartTime = 0;
        CDStartTime = Time.time - EnemyState.AttackCD;
    }

    public void ComboAttack()
    {
        if (EnemyState.Combo == 3)
        {
            EnemyState.Combo = 0;
        }

        if (EnemyState.Combo == 2)
        {
            PreCast *= Atk3PreCast;
        }

        CallComboAttack(true);

        AttackStartTime = Time.time;

        CDStartTime = Time.time;
    }

    public IEnumerator ThronAttack(float XrayOffset)
    {
        CommonState.AttackAble = false;
        CommonState.AttackIng = true;

        LastAttackTime = Time.time;

        StartCoroutine(CommonMove.SuddenlyBrake(CommonState.AttackAniLength[5] * 0.75f));

        CommonAnimation.Animator.SetTrigger("AtkThron");

        Instantiate(Thron, new Vector2(XrayOffset, transform.position.y), Quaternion.identity);

        yield return new WaitForSecondsRealtime(EnemyState.AttackAniLength[5]);

        CommonState.AttackIng = false;
        CommonState.AttackAble = true;

        yield return new WaitForSecondsRealtime(BackSwing);
    }

    public void MutipleThronAttack()
    {

    }

    // thron animation index 0

    public IEnumerator UmiAttack()
    {
        yield return new WaitForSecondsRealtime(EnemyState.SpAttackAniLength[1]);
    }

    // Umi animation index 1


    private void EnemyAttackBoxUpdate()
    {
        AttackBoxSize = new Vector2(transform.lossyScale.x * AttackBoxWidth, transform.lossyScale.y * AttackBoxHeight);

        AttackBoxPos = new Vector2(transform.position.x + AttackBoxWidthOffset * EnemyState.MoveDirection,
                                     transform.position.y + AttackBoxHeightOffset);
    }
    void Start()
    {
        EnemyInitSet();
    }

    void FixedUpdate()
    {
        EnemyAttackBoxUpdate();


        if (Time.time > CDStartTime + EnemyState.AttackCD)
        {
            EnemyState.AttackAble = true;
        }
        else
        {
            EnemyState.AttackAble = false;
        }

        //if (EnemyState.AttackIng)
        //    DealDamage();


    }
}
