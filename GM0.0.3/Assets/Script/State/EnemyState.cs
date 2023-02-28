using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : CommonState
{
    [Header("���a���A")]
    public bool PlayerInView;

    public bool PlayerOnGround;

    public int PlayerDistanceIndex;
    public bool PlayerNearing;

    [Header("�ۦ�")]
    public int[] AttackMethodUsedTime;
    public int AttackMethodNow;

    void Start()
    {
        InitValueSet();
    }
   
    void Update()
    {
        
    }
}
