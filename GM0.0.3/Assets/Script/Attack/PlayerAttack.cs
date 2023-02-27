using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : CommonAttack
{
    private PlayerState PlayerState;
    private PlayerAnimation PlayerAnimation;

    private float AttackStartTime;
    private float CDStartTime;
    public float ComboAdjust;

    // 0 -> idle, 1 -> atk1, .......

    void Start()
    {
        PlayerState = this.GetComponent<PlayerState>();
        CommonState = this.GetComponent<PlayerState>();
        CommonMove = this.GetComponent<PlayerMove>();
        PlayerAnimation = this.GetComponent<PlayerAnimation>();

        AttackStartTime = 0;
        CDStartTime = Time.time - PlayerState.AttackCD;
    }

    void Update()
    {
        BaseAttackBoxUpdate();

        if (Time.time < CDStartTime + PlayerState.AttackCD)
        {
            PlayerState.AttackAble = false;
        }
        else
        {
            PlayerState.AttackAble = true;

            Debug.Log("CD end");
        }

        if (Input.GetKeyDown(KeyCode.J))
        {

            if (!PlayerState.GroundTouching)
            {
                PlayerState.Combo = 1;

                CallComboAttack(true);

                PlayerState.Combo = 4;
            }

            if (PlayerState.Rolling)
            {
                PlayerState.Combo = 2;

                CallComboAttack(false);

                PlayerState.Combo = 4;
            }

            if (PlayerState.AttackAble & PlayerState.Combo < 4)
            {
                CallComboAttack(true);

                AttackStartTime = Time.time;

                //    Debug.Log("function work");
            }

            //  Debug.Log("button work");
        }


        if (PlayerState.Combo > 3 || Time.time > AttackStartTime + PlayerState.AttackAniLength[PlayerState.Combo] & PlayerState.AttackIng)
        {
            PlayerState.AttackAble = false;

            PlayerState.ComboIng = false;

            CDStartTime = Time.time;

            PlayerState.Combo = 0;

            Debug.Log("CD start");
        }
        else
        {
            PlayerState.AttackAble = true;
        }
    }


    private void CallComboAttack(bool UseSuddenlyBrake)
    {
        AttackStartTime = Time.time;

        PlayerState.Combo++;
        PlayerState.ComboIng = true;

        PlayerAnimation.Animator.SetTrigger("Atk" + PlayerState.Combo.ToString());


        StartCoroutine(Attack(UseSuddenlyBrake));

        //   Debug.Log("function work");
    }


}
