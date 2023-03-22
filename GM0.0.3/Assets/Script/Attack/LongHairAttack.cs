using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongHairAttack : CommonAttack
{
    private EnemyState EnemyState;

    [Header("地刺攻擊判定")]
    public float ThronAttackBoxHeight;
    public float ThronAttackBoxWidth;
    public float ThronAttackBoxHeightOffset;
    public GameObject Thron;

    private Vector2 ThronAttackBoxPos;
    private Vector2 ThronAttackBoxSize;

    [Header("海膽攻擊判定")]
    public float UmiAttackBoxHeight;
    public float UmiAttackBoxWidth;
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
        if (EnemyState.Combo >= 3)
        {
            EnemyState.Combo = 0;

        }
        else
        {
            EnemyState.Combo++;
        }

        CallComboAttack(true);

        AttackStartTime = Time.time;

        CDStartTime = Time.time;
    }

    public IEnumerator ThronAttack(float XrayOffset)
    {
        CommonState.AttackAble = false;
        CommonState.AttackIng = true;

        ThronAttackBoxSize = new Vector2(ThronAttackBoxWidth, ThronAttackBoxHeight);

        ThronAttackBoxPos = new Vector2(transform.position.x + XrayOffset, transform.position.y + ThronAttackBoxHeightOffset);

        yield return new WaitForSecondsRealtime(EnemyState.SpAttackAniLength[0]); // 等待生長的時間

        var AttackDetect = Physics2D.OverlapBoxAll(ThronAttackBoxPos, ThronAttackBoxSize, 0, AttackAble);

        foreach (var Attacked in AttackDetect)
        {
            StartCoroutine(Attacked.GetComponent<CommonHP>().Hurt(ChatacterData.Atk, this.transform.position, true));
        }

        CommonState.AttackIng = false;
        CommonState.AttackAble = true;
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
    void Start()
    {
        EnemyInitSet();
    }

    void Update()
    {
        BaseAttackBoxUpdate();

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

        ResetCombo();
    }
}
