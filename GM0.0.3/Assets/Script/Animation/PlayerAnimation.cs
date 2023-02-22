using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : CommonAnimation
{
    // Start is called before the first frame update
    void Start()
    {
        InitComponentSet();

        CommonState = this.GetComponent<PlayerState>();
    }

    // Update is called once per frame
    void Update()
    {
        CommonUpdate();
    }
}
