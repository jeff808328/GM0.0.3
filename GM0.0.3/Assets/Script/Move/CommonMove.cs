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

    public ChatacterData ChatacterData;
    protected Rigidbody2D Rd;

    #endregion

    protected void InitValueSet()
    {
        LastMoveDirection = 0;

        HorizonSpeedMax = ChatacterData.MaxMoveSpeed;
        VerticalSpeedMax = ChatacterData.JumpSpeed;

        AddSpeed = ChatacterData.AddSpeed;
        MinusSpeed = ChatacterData.MinusSpeed;

        GravityValue = ChatacterData.Gravity;

        OriginAddSpeedAdjust = AddSpeedAdjust;
        OriginMinusSpeedAdjust = MinusSpeedAdjust;
    }

    protected void InitComponmentSet()
    {
        Rd = this.GetComponent<Rigidbody2D>();
    }

    protected void Run(int Direction)
    {
        if (Direction != LastMoveDirection)
            Flip(Direction);

        HorizonSpeed += AddSpeed * Direction * Time.deltaTime * AddSpeedAdjust; // v = v0 + at*調整值
        HorizonSpeed = Mathf.Clamp(HorizonSpeed, -HorizonSpeedMax, HorizonSpeedMax); // 避免超過速度上限

        LastMoveDirection = Direction; // 紀錄當前移動方向,轉向和減速用
    }

    protected void Flip(int Direction)
    {
        if (Direction >= 0)
        {
            this.transform.localScale = new Vector3(1, 1, 1);
            this.transform.eulerAngles = new Vector3(135, 0, 0);
        }
        else
        {
            this.transform.localScale = new Vector3(-1, 1, 1);
            this.transform.eulerAngles = new Vector3(55, 0, 0);
        }
    }

    protected void Brake()
    {
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
        VerticalSpeed = VerticalSpeedMax;
    }

    protected void Gravity()
    {
        VerticalSpeed -= GravityValue * Time.deltaTime * GravityAdjust;

        VerticalSpeed = Mathf.Clamp(VerticalSpeed, GravityMax, VerticalSpeedMax);
    }

    protected void Roll(float Length)
    {

    }

    protected void SuddenlyBrake(float Length)
    {

    }

    protected void Lock(float Length)
    {

    }
}
