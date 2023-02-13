using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InkMovement : MonoBehaviour
{
    //取得角色控制器
    public CharacterController controller;
    //存取Ink Animator
    public Animator anim;

    //存取動畫階段
    AnimatorStateInfo stateInfo;
    AnimatorTransitionInfo transitionInfo;

    //動畫參數轉Hash
    int isAttackingHash = Animator.StringToHash("isAttacking");
    //動畫轉場轉Hash
    int DashToSlash3Hash = Animator.StringToHash("Base Layer.DashToSlash3");

    //動畫階段轉Hash
    int DashStateHash = Animator.StringToHash("Base Layer.Dash");
    int Slash1StateHash = Animator.StringToHash("Base Layer.Slash1");
    int Slash3StateHash = Animator.StringToHash("Base Layer.Slash3");
    //動畫參數
    bool isAttackingBool;
    int AttackRoundHash = Animator.StringToHash("AttackRound");
    int attackRound = 1;

    //地面檢測參數
    public Transform groundCheck;
    public LayerMask groundMask;
    public float groundDistance = 0.4f;
    bool isGrounded;
    //移動參數
    public float speed = 6f;
    public float DashMultiplier = 2f;
    public float DashDrag = 0.1f;
    float realSpeed;
    float speedStored;
    public float gravity = -9.8f;
    public float turnSmoothTime = 0.1f;
    public float jumpHeight = 3f;
    
    float turnSmoothVelocity;
    Vector3 velocity;
    
    void Start()
    {
        realSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        //測試，AnimatorTransitionInfo
        

        //調取動畫階段
        stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        //transitionInfo = anim.GetCurrentAnimatorInfo(0);


        
        
        //取得，正在攻擊，Bool
        isAttackingBool = anim.GetBool("isAttacking");
        
        //獲取水平(X軸)與垂直(Z軸)輸入
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        //取得玩家向量，moveDirection提供X軸參數，faceDirection提供X、Z軸參數
        Vector2 moveDirection = new Vector2(horizontal, 0f).normalized;
        Vector3 faceDirection = new Vector3(horizontal, 0f, vertical).normalized;

        //取得玩家斬擊階段
        attackRound = anim.GetInteger(AttackRoundHash);

        //向量大於0.1時，且當前非斬擊時，移動物件
        if(moveDirection.magnitude >= 0.1f && !isAttackingBool)
        {
            //重置儲存的速度
            speedStored = realSpeed;
            //取得角度(Rad)後，轉換為Degree，並旋轉
            float targetAngle = Mathf.Atan2(faceDirection.x, faceDirection.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle,ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            
            controller.Move(moveDirection * realSpeed * Time.deltaTime);   
        }

        //衝刺斬擊獨立移動

        if(moveDirection.magnitude >= 0.1f && stateInfo.fullPathHash == Slash3StateHash && !isGrounded)
        {
            controller.Move(moveDirection * realSpeed * Time.deltaTime);
            velocity.y = -5f; 
        }
        if(moveDirection.magnitude >= 0.1f && stateInfo.fullPathHash == Slash3StateHash && isGrounded)
        {
            if(speedStored >= 0)
            {
                speedStored -= DashDrag * Time.deltaTime;
            } 
            controller.Move(moveDirection * speedStored * Time.deltaTime);
        }
        
        //重力加速度
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        
        //地面檢測，在檢測位置放置一個檢測球體
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
         
        //若檢測到地面，重置下落速度
        if(isGrounded && velocity.y<0)
        {
            velocity.y = -2f;
        }

        //跳躍
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        //衝刺
        if(Input.GetButtonDown("Dash") && stateInfo.fullPathHash != DashStateHash)
        {
            realSpeed = speed * DashMultiplier;                
        }

        //衝刺後減速
        if(realSpeed > speed)
        {
            realSpeed -= DashDrag * Time.deltaTime;
        }
    }
}
