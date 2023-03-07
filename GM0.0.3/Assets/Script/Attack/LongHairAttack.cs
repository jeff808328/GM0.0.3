using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongHairAttack : CommonAttack
{
    private EnemyState EnemyState;

    [Header("¦a¨ë§ðÀ»§P©w")]
    public float ThronAttackBoxHeight;
    public float ThronAttackBoxWidth;
    public float ThronAttackBoxHeightOffset;
    public float ThronAttackBoxWidthOffset;

    private Vector2 ThronAttackBoxPos;
    private Vector2 ThronAttackBoxSize;

    [Header("®üÁx§ðÀ»§P©w")]
    public float UmiAttackBoxHeight;
    public float UmiAttackBoxWidth;
    public float UmiAttackBoxHeightOffset;
    public float UmiAttackBoxWidthOffset;

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

    public void CallCommonAttack()
    {
        if (EnemyState.Combo >= 3)
        {
            EnemyState.Combo = 0;
        }
        else
        {
            EnemyState.Combo++;
        }

        if (EnemyState.AttackAble)
        {
            Attack(true);
        }
    }

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
    }
}
