using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : CommonHP
{
    private EnemyState EnemyState;

    void Start()
    {
        InitValueSet();

        CommonMove = this.GetComponent<EnemyMove>();
        CommonState = this.GetComponent<EnemyState>();
        EnemyState = this.GetComponent<EnemyState>();
        CommonAnimation = this.GetComponent<EnemyAnimation>();
        CommonAudioManager = this.GetComponent<EnemyAudioManager>();

        EnemyState.HPOri = Hp;
    }

    private void Update()
    {
        EnemyState.HP = Hp;
    }
}
