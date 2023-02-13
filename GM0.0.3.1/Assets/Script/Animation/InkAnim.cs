using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InkAnim : MonoBehaviour
{
    //存取CharacterController
    public CharacterController InkController;
    
    //地面檢測參數
    public Transform groundCheck;
    public LayerMask groundMask;
    public float groundDistance = 0.4f;
    bool isGrounded;
    //存取Animator
    public Animator anim;

    //存取動畫階段
    AnimatorStateInfo stateInfo;

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
    
    //斬擊Value
    int SlashValueHash = Animator.StringToHash("SlashValue");
    public float slashOriginValue = 10f;
    float slashValue;
    public float slashValueDecrease = 0.1f;

    //衝刺動畫狀態Hash
    int DashStateHash = Animator.StringToHash("Base Layer.Dash");
    //斬擊動畫判定BoolHash
    int isDashingHash = Animator.StringToHash("isDashing");
    bool isDashing = false;
    //攻擊動畫狀態Hash
    int Slash1StateHash = Animator.StringToHash("Base Layer.Slash1");
    int Slash2StateHash = Animator.StringToHash("Base Layer.Slash2");
    int SlashJumpTo2StateHash = Animator.StringToHash("Base Layer.SlashJumpTo2");
    int Slash3StateHash = Animator.StringToHash("Base Layer.Slash3");
    //攻擊中BoolHash
    int isAttackingHash = Animator.StringToHash("isAttacking");
    //紀錄攻擊階段
    int attackRound = 1;
    int AttackRoundHash = Animator.StringToHash("AttackRound");
    
    

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

        //調取動畫階段
        stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        
        //跑步
        anim.SetFloat(MoveHorizontalHash,Mathf.Abs(horizontal));
        
        //確認碰觸地面
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        
        //跳躍
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            anim.SetTrigger(jumpHash);
            slashValue = 0;
        }
        
        //衝刺
        if(Input.GetButtonDown("Dash") && stateInfo.fullPathHash != DashStateHash)
        {
            anim.SetTrigger(dashHash);
            slashValue = 0;
        }
        //衝刺動畫Bool切換
        if(stateInfo.fullPathHash == DashStateHash)
        {
            anim.SetBool(isDashingHash,true);
            isDashing = true;
        }

        else
        {
            anim.SetBool(isDashingHash,false);
            isDashing = false;
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

        
        //根據動畫階段調整下落Bool
        if(stateInfo.fullPathHash == fallStateHash)
        {
            anim.SetBool(isFallingHash,true);
        }

        if(stateInfo.fullPathHash != fallStateHash)
        {
            anim.SetBool(isFallingHash,false);
        }
        
        //根據動畫階段調整攻擊Bool,攻擊時設為true
        if(stateInfo.fullPathHash == Slash1StateHash || stateInfo.fullPathHash == Slash2StateHash || stateInfo.fullPathHash == SlashJumpTo2StateHash || stateInfo.fullPathHash == Slash3StateHash)
        {
            anim.SetBool(isAttackingHash,true);
        }
        //不攻擊時，攻擊Bool設為false，同時重置攻擊階段為1
        if(stateInfo.fullPathHash != Slash1StateHash && stateInfo.fullPathHash != Slash2StateHash && stateInfo.fullPathHash != SlashJumpTo2StateHash && stateInfo.fullPathHash != Slash3StateHash)
        {
            anim.SetBool(isAttackingHash,false);
            attackRound = 1;
            anim.SetInteger(AttackRoundHash,attackRound);
        }

        //攻擊
        if(Input.GetButtonDown("Slash"))
        {
            Attack();           
        }
        

        //隨時間減少連擊預備值，Animator設定為低於定值不觸發斬擊
        if(slashValue>0)
        {
            slashValue -= slashValueDecrease * Time.deltaTime;
               
        }

        anim.SetFloat(SlashValueHash, slashValue);

    }
    
    //攻擊程式
    void Attack()
    {
        slashValue = slashOriginValue;
        
        //攻擊階段符合時，把連擊數增加，Animator使用增加後的連擊數當下一段攻擊之條件
        if(stateInfo.fullPathHash == Slash1StateHash)
        {
            attackRound = 2;
            anim.SetInteger(AttackRoundHash,attackRound);
        }
        if(stateInfo.fullPathHash == Slash2StateHash)
        {
            attackRound = 3;
            anim.SetInteger(AttackRoundHash,attackRound);
        }
        //衝刺動畫可強制啟動環形斬
        if(isDashing)
        {
            attackRound = 3;
            anim.SetInteger(AttackRoundHash,attackRound);
        }
    }

    
}
