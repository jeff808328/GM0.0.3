using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonMove : MonoBehaviour
{

    [Header("水平速度控制")]
    protected float HorizonSpeedMax = 0; //速度上限
    public float HorizonSpeed = 0; // 運算用 & 當前值

    public float AddSpeedAdjust; // 移動速度控制, 直接用 Time.DeltaTime 值太小
    protected float OriginAddSpeedAdjust;

    [SerializeField] protected float MinusSpeedAdjust; // 移動速度控制, 直接用 Time.DeltaTime 值太小
    private float OriginMinusSpeedAdjust;

    private float AddSpeed; // 加速度初始值
    private float MinusSpeed; // 減速度初始值   

    [SerializeField] protected int LastMoveDirection; // 上次的移動方向 1朝右 -1朝左


    [Header("垂直速度控制")]
    protected float VerticalSpeedMax = 0; //速度上限
    public float VerticalSpeed = 0; // 運算用 & 當前值

    private float GravityValue; // 重力初始值
    public float GravityAdjust; // 重力調整值 
    protected float OriGravityAdjust;
    public float GravityMax; // 重力最大值

    protected int MaxJumpTimes; // 最大跳躍次數

    public float JumpSpeedAdjust;


    [Header("翻滾速度控制")]
    private float BeforeDashSpeed;
    private float BeforeDahsMoveDirection;
    public float DashAdjust;
    protected float DashCDStartTime;

    [Header("實際速度")]
    [SerializeField] protected Vector2 FinalMoveSpeed;

    [Header("翻轉角度設定")]
    public float RightAngle;
    public float LeftAngle;
    public float FlipLength;

    public ChatacterData CharacterData;
    protected Rigidbody2D Rd;
    protected CommonState CommonState;
    protected Transform ModelTransform;

    private float AniStartTime;


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

        ModelTransform = transform.GetChild(0);
        ModelTransform.localEulerAngles = new Vector3(0, RightAngle, 0);
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
            Flip(Direction);

        HorizonSpeed += AddSpeed * Direction * Time.deltaTime * AddSpeedAdjust; // v = v0 + at*調整值
        HorizonSpeed = Mathf.Clamp(HorizonSpeed, -HorizonSpeedMax, HorizonSpeedMax); // 避免超過速度上限

        LastMoveDirection = Direction; // 紀錄當前移動方向,轉向和減速用
    }

    public IEnumerator Jump()
    {
        CommonState.ActionLayerNow = 1;

        CommonState.Jumping = true;

        VerticalSpeed = VerticalSpeedMax;

        CommonState.JumpTime++;

        yield return new WaitForSecondsRealtime(CommonState.RaiseAniLength);

        CommonState.Jumping = false;
    }

    protected void Flip(int Direction)// 翻面 // Run的備註
    {
        float startangle = transform.rotation.y;

        if (!CommonState.GroundTouching)
        {
            HorizonSpeed *= -0.75f;
        }


        if (Direction >= 0)
        {
            this.transform.localScale = new Vector3(1, 1, 1);

            //while (t < FlipLength)
            //{
            //    angle = Mathf.Lerp(startangle, RightAngle, t / FlipLength);

            //    transform.eulerAngles = new Vector3(transform.eulerAngles.x, angle, 0);

            //    t += Time.deltaTime;
            //}

            ModelTransform.localEulerAngles = new Vector3(transform.eulerAngles.x, RightAngle, 0);

        }
        else
        {
            this.transform.localScale = new Vector3(-1, 1, 1);

            //while (t < FlipLength)
            //{
            //    angle = Mathf.Lerp(startangle, LeftAngle, t / FlipLength);

            //    transform.eulerAngles = new Vector3(transform.eulerAngles.x, angle, 0);

            //    t += Time.deltaTime;
            //}

            ModelTransform.localEulerAngles = new Vector3(transform.eulerAngles.x, RightAngle, 0);

        }

     //   Debug.Log("true flip");
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
        CommonState.MoveAble = false;
        CommonState.RollAble = false;
        CommonState.Rolling = true;
        CommonState.AttackAble = true;

        HorizonSpeedMax = Speed;

        GravityAdjust = 0;
        //   VerticalSpeed = 1;

        BeforeDashSpeed = HorizonSpeed;
        HorizonSpeed = Speed * Direction;

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

        GravityAdjust = OriGravityAdjust;

        CommonState.Rolling = false;
        CommonState.RollAble = true;
        CommonState.MoveAble = true;
        CommonState.ActionLayerNow = 0;
        CommonState.IsUnbreakable = false;

        DashCDStartTime = Time.time;
    }

    public IEnumerator SuddenlyBrake(float Length)
    {
      //  Debug.Log("suddenly brake");

        HorizonSpeed = 0;
        AddSpeed = 0;

        CommonState.Moveing = false;
        CommonState.MoveAble = false;

        yield return new WaitForSecondsRealtime(Length);

        CommonState.MoveAble = true;

        AddSpeed = CharacterData.AddSpeed;
    }

    public IEnumerator AntiGravity(float Length)
    {
        GravityValue = 0;
        Rd.mass = 0;// rd的gravity要關掉

        yield return new WaitForSecondsRealtime(Length);

        Rd.mass = 10;
        GravityValue = CharacterData.Gravity;
    }
}
