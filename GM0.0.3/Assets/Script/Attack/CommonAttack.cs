using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonAttack : MonoBehaviour
{
    [Header("攻擊判定箱參數")]
    public LayerMask AttackAble;

    protected Vector2 AttackBoxPos;
    protected Vector2 AttackBoxSize;

    protected Rigidbody2D AttackDetection;

    public float AttackBoxHeight;
    public float AttackBoxWidth;
    public float AttackBoxHeightOffset;
    public float AttackBoxWidthOffset;

    [Header("攻擊硬直")]

    public float PreCast;
    protected float PreCastOri;
    public float BackSwing;
    protected float BackSwingOri;

    protected float AttackStartTime;
    public float CDStartTime;

    protected float LastAttackTime;
    public float AttackResetTime;

    #region Component

    [Header("組件")]
    public ChatacterData ChatacterData;
    protected CommonState CommonState;
    protected CommonMove CommonMove;
    protected CommonAnimation CommonAnimation;

    #endregion

    //  protected int AttackIndex;

    protected void BaseAttackBoxUpdate()
    {
        AttackBoxSize = new Vector2(transform.lossyScale.x * AttackBoxWidth, transform.lossyScale.y * AttackBoxHeight);

        AttackBoxPos = new Vector2(transform.position.x + AttackBoxWidthOffset * transform.lossyScale.x,
                                     transform.position.y + AttackBoxHeightOffset);
    }

    protected void CallComboAttack(bool UseSuddenlyBrake)
    {
        AttackStartTime = Time.time;

        CommonState.Combo++;
        CommonState.ComboIng = true;

   //     Debug.Log(CommonState.Combo);

     //   Debug.Log("call attack");
        //   StartCoroutine(AttackStateTrigger(UseSuddenlyBrake));

        StartCoroutine(Attack(UseSuddenlyBrake));

        //   Debug.Log("function work");
    }

    protected IEnumerator AttackStateTrigger(bool UseSuddenlyBrake)
    {
        CommonState.AttackIng = true;
        CommonState.AttackAble = false;

        Debug.Log(this.gameObject.name + "atk start" + CommonState.Combo.ToString());

        LastAttackTime = Time.time;

        if (UseSuddenlyBrake)
            StartCoroutine(CommonMove.SuddenlyBrake(CommonState.AttackAniLength[CommonState.Combo]));

        yield return new WaitForSecondsRealtime(CommonState.AttackAniLength[CommonState.Combo] * 0.75f);

        CommonState.AttackAble = true;
        CommonState.AttackIng = false;

        Debug.Log(this.gameObject.name + "atk end" + CommonState.Combo.ToString());

        yield return new WaitForSecondsRealtime(CommonState.AttackAniLength[CommonState.Combo] * 0.25f);

    }

    protected void DealDamage()
    {
        var AttackDetect = Physics2D.OverlapBoxAll(AttackBoxPos, AttackBoxSize, 0, AttackAble);

        if (AttackDetect != null)
        {
            //      Debug.Log("attack");

            foreach (var Attacked in AttackDetect)
            {
                StartCoroutine(Attacked.GetComponent<CommonHP>().Hurt(ChatacterData.Atk, this.transform.position, false));

                //      Debug.Log(Attacked.gameObject.name);
            }
        }
    }

    protected void ResetCombo()
    {
        if (Time.time > LastAttackTime + AttackResetTime)
        {
            CommonState.Combo = 0;
        //    Debug.Log("combo reset");
        }

    }
    // 過久沒有攻擊,則將combo重設為0


    protected IEnumerator Attack(bool UseSuddenlyBrake)
    {
        Debug.Log("function start");
        CommonState.AttackAble = false;
        CommonState.AttackIng = true;

        LastAttackTime = Time.time;

        if (UseSuddenlyBrake)
            StartCoroutine(CommonMove.SuddenlyBrake(CommonState.AttackAniLength[CommonState.Combo]*0.75f));

        CommonAnimation.Animator.SetTrigger("Atk" + CommonState.Combo.ToString());

        yield return new WaitForSecondsRealtime(PreCast);

        var AttackDetect = Physics2D.OverlapBoxAll(AttackBoxPos, AttackBoxSize, 0, AttackAble);

        foreach (var Attacked in AttackDetect)
        {
         //   Debug.Log(Attacked.gameObject.name);
            StartCoroutine(Attacked.GetComponent<CommonHP>().Hurt(ChatacterData.Atk, this.transform.position, false));
        }

        Debug.Log(CommonState.Combo);

        yield return new WaitForSecondsRealtime(CommonState.AttackAniLength[CommonState.Combo] - PreCast);

        CommonState.AttackIng = false;
        CommonState.AttackAble = true;

        PreCast = PreCastOri;

        yield return new WaitForSecondsRealtime(BackSwing);

        //      Debug.Log("function end");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawWireCube(AttackBoxPos, AttackBoxSize);
    }
}
