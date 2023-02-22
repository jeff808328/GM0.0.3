using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : CommonHP
{
    void Start()
    {
        InitValueSet();

        CommonMove = this.GetComponent<PlayerMove>();
        CommonState = this.GetComponent<PlayerState>();
        CommonAnimation = this.GetComponent<PlayerAnimation>();
    }

    void Update()
    {
        
    }
}
