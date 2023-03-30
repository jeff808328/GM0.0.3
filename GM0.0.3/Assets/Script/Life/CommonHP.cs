using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommonHP : MonoBehaviour
{
    public ChatacterData ChatacterData;
    protected CommonMove CommonMove;
    protected CommonState CommonState;
    protected CommonAnimation CommonAnimation;
    protected CommonAudioManager CommonAudioManager;

    [Header("傷害參數")]

    [SerializeField] protected float Hp;

    [SerializeField] protected float Def;
    [SerializeField] protected float DamageAdjsut;

    [Header("擊飛動畫參數")]
    public float HvRollSpeed;
    public float LtRollSpeed;

    [Header("HP bar")]
    public Image BarImage;
  

    private float LastHurtTime;
    private int RollDirection;

    protected void InitValueSet()
    {
        Hp = ChatacterData.HP;
        Def = ChatacterData.Def;

        DamageAdjsut = 1f;
    }

    public IEnumerator Hurt(float AttackerAtk, Vector2 AttackerPos, bool HeavyDamage)
    {
        CommonState.MoveAble = false;
        CommonState.AttackAble = false;
        CommonState.Hurting = true;

        Hp -= AttackerAtk * DamageAdjsut;
        BarImage.fillAmount = Hp * 0.01f;

        DieCheck();

        RollDirection = ((AttackerPos.x > transform.position.x) ? -1 : 1);

        StartCoroutine(DamageControl(0, CommonState.UnbreakableLength));

        if (HeavyDamage & Time.time > LastHurtTime + CommonState.HeavyHurtAniLength) // 避免重複觸發受傷動畫,待優化,也許跟isunbraekable混在一起用
        {
         //   Debug.Log(this.gameObject.name + " is heavy hurt");

            LastHurtTime = Time.time + CommonState.HeavyHurtAniLength;

            CommonAnimation.Animator.SetTrigger("HeavyHurt");
            CommonAudioManager.CallHurt(1);

            StartCoroutine(DamageControl(0, CommonState.HeavyHurtAniLength));
            StartCoroutine(CommonMove.Roll(RollDirection, CommonState.HeavyHurtAniLength, HvRollSpeed));

            yield return new WaitForSecondsRealtime(CommonState.HeavyHurtAniLength);
        }

        else if (Time.time > LastHurtTime + 0.05f)
        {
        //    Debug.Log(this.gameObject.name + " is light hurt");

            LastHurtTime = Time.time + CommonState.LightHurtAniLength;

            CommonAnimation.Animator.SetTrigger("LightHurt");
            CommonAudioManager.CallHurt(0);

            StartCoroutine(DamageControl(0, CommonState.LightHurtAniLength));
            StartCoroutine(CommonMove.Roll(RollDirection, CommonState.LightHurtAniLength, LtRollSpeed));

            yield return new WaitForSecondsRealtime(CommonState.LightHurtAniLength);
        }

        CommonState.Hurting = false;
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
            StartCoroutine(Die());
            CommonState.Aliving = false;
        }
        else
        {
            CommonState.Aliving = true;
        }
    }

    private IEnumerator Die()
    {
        CommonAudioManager.CallDeath();

        yield return new WaitForSecondsRealtime(CommonState.DieAniLength);

        Destroy(this.gameObject);
    }

}
