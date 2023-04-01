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

    //彈窗設置
    //彈出
    public void PopUp()
    {
        gameObject.SetActive(true);
    }

    //收回
    public void Close()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
