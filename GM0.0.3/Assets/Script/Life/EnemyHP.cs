using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : CommonHP
{
    void Start()
    {
        InitValueSet();

        CommonMove = this.GetComponent<EnemyMove>();
        CommonState = this.GetComponent<EnemyState>();
        CommonAnimation = this.GetComponent<EnemyAnimation>();
    }
}
