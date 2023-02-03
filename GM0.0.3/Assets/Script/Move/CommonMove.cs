using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonMove : MonoBehaviour
{
    #region �����t�ױ���

    private float HorizonSpeedMax = 0; //�t�פW��
    public float HorizonSpeed = 0; // �B��� & ��e��

    public float AddSpeedAdjust; // ���ʳt�ױ���, ������ Time.DeltaTime �ȤӤp
    private float OriginAddSpeedAdjust;

    [SerializeField] protected float MinusSpeedAdjust; // ���ʳt�ױ���, ������ Time.DeltaTime �ȤӤp
    private float OriginMinusSpeedAdjust;

    private float AddSpeed; // �[�t�ת�l��
    private float MinusSpeed; // ��t�ת�l��   

    protected int LastMoveDirection; // �W�������ʤ�V 1�¥k -1�¥�
    #endregion

    #region �����t�ױ���
    protected float VerticalSpeedMax = 0; //�t�פW��
    public float VerticalSpeed = 0; // �B��� & ��e��

    private float GravityValue; // ���O��l��
    public float GravityAdjust; // ���O�վ�� 
    public float GravityMax; // ���O�̤j��

    protected int MaxJumpTimes; // �̤j���D����

    private bool GroundTouching; // �a�O����

    protected int JumpTime;
    #endregion

    #region �t�ױ���

    [SerializeField] protected Vector2 FinalMoveSpeed;

    private float BeforeDashSpeed;
    private float BeforeDahsMoveDirection;

    #endregion 

    #region �ե�

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

    protected void Run(int Direction) // �[�t // �b�i�ޱ����p�U�i��
    {
        CommonState.ActionLayerNow = 1;

        if (Direction != LastMoveDirection)
            Flip(Direction);

        HorizonSpeed += AddSpeed * Direction * Time.deltaTime * AddSpeedAdjust; // v = v0 + at*�վ��
        HorizonSpeed = Mathf.Clamp(HorizonSpeed, -HorizonSpeedMax, HorizonSpeedMax); // �קK�W�L�t�פW��

        LastMoveDirection = Direction; // ������e���ʤ�V,��V�M��t��
    }

    protected void Flip(int Direction) // ½�� // Run���Ƶ�
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

    protected void Brake() // �b���a�L��J�B�D�L�Ī��p�ɥi��
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
