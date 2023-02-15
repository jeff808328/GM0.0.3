using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : CommonMove
{
    private PlayerState PlayerState;
    private CommonAnimation CommonAnimation;

    private float LastMoveAni;

    void Start()
    {
        InitValueSet();

        InitComponmentSet();

        PlayerInitComponentSet();
    }

    void Update()
    {
        if (PlayerState.MoveAble)
        {
            if (PlayerState.ActionLayerNow < 3)
            {
                if (Input.GetKeyDown(KeyCode.R) & PlayerState.RollAble)
                {
                    Roll(LastMoveDirection, PlayerState.RollAniLength, CharacterData.MaxMoveSpeed * DashAdjust);
                }
            }

            if (PlayerState.ActionLayerNow <= 1)
            {
                if (Input.GetKeyDown(KeyCode.J) & PlayerState.JumpTime < PlayerState.MaxJumpTime)
                {
                    Jump();
                }

                if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) & PlayerState.GroundTouching)
                {
                    Run(-1);

                    if (Mathf.Abs(HorizonSpeed) < HorizonSpeedMax * 0.5f & LastMoveAni + PlayerState.RunAniLength < Time.time)
                    {
                        LastMoveAni = Time.time;
                        CommonAnimation.PlayAnimation(1, 0, 0.4f);
                    }
                    else if (Mathf.Abs(HorizonSpeed) > HorizonSpeedMax * 0.5f & LastMoveAni + PlayerState.RunAniLength < Time.time)
                    {
                        LastMoveAni = Time.time;
                        CommonAnimation.PlayAnimation(2, 0.2f, 0.5f);
                    }
                }
                else if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) & PlayerState.GroundTouching)
                {
                    Run(1);

                    if (Mathf.Abs(HorizonSpeed) < HorizonSpeedMax & LastMoveAni + PlayerState.RunAniLength < Time.time)
                    {
                        LastMoveAni = Time.time;
                        CommonAnimation.PlayAnimation(1, 0, 0.4f);
                    }
                    else if (Mathf.Abs(HorizonSpeed) > HorizonSpeedMax  & LastMoveAni + PlayerState.RunAniLength < Time.time)
                    {
                        LastMoveAni = Time.time;
                        CommonAnimation.PlayAnimation(2, 0.2f, 0.5f);
                    }
                }
                else
                {
                    CommonAnimation.PlayAnimation(0, 0, 1.3f);
                    Brake();
                    PlayerState.Moveing = false;
                }


            }
        }

        if (!PlayerState.GroundTouching)
        {
            PlayerState.ActionLayerNow = 1;
            GravityAdjust = OriGravityAdjust;
        }
        else
        {
            PlayerState.JumpTime = 0;
            GravityAdjust = 0;
        }

        Gravity();

        FinalMoveSpeed = new Vector2(HorizonSpeed, VerticalSpeed);
        Rd.velocity = FinalMoveSpeed;
    }

    private void PlayerInitComponentSet()
    {
        CommonState = this.GetComponent<PlayerState>();
        PlayerState = this.GetComponent<PlayerState>();

        CommonAnimation = this.GetComponent<CommonAnimation>();

        LastMoveAni = Time.time;
    }

    private void Jump()
    {
        PlayerState.ActionLayerNow = 1;

        VerticalSpeed = VerticalSpeedMax;

        PlayerState.JumpTime++;
    }

}
