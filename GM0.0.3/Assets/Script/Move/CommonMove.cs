using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonMove : MonoBehaviour
{
    #region 水平速度控制

    private float HorizonSpeedMax = 0; //速度上限
    public float HorizonSpeed = 0; // 運算用 & 當前值

    public float AddSpeedAdjust; // 移動速度控制, 直接用 Time.DeltaTime 值太小
    private float OriginAddSpeedAdjust;

    [SerializeField] protected float MinusSpeedAdjust; // 移動速度控制, 直接用 Time.DeltaTime 值太小
    private float OriginMinusSpeedAdjust;

    private float AddSpeed; // 加速度初始值
    private float MinusSpeed; // 減速度初始值   

    protected int LastMoveDirection; // 上次的移動方向 1朝右 -1朝左
    #endregion

    #region 垂直速度控制
    protected float VerticalSpeedMax = 0; //速度上限
    public float VerticalSpeed = 0; // 運算用 & 當前值

    private float GravityValue; // 重力初始值
    public float GravityAdjust; // 重力調整值 
    public float GravityMax; // 重力最大值

    protected int MaxJumpTimes; // 最大跳躍次數

    private bool GroundTouching; // 地板偵測

    protected int JumpTime;
    #endregion

    #region 速度控制

    [SerializeField] protected Vector2 FinalMoveSpeed;

    private float BeforeDashSpeed;
    private float BeforeDahsMoveDirection;

    #endregion 

    #region 組件

    public ChatacterData CharacterData;
    protected Rigidbody2D Rd;
    protected CommonState CommonState;

    #endregion

    protected void InitValueSet()
    {
        LastMoveDirection = 0;

        HorizonSpeedMax = CharacterData.MaxMoveSpeed;
        VerticalSpeedMax = CharacterData.JumpSpeed;

        AddSpeed = CharacterData.AddSpeed;
        MinusSpeed = CharacterData.MinusSpeed;

        GravityValue = CharacterData.Gravity;

        OriginAddSpeedAdjust = AddSpeedAdjust;
        OriginMinusSpeedAdjust = MinusSpeedAdjust;
    }

    protected void InitComponmentSet()
    {
        Rd = this.GetComponent<Rigidbody2D>();
    }

    protected void Run(int Direction) // 加速 // 在可操控情況下可用
    {
        CommonState.ActionLayerNow = 1;

        if (Direction != LastMoveDirection)
            Flip(Direction);

        HorizonSpeed += AddSpeed * Direction * Time.deltaTime * AddSpeedAdjust; // v = v0 + at*調整值
        HorizonSpeed = Mathf.Clamp(HorizonSpeed, -HorizonSpeedMax, HorizonSpeedMax); // 避免超過速度上限

        LastMoveDirection = Direction; // 紀錄當前移動方向,轉向和減速用
    }

    protected void Flip(int Direction) // 翻面 // Run的備註
    {
        if (Direction >= 0)
        {
            this.transform.localScale = new Vector3(1, 1, 1);
        //    this.transform.eulerAngles = new Vector3(135, 0, 0);
        }
        else
        {
            this.transform.localScale = new Vector3(-1, 1, 1);
       //     this.transform.eulerAngles = new Vector3(55, 0, 0);
        }
    }

    protected void Brake() // 在玩家無輸入且非無敵狀況時可用
    {
        CommonState.ActionLayerNow = 0;

        HorizonSpeed -= MinusSpeed * LastMoveDirection * Time.deltaTime * MinusSpeedAdjust;

        if (LastMoveDirection == 1)
        {
            HorizonSpeed = Mathf.Clamp(HorizonSpeed, 0, HorizonSpeedMax);
        }
        else if (LastMoveDirection == -1)
        {
            HorizonSpeed = Mathf.Clamp(HorizonSpeed, -HorizonSpeedMax, 0);
        }
    }

    protected void Jump()
    {
        CommonState.ActionLayerNow = 1;

        VerticalSpeed = VerticalSpeedMax;
    }

    protected void Gravity()
    {
        VerticalSpeed -= GravityValue * Time.deltaTime * GravityAdjust;

        VerticalSpeed = Mathf.Clamp(VerticalSpeed, GravityMax, VerticalSpeedMax);
    }

    protected IEnumerator Roll(int Direction, float Length, float Speed)
    {
        CommonState.IsUnbreakable = true;
        CommonState.ActionLayerNow = 3;
        CommonState.AttackAble = false;
        CommonState.MoveAble = false;

        HorizonSpeedMax *= 2;

        BeforeDashSpeed = HorizonSpeed;
        HorizonSpeed = Speed;

        yield return new WaitForSecondsRealtime(Length);

        if(LastMoveDirection == Direction)
        {
            HorizonSpeed = BeforeDashSpeed;
        }
        else
        {
            HorizonSpeed = 0;
        }

        HorizonSpeedMax = CharacterData.MaxMoveSpeed;

        CommonState.AttackAble = true;
        CommonState.MoveAble = true;
        CommonState.ActionLayerNow = 0;
        CommonState.IsUnbreakable = false;
    }

    protected IEnumerator SuddenlyBrake(float Length)
    {
        HorizonSpeed = 0;
        AddSpeed = 0;

        yield return new WaitForSecondsRealtime(Length);

        AddSpeed = CharacterData.AddSpeed;
    }

    protected IEnumerator Lock(float Length)
    {
        GravityValue = 0;

        yield return new WaitForSecondsRealtime(Length);

        GravityValue = CharacterData.Gravity;
    }
}
