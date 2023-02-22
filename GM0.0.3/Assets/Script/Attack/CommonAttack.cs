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

    #region Component

    public ChatacterData ChatacterData;
    protected CommonState CommonState;
    protected CommonMove CommonMove;

    #endregion

    protected int AttackIndex;

    protected void BaseAttackBoxUpdate()
    {
        AttackBoxSize = new Vector2(transform.lossyScale.x * AttackBoxWidth, transform.lossyScale.y * AttackBoxHeight);

        AttackBoxPos = new Vector2(transform.position.x + AttackBoxWidthOffset * transform.lossyScale.x,
                                     transform.position.y + AttackBoxHeightOffset);
    }

    protected IEnumerator Attack()
    {
        Debug.Log("function start");

        CommonState.AttackIng = true;

        StartCoroutine(CommonMove.SuddenlyBrake(CommonState.AttackAniLength[AttackIndex]));

     //   yield return new WaitForSecondsRealtime(PreCast);

        var AttackDetect = Physics2D.OverlapBoxAll(AttackBoxPos, AttackBoxSize, 0, AttackAble);

        yield return new WaitForSecondsRealtime(CommonState.AttackAniLength[AttackIndex]);

        foreach(var Attacked in AttackDetect)
        {
            Attacked.GetComponent<CommonHP>().Hurt(ChatacterData.Atk,this.transform.position,false);
        }

    //    yield return new WaitForSecondsRealtime(BackSwing);

        CommonState.AttackIng = false;

        Debug.Log("function end");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawWireCube(AttackBoxPos, AttackBoxSize);
    }
}
