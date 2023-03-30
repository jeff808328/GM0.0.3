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
    public float UmiPreCast;
    public float UmiBackSwing;
    public GameObject Umi;
    private GameObject UmiCon;

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

        transform.GetChild(0).localEulerAngles = new Vector3(0, 180, 0);

        StartCoroutine(CommonMove.SuddenlyBrake(CommonState.AttackAniLength[5] * 0.75f));

        CommonAnimation.Animator.SetTrigger("AtkThron");

        Instantiate(Thron, new Vector2(XrayOffset, transform.position.y), Quaternion.identity);

        yield return new WaitForSecondsRealtime(EnemyState.AttackAniLength[5]);

        transform.GetChild(0).localEulerAngles = new Vector3(0, 90, 0);

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
        //  Debug.Log("Umiattack triggered");

        CommonState.AttackAble = false;
        CommonState.AttackIng = true;

        LastAttackTime = Time.time;

        yield return new WaitForSecondsRealtime(UmiPreCast);

        transform.GetChild(0).localEulerAngles = new Vector3(0, 180, 0);
        CommonAnimation.Animator.SetTrigger("AtkUmi");

        StartCoroutine(CommonMove.SuddenlyBrake(CommonState.AttackAniLength[4] * 0.75f));
        StartCoroutine(CommonMove.AntiGravity(CommonState.AttackAniLength[4] * 0.75f));

        UmiCon = Instantiate(Umi, new Vector2(transform.position.x, transform.position.y + 1f), Quaternion.identity);
        UmiCon.GetComponent<UmiDestory>().FollowTarget = this.gameObject;

        var AttackDetect = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y + 1f), UmiAttackRadious, 0, AttackAble);

        foreach (var Attacked in AttackDetect)
        {
            StartCoroutine(Attacked.GetComponent<CommonHP>().Hurt(10, this.transform.position, false));
            Debug.Log(Attacked.gameObject.name);
        }

        yield return new WaitForSecondsRealtime(EnemyState.AttackAniLength[4]);

        transform.GetChild(0).localEulerAngles = new Vector3(0, 90, 0);

        CommonState.AttackIng = false;
        CommonState.AttackAble = true;

        yield return new WaitForSecondsRealtime(UmiBackSwing);
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

    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawWireCube(AttackBoxPos, AttackBoxSize);

        Gizmos.color = Color.gray;

        Gizmos.DrawWireSphere(new Vector2(transform.position.x, transform.position.y + 1f), UmiAttackRadious);
    }
}
