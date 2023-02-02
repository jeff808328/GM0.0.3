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

        HorizonSpeed += AddSpeed * Direction * Time.deltaTime * AddSpeedAdjust; // v = v0 + at*�վ��
        HorizonSpeed = Mathf.Clamp(HorizonSpeed, -HorizonSpeedMax, HorizonSpeedMax); // �קK�W�L�t�פW��

        LastMoveDirection = Direction; // ������e���ʤ�V,��V�M��t��
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
