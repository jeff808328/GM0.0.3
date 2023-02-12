using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InkAnim : MonoBehaviour
{
    //地面檢測參數
    public Transform groundCheck;
    public LayerMask groundMask;
    public float groundDistance = 0.4f;
    bool isGrounded;
    //存取Animator
    public Animator anim;

    //存取CharacterController
    public CharacterController InkController;

    //轉換成Hash值
    
    int jumpHash = Animator.StringToHash("Jump");
    int dashHash = Animator.StringToHash("Dash");
    int MoveHorizontalHash = Animator.StringToHash("MoveHorizontal");
    int VelocityYHash = Animator.StringToHash("VelocityY"); 
    int TouchGroundHash = Animator.StringToHash("TouchGround");
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //獲取水平(X軸)與垂直(Z軸)輸入
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        //跑步
        anim.SetFloat(MoveHorizontalHash,Mathf.Abs(horizontal));

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        //跳躍
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            anim.SetTrigger(jumpHash);
        }
        //衝刺
        if(Input.GetButtonDown("Dash"))
        {
            anim.SetTrigger(dashHash);
        }
        //落下
        float speedY = InkController.velocity.y;
        anim.SetFloat(VelocityYHash,speedY);

        //地面
        if(isGrounded)
        {
            anim.SetBool(TouchGroundHash,true);
        }
        if(!isGrounded)
        {
            anim.SetBool(TouchGroundHash,false);
        }

    }

    
}
