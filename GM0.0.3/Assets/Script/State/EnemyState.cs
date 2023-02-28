using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : CommonState
{
    [Header("ª±®aª¬ºA")]
    public bool PlayerInView;

    public bool PlayerOnGround;

    public int PlayerDistanceIndex;
    public bool PlayerNearing;

    [Header("©Û¦¡")]
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
