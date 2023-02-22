using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonHP : MonoBehaviour
{
    public ChatacterData ChatacterData;
    protected CommonMove CommonMove;
    protected CommonState CommonState;
    protected CommonAnimation CommonAnimation;

    [Header("端甡把计")]

    [SerializeField] protected float Hp;
    [SerializeField] protected float Def;
    [SerializeField] protected float DamageAdjsut;

    [Header("阑笆礶把计")]
    public float HvRollSpeed;
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
        Hp -= AttackerAtk * DamageAdjsut;

        DieCheck();

        RollDirection = ((AttackerPos.x > transform.position.x) ? -1 : 1);

        StartCoroutine(DamageControl(0, CommonState.UnbreakableLength));

        CommonState.MoveAble = false;
        CommonState.AttackAble = false;

        if (HeavyDamage)
        {
            CommonAnimation.Animator.SetTrigger("HeavyHurt");

            yield return new WaitForSecondsRealtime(CommonState.HeavyHurtAniLength);

            StartCoroutine(CommonMove.Roll(RollDirection, CommonState.HeavyHurtAniLength, HvRollSpeed));
        }

        else
        {
            CommonAnimation.Animator.SetTrigger("LightHurt");

            yield return new WaitForSecondsRealtime(CommonState.LightHurtAniLength);

            StartCoroutine(CommonMove.Roll(RollDirection, CommonState.LightHurtAniLength, LtRollSpeed));
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
            Debug.Log(this.gameObject.name + "die");
            Destroy(this.gameObject);
        }
    }

}
