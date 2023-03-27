using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongHairAttack : CommonAttack
{
    private EnemyState EnemyState;

    [Header("���q�����]�w")]
    public float Atk3PreCast;

    [Header("�a������]�w")]
    public float ThronPreCast;
    public float ThronBackSwing;
    public GameObject Thron;

    [Header("���x�����]�w")]
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
        yield return new WaitForSecondsRealtime(ThronPreCast);

        yield return new WaitForSecondsRealtime(EnemyState.AttackAniLength[5]);

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
