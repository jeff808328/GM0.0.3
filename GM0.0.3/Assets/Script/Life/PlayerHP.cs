using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : CommonHP
{
    // Start is called before the first frame update
    void Start()
    {
        InitValueSet();

        CommonMove = this.GetComponent<PlayerMove>();
        CommonState = this.GetComponent<PlayerState>();
        CommonAnimation = this.GetComponent<PlayerAnimation>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
