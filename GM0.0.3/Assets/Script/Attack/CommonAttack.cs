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
    public float BackSwing;

    protected float AttackStartTime;
    protected float CDStartTime;

    #region Component

    [Header("組件")]
    public ChatacterData ChatacterData;
    protected CommonState CommonState;
    protected CommonMove CommonMove;
    protected CommonAnimation CommonAnimation;

    #endregion

    protected int AttackIndex;

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

        CommonAnimation.Animator.SetTrigger("Atk" + CommonState.Combo.ToString());


        StartCoroutine(Attack(UseSuddenlyBrake));

        //   Debug.Log("function work");
    }



    protected IEnumerator Attack(bool UseSuddenlyBrake)
    {
        Debug.Log("function start");

        CommonState.AttackIng = true;

        if (UseSuddenlyBrake)
            StartCoroutine(CommonMove.SuddenlyBrake(CommonState.AttackAniLength[AttackIndex]));

        yield return new WaitForSecondsRealtime(PreCast);

        var AttackDetect = Physics2D.OverlapBoxAll(AttackBoxPos, AttackBoxSize, 0, AttackAble);

        yield return new WaitForSecondsRealtime(CommonState.AttackAniLength[AttackIndex]);

        foreach (var Attacked in AttackDetect)
        {
            Attacked.GetComponent<CommonHP>().Hurt(ChatacterData.Atk, this.transform.position, false);
        }

        yield return new WaitForSecondsRealtime(BackSwing);

        CommonState.AttackIng = false;

        Debug.Log("function end");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawWireCube(AttackBoxPos, AttackBoxSize);
    }
}
