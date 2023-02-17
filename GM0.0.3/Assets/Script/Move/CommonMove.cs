using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonMove : MonoBehaviour
{

    #region �����t�ױ���

    protected float HorizonSpeedMax = 0; //�t�פW��
    [HideInInspector] public float HorizonSpeed = 0; // �B��� & ��e��

    public float AddSpeedAdjust; // ���ʳt�ױ���, ������ Time.DeltaTime �ȤӤp
    protected float OriginAddSpeedAdjust;

    [SerializeField] protected float MinusSpeedAdjust; // ���ʳt�ױ���, ������ Time.DeltaTime �ȤӤp
    private float OriginMinusSpeedAdjust;

    private float AddSpeed; // �[�t�ת�l��
    private float MinusSpeed; // ��t�ת�l��   

    [SerializeField] protected int LastMoveDirection; // �W�������ʤ�V 1�¥k -1�¥�
    #endregion

    #region �����t�ױ���

    protected float VerticalSpeedMax = 0; //�t�פW��
    [HideInInspector] public float VerticalSpeed = 0; // �B��� & ��e��

    private float GravityValue; // ���O��l��
    public float GravityAdjust; // ���O�վ�� 
    protected float OriGravityAdjust;
    public float GravityMax; // ���O�̤j��

    protected int MaxJumpTimes; // �̤j���D����

    private bool GroundTouching; // �a�O����

    #endregion

    #region �t�ױ���

    private float BeforeDashSpeed;
    private float BeforeDahsMoveDirection;
    public float DashAdjust;

    [SerializeField] protected Vector2 FinalMoveSpeed;

    #endregion


    #region ���ױ���

    public float RightAngle;
    public float LeftAngle;
    public float FlipLength;

    #endregion

    #region �ե�

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

    protected void Run(int Direction) // �[�t // �b�i�ޱ����p�U�i��
    {
        CommonState.ActionLayerNow = 1;
        CommonState.Moveing = true;

        if (Direction != LastMoveDirection)
            StartCoroutine(Flip(Direction));

        HorizonSpeed += AddSpeed * Direction * Time.deltaTime * AddSpeedAdjust; // v = v0 + at*�վ��
        HorizonSpeed = Mathf.Clamp(HorizonSpeed, -HorizonSpeedMax, HorizonSpeedMax); // �קK�W�L�t�פW��

        LastMoveDirection = Direction; // ������e���ʤ�V,��V�M��t��
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


    protected IEnumerator Flip(int Direction)// ½�� // Run���Ƶ�
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

    protected void Brake() // �b���a�L��J�B�D�L�Ī��p�ɥi��
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
