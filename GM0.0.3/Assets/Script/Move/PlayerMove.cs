using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : CommonMove
{
    private PlayerState PlayerState; 

    void Start()
    {
        InitValueSet();

        InitComponmentSet();

        PlayerInitComponentSet();
    }

    void Update()
    {
        if(PlayerState.MoveAble)
        {
            if (PlayerState.ActionLayerNow < 3)
            {
                if(Input.GetKeyDown(KeyCode.R) & PlayerState.RollAble)
                {
                    Roll(LastMoveDirection,PlayerState.RollAniLength,CharacterData.MaxMoveSpeed * DashAdjust);
                }
            }

            if(PlayerState.ActionLayerNow <= 1)
            {
                if(Input.GetKeyDown(KeyCode.Escape) & PlayerState.JumpTime < PlayerState.MaxJumpTime)
                {
                    Jump();
                }

                if((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) & PlayerState.GroundTouching)
                {
                    Run(-1);
                }
                else if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) & PlayerState.GroundTouching)
                {
                    Run(1);
                } 
                else
                {
                    Brake();
                    PlayerState.Moveing = false;
                }
            }
        }

        if(!PlayerState.GroundTouching)
        {
            PlayerState.ActionLayerNow = 1;
            
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
    }

    private void Jump()
    {
        PlayerState.ActionLayerNow = 1;

        VerticalSpeed = VerticalSpeedMax;

        PlayerState.JumpTime++;
    }

}
