using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetect : CommonDetect
{
    void Start()
    {
        CommonState = this.GetComponent<PlayerState>();
    }

    void Update()
    {
        BaseDetectBoxUpdate();
    }
}
