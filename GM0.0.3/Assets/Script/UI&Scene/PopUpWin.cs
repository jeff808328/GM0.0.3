using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpWin : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    //�u���]�m
    //�u�X
    public void PopUp()
    {
        gameObject.SetActive(true);
    }

    //���^
    public void Close()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
