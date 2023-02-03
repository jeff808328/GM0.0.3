using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : CommonMove
{
    private PlayerState PlayerState; 

    void Start()
    {
        InitValueSet();

        InitComponmentSet();

        PlayerInitComponentSet();
    }

    void Update()
    {
        
    }

    private void PlayerInitComponentSet()
    {
        CommonState = this.GetComponent<PlayerState>();
        PlayerState = this.GetComponent<PlayerState>();
    }

    
}
