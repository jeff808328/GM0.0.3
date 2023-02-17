using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonMove : MonoBehaviour
{

    #region 水平速度控制

    protected float HorizonSpeedMax = 0; //速度上限
    [HideInInspector] public float HorizonSpeed = 0; // 運算用 & 當前值

    public float AddSpeedAdjust; // 移動速度控制, 直接用 Time.DeltaTime 值太小
    protected float OriginAddSpeedAdjust;

    [SerializeField] protected float MinusSpeedAdjust; // 移動速度控制, 直接用 Time.DeltaTime 值太小
    private float OriginMinusSpeedAdjust;

    private float AddSpeed; // 加速度初始值
    private float MinusSpeed; // 減速度初始值   

    [SerializeField] protected int LastMoveDirection; // 上次的移動方向 1朝右 -1朝左
    #endregion

    #region 垂直速度控制

    protected float VerticalSpeedMax = 0; //速度上限
    [HideInInspector] public float VerticalSpeed = 0; // 運算用 & 當前值

    private float GravityValue; // 重力初始值
    public float GravityAdjust; // 重力調整值 
    protected float OriGravityAdjust;
    public float GravityMax; // 重力最大值

    protected int MaxJumpTimes; // 最大跳躍次數

    private bool GroundTouching; // 地板偵測

    #endregion

    #region 速度控制

    private float BeforeDashSpeed;
    private float BeforeDahsMoveDirection;
    public float DashAdjust;

    [SerializeField] protected Vector2 FinalMoveSpeed;

    #endregion


    #region 角度控制

    public float RightAngle;
    public float LeftAngle;
    public float FlipLength;

    #endregion

    #region 組件

    public ChatacterData CharacterData;
    protected Rigidbody2D Rd;
    protected CommonState CommonState;

    #endregion

    protected void InitValueSet()
    {
        LastMoveDirection = 1;

        HorizonSpeedMax = CharacterData.MaxMoveSpeed;
        VerticalSpeedMax = CharacterData.JumpSpeed;

        AddSpeed = CharacterData.AddSpeed;
        MinusSpeed = CharacterData.MinusSpeed;

        GravityValue = CharacterData.Gravity;

        OriginAddSpeedAdjust = AddSpeedAdjust;
        OriginMinusSpeedAdjust = MinusSpeedAdjust;

        OriGravityAdjust = GravityAdjust;

        this.transform.eulerAngles = new Vector3(0, RightAngle, 0);
    }

    protected void InitComponmentSet()
    {
        Rd = this.GetComponent<Rigidbody2D>();
    }

    protected void Run(int Direction) // 加速 // 在可操控情況下可用
    {
        CommonState.ActionLayerNow = 1;
        CommonState.Moveing = true;

        if (Direction != LastMoveDirection)
            StartCoroutine(Flip(Direction));

        HorizonSpeed += AddSpeed * Direction * Time.deltaTime * AddSpeedAdjust; // v = v0 + at*調整值
        HorizonSpeed = Mathf.Clamp(HorizonSpeed, -HorizonSpeedMax, HorizonSpeedMax); // 避免超過速度上限

        LastMoveDirection = Direction; // 紀錄當前移動方向,轉向和減速用
    }


    protected IEnumerator Jump()
    {
        CommonState.ActionLayerNow = 1;

        CommonState.Jumping = true;

        VerticalSpeed = VerticalSpeedMax;

        CommonState.JumpTime++;

        yield return new WaitForSecondsRealtime(CommonState.RaiseAniLength);

        CommonState.Jumping = false;
    }


    protected IEnumerator Flip(int Direction)// 翻面 // Run的備註
    {
        float t = 0;
        float angle = 0;
        float startangle = transform.rotation.y;

        if (!CommonState.GroundTouching)
        {
            HorizonSpeed *= -0.75f;
        }


        if(Direction >= 0)
        {
            while (t < FlipLength)
            {
                angle = Mathf.Lerp(startangle, RightAngle, t / FlipLength);

                transform.eulerAngles = new Vector3(transform.eulerAngles.x, angle,0);

                t += Time.deltaTime;
            }

        }
        else
        {
            while (t < FlipLength)
            {
                angle = Mathf.Lerp(startangle, LeftAngle, t / FlipLength);

                transform.eulerAngles = new Vector3(transform.eulerAngles.x, angle, 0);

                t += Time.deltaTime;
            }
        }

        yield return null;
   
    }

    protected void Brake() // 在玩家無輸入且非無敵狀況時可用
    {
        CommonState.ActionLayerNow = 0;
        CommonState.Moveing = false;

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

    protected void Gravity()
    {
        VerticalSpeed -= GravityValue * Time.deltaTime * GravityAdjust;

        VerticalSpeed = Mathf.Clamp(VerticalSpeed, GravityMax, VerticalSpeedMax);
    }

    public IEnumerator Roll(int Direction, float Length, float Speed)
    {
        CommonState.IsUnbreakable = true;
        CommonState.ActionLayerNow = 3;
        CommonState.AttackAble = false;
        CommonState.MoveAble = false;
        CommonState.RollAble = false;

        HorizonSpeedMax = Speed;

        BeforeDashSpeed = HorizonSpeed;
        HorizonSpeed = Speed*LastMoveDirection;

        yield return new WaitForSecondsRealtime(Length);

        if (LastMoveDirection == Direction)
        {
            HorizonSpeed = BeforeDashSpeed;
        }
        else
        {
            HorizonSpeed = 0;
        }

        HorizonSpeedMax = CharacterData.MaxMoveSpeed;

        CommonState.RollAble = true;
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
