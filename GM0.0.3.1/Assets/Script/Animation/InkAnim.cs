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

    //移動類Hash
    int jumpHash = Animator.StringToHash("Jump");
    int dashHash = Animator.StringToHash("Dash");
    int MoveHorizontalHash = Animator.StringToHash("MoveHorizontal");
    int VelocityYHash = Animator.StringToHash("VelocityY"); 
    //觸地Bool的Hash
    int TouchGroundHash = Animator.StringToHash("TouchGround");
    
    //掉落類Hash
    int fallStateHash = Animator.StringToHash("Base Layer.Falling");
    int isFallingHash = Animator.StringToHash("isFalling");
    
    //斬擊Tirgger
    int SlashHash = Animator.StringToHash("Slash");
    //攻擊動畫狀態Hash
    int Slash1StateHash = Animator.StringToHash("Base Layer.Slash1");
    int Slash2StateHash = Animator.StringToHash("Base Layer.Slash2");
    int SlashJumpTo2StateHash = Animator.StringToHash("Base Layer.SlashJumpTo2");
    int Slash3StateHash = Animator.StringToHash("Base Layer.Slash3");
    //攻擊中Bool的Hash
    int isAttackingHash = Animator.StringToHash("isAttacking");
    
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
        
        //確認碰觸地面
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
        
        //攻擊
        if(Input.GetButtonDown("Slash"))
        {
            Attack();
        }
        
        //落下
        float speedY = InkController.velocity.y;
        anim.SetFloat(VelocityYHash,speedY);
        
        //依據是否碰觸地面，調整Bool
        if(isGrounded)
        {
            anim.SetBool(TouchGroundHash,true);
        }
        if(!isGrounded)
        {
            anim.SetBool(TouchGroundHash,false);
        }

        //調取動畫階段
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        //根據動畫階段調整下落Bool
        if(stateInfo.fullPathHash == fallStateHash)
        {
            anim.SetBool(isFallingHash,true);
        }

        if(stateInfo.fullPathHash != fallStateHash)
        {
            anim.SetBool(isFallingHash,false);
        }
        
        //根據動畫階段調整攻擊Bool
        if(stateInfo.fullPathHash == Slash1StateHash || stateInfo.fullPathHash == Slash2StateHash || stateInfo.fullPathHash == SlashJumpTo2StateHash || stateInfo.fullPathHash == Slash3StateHash)
        {
            anim.SetBool(isAttackingHash,true);
        }

        if(stateInfo.fullPathHash != Slash1StateHash && stateInfo.fullPathHash != Slash2StateHash && stateInfo.fullPathHash != SlashJumpTo2StateHash && stateInfo.fullPathHash != Slash3StateHash)
        {
            anim.SetBool(isAttackingHash,false);
        }


    }
    
    //攻擊程式
    void Attack()
    {
        anim.SetTrigger(SlashHash);
    }

    
}
