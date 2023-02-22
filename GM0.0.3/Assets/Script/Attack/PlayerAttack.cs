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
        CDStartTime = 0;
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

                CallAttack();
            }
            
            if (PlayerState.Rolling)
            {
                PlayerState.Combo = 2;

                CallAttack();
            }

            if (PlayerState.AttackAble & PlayerState.Combo < 4)
            {
                CallAttack();

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


    private void CallAttack()
    {
        AttackStartTime = Time.time;

        PlayerState.Combo++;
        PlayerState.ComboIng = true;

        PlayerAnimation.Animator.SetTrigger("Atk" + PlayerState.Combo.ToString());
        StartCoroutine(Attack());

        //   Debug.Log("function work");
    }
}