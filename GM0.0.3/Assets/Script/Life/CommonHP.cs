using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonHP : MonoBehaviour
{
    public ChatacterData ChatacterData;
    protected CommonMove CommonMove;
    protected CommonState CommonState;
    protected CommonAnimation CommonAnimation;

    [SerializeField] protected float Hp;
    [SerializeField] protected float Def;
    [SerializeField] protected float DamageAdjsut;

    public float HvRollLength;
    public float HvRollSpeed;
    public float LtRollLength;
    public float LtRollSpeed;

    private int RollDirection;

    protected void InitValueSet()
    {
        Hp = ChatacterData.HP;
        Def = ChatacterData.Def;

        DamageAdjsut = 1f;
    }

    public IEnumerator Hurt(float AttackerAtk, Vector2 AttackerPos, bool HeavyDamage)
    {
        Hp -= AttackerAtk;

        RollDirection = ((AttackerPos.x > transform.position.x) ? -1 : 1);

        StartCoroutine(DamageControl(0, CommonState.UnbreakableLength));

        CommonState.MoveAble = false;
        CommonState.AttackAble = false;

        if (HeavyDamage)
        {
            CommonAnimation.Animator.SetTrigger("HeavyHurt");

            yield return new WaitForSecondsRealtime(CommonState.HeavyHurtAniLength);

            StartCoroutine(CommonMove.Roll(RollDirection, HvRollLength, HvRollSpeed));
        }

        else
        {
            CommonAnimation.Animator.SetTrigger("LightHurt");

            yield return new WaitForSecondsRealtime(CommonState.LightHurtAniLength);

            StartCoroutine(CommonMove.Roll(RollDirection, LtRollLength, LtRollSpeed));
        }

        CommonState.MoveAble = true;
        CommonState.AttackAble = true;
    }

    public IEnumerator DamageControl(float Value, float Time)
    {
        DamageAdjsut = Value;

        CommonState.RollAble = false;
        CommonState.IsUnbreakable = true;

        yield return new WaitForSecondsRealtime(Time);

        CommonState.IsUnbreakable = false;
        CommonState.RollAble = true;

        DamageAdjsut = 1f;
    }

    private void DieCheck()
    {
        if (Hp < 0)
        {

        }
    }

}
