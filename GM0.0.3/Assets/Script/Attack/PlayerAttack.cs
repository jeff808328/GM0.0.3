using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : CommonAttack
{
    private PlayerState PlayerState;

    // 0 -> idle, 1 -> atk1, .......
  
    void Start()
    {
        PlayerState = this.GetComponent<PlayerState>();
        CommonState = this.GetComponent<PlayerState>();
        CommonMove = this.GetComponent<PlayerMove>();
        CommonAnimation = this.GetComponent<PlayerAnimation>();

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

          //  Debug.Log("CD end");
        }

        if (Input.GetKeyDown(KeyCode.J))
        {

            if (!PlayerState.GroundTouching)
            {
                PlayerState.Combo = 1;

                PlayerState.AttackCD = PlayerState.AttackCDOri * 0.3f;

                CallComboAttack(true);

                PlayerState.Combo = 4;
            }

            if (PlayerState.Rolling)
            {
                PlayerState.Combo = 2;

                PlayerState.AttackCD = PlayerState.AttackCDOri * 0.3f;

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

            PlayerState.AttackCD = PlayerState.AttackCDOri;

            CDStartTime = Time.time;

            PlayerState.Combo = 0;

            Debug.Log("CD start");
        }
        else
        {
            PlayerState.AttackAble = true;
        }
    }


    //也許可以試試在attacking時 持續對範圍內的敵人造成傷害的寫法


}
