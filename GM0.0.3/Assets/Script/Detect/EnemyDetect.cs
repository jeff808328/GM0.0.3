using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetect : CommonDetect
{
    [Header("½�఻��")]
    public float FlipDetectLength;

    [Header("��������]�w")]
    public LayerMask Player;

    public float ViewBoxHeight;
    public float ViewBoxWidth;
    public float ViewBoxHeightOffset;
    public float ViewBoxWidthOffset;

    public float ViewFocusLength; // ���a�i�J������h�֮ɶ��}�l�l��

    public Color ViewBoxColor;

    [Header("���a�Z���p��")]
    public float MidMini; // �����q�̤p��
    private float MidMiniOri;

    public float MidMax; // �����q�̤j��
    private float MidMaxOri;

    public float RamdonValue; // �վ�ζü�
    public float Buffer; // �����q���׳̤p��

    private Vector3 SelfPos;
    private Vector3 MidMiniPos; // show�����q����
    private Vector3 MidMaxPos;

    [Header("�������Ĳ�o�]�w")]
    public float AtkTriggerBoxHeight;
    public float AtkTriggerBoxWidth;
    public float AtkTriggerBoxHeightOffset;
    public float AtkTriggerBoxWidthOffset;

    public Color AtkTriggerBoxColor;

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
}
