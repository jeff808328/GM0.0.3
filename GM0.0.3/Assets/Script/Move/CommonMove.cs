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
    #endregion

    #region �����t�ױ���
    protected float VerticalSpeedMax = 0; //�t�פW��
    public float VerticalSpeed = 0; // �B��� & ��e��

    private float Gravity; // ���O��l��
    public float GravityAdjust; // ���O�վ�� 
    public float GravityMax; // ���O�̤j��

    protected int MaxJumpTimes; // �̤j���D����

    private bool GroundTouching; // �a�O����

    protected int JumpTime;
    #endregion

    #region �ե�

    protected CommonState CommonState;

    #endregion


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
