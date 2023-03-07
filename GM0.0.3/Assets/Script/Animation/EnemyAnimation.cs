using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : CommonAnimation
{
    void Start()
    {
        InitComponentSet(); 

        CommonState = this.GetComponent<EnemyState>();
    }

    void Update()
    {
        CommonUpdate();
    }
}
