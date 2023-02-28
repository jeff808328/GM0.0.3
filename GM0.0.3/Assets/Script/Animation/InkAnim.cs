using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InkAnim : MonoBehaviour
{
    //存取CharacterController
    public CharacterController InkController;
    
    //存取Particle

    public ParticleSystem swordParticle;

    //存取大劍
    public GameObject greatSwordHand;
    public GameObject greatSwordBack;
    
    //地面檢測參數
    public Transform groundCheck;
    public LayerMask groundMask;
    public float groundDistance = 0.4f;
    bool isGrounded;
    //存取Animator
    public Animator anim;

    //存取動畫階段
    AnimatorStateInfo stateInfo;

    //儲存移動輸入
    float horizontal;
    float vertical;

    //移動類Hash
    int jumpHash = Animator.StringToHash("Jump");
    int dashHash = Animator.StringToHash("Dash");
    int MoveHorizontalHash = Animator.StringToHash("MoveHorizontal");
    int VelocityYHash = Animator.StringToHash("VelocityY"); 
    //觸地BoolHash
    int TouchGroundHash = Animator.StringToHash("TouchGround");
    //掉落類BoolHash
    
    int isFallingHash = Animator.StringToHash("isFalling");
    
    //斬擊Value
    int SlashValueHash = Animator.StringToHash("SlashValue");
    public float slashOriginValue = 10f;
    float slashValue;
    public float slashValueDecrease = 0.1f;
    //命中判定
    public Transform attackPoint;

    public float attackRange = 0.5f;
    public Vector3 attackArea;
    public LayerMask enemyLayers;
    
    //儲存動畫Hash值，在執行底端與當前動畫Hash值同步，用以確認動畫是否切換
    
    int storeCurrentAnimStateHash;


    //命中Array儲存
    Collider[] hitEnemies;
    GameObject[] hittedEnemies;

    //斬擊動畫判定BoolHash
    int isDashingHash = Animator.StringToHash("isDashing");
    //衝刺BoolHash
    bool isDashing = false;
    //動畫狀態Hash
    int DashStateHash    = Animator.StringToHash("Base Layer.Dash");
    int fallingStateHash = Animator.StringToHash("Base Layer.Falling");
    int fallStateHash    = Animator.StringToHash("Base Layer.Fall");
    int LTHurtStateHash  = Animator.StringToHash("Base Layer.LTHurt");
    int HVHurtStateHash  = Animator.StringToHash("Base Layer.HVHurt");
    int JumpStateHash  = Animator.StringToHash("Base Layer.Jump");
    int Idle1StateHash  = Animator.StringToHash("Base Layer.Idle1");
    int Slash1StateHash  = Animator.StringToHash("Base Layer.Slash1");
    int Slash2StateHash  = Animator.StringToHash("Base Layer.Slash2");
    int SlashJumpTo2StateHash = Animator.StringToHash("Base Layer.SlashJumpTo2");
    int Slash3StateHash  = Animator.StringToHash("Base Layer.Slash3");
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
        //設定大劍啟用
        SetActiveOfSword();
        
        //設定大劍特效
        AdjustSwordEffect();

        //獲取輸入與讀取動畫資訊
        GetInfo();
        
        //確認碰觸地面
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        //基礎移動(跑步、跳躍、衝刺)
        CharacterStandardMove();
        
        //調整衝刺狀態Bool
        AdjustIsDashingBool();
        
        //落下
        float speedY = InkController.velocity.y;
        anim.SetFloat(VelocityYHash,speedY);
        
        //調整觸地Bool
        AdjustTouchGroundBool();

        //調整下落Bool
        AdjustFallingBool();
        
        //調整攻擊Bool
        AdjustAttackBool();
        
        //重置受擊物件Tag
        ReverseHittedTag();

        //攻擊
        if(Input.GetButtonDown("Slash"))
        {
            Attack();           
        }

        //偵測攻擊
        AttackDetect();
    
        //隨時間減少攻擊值，Animator設定為低於定值不觸發斬擊
        if(slashValue>0)
        {
            slashValue -= slashValueDecrease * Time.deltaTime;           
        }
        //同步攻擊值
        anim.SetFloat(SlashValueHash, slashValue);

        //同步當前動畫Hash值
        storeCurrentAnimStateHash = stateInfo.fullPathHash;

    }
    
    
    //設定大劍啟用
    void SetActiveOfSword()
    {
        //偵測動畫狀態Tag,並依此啟動、停用背上和手中的劍
        if(stateInfo.IsTag("InHand"))
        {
            greatSwordHand.SetActive(true);
            greatSwordBack.SetActive(false);
        }
        else
        {
            greatSwordHand.SetActive(false);
            greatSwordBack.SetActive(true); 
        }
    }
    
    //設定大劍特效
    void AdjustSwordEffect()
    {
        //設定大劍特效
        if(stateInfo.fullPathHash == Idle1StateHash)
        {
            swordParticle.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }
        else if(!swordParticle.isEmitting)
        {   
            swordParticle.Play(true);
        }
    }

    //調整觸地Bool
    void AdjustTouchGroundBool()
    {
        //依據是否碰觸地面，調整Bool
        if(isGrounded)
        {
            anim.SetBool(TouchGroundHash,true);
        }
        else
        {
            anim.SetBool(TouchGroundHash,false);
        }
    }

    //基礎移動(跑步、跳躍、衝刺)
    void CharacterStandardMove()
    {
        //跑步
        anim.SetFloat(MoveHorizontalHash,Mathf.Abs(horizontal));          
        
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
    }

    //調整衝刺狀態Bool
    void AdjustIsDashingBool()
    {
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
    }

    //調整下落Bool
    void AdjustFallingBool()
    {
        //根據動畫階段調整下落Bool
        if(stateInfo.fullPathHash == fallingStateHash)
        {
            anim.SetBool(isFallingHash,true);
        }
        else
        {
            anim.SetBool(isFallingHash,false);
        }
    }

    //調整攻擊Bool
    void AdjustAttackBool()
    {
        //根據動畫階段調整攻擊Bool,攻擊時設為true
        if(stateInfo.fullPathHash == Slash1StateHash || stateInfo.fullPathHash == Slash2StateHash || stateInfo.fullPathHash == SlashJumpTo2StateHash || stateInfo.fullPathHash == Slash3StateHash)
        {
            anim.SetBool(isAttackingHash,true);
        }
        
        //不攻擊時，攻擊Bool設為false，同時重置攻擊階段為1
        else
        {
            anim.SetBool(isAttackingHash,false);
            attackRound = 1;
            anim.SetInteger(AttackRoundHash,attackRound);

            //恢復受擊目標Tag
            hittedEnemies = GameObject.FindGameObjectsWithTag("Hitted");
            foreach(GameObject enemy in hittedEnemies)
            {
                enemy.tag = "Untagged";
            }
        }
    }

    //恢復受擊物件的Tag，從Hitted變回Untagged
    void ReverseHittedTag()
    {
        //Debug.Log(stateInfo.normalizedTime % 1.0f);

        if(stateInfo.fullPathHash != storeCurrentAnimStateHash && anim.GetBool("isAttacking"))
        {
            Debug.Log("Reset");
            
            hittedEnemies = GameObject.FindGameObjectsWithTag("Hitted");
            foreach(GameObject enemy in hittedEnemies)
            {
                enemy.tag = "Untagged";
            }
        }
    }
    
    //攻擊程式
    void Attack()
    {
        //重置攻擊值
        slashValue = slashOriginValue;

        //攻擊階段符合時，把連擊數增加，Animator使用增加後的連擊數當下一段攻擊之條件
        if(stateInfo.fullPathHash == Slash1StateHash && attackRound != 2)
        {
            attackRound = 2;
            anim.SetInteger(AttackRoundHash,attackRound);

            

        }
        if(stateInfo.fullPathHash == Slash2StateHash && attackRound !=3)
        {
            attackRound = 3;
            anim.SetInteger(AttackRoundHash,attackRound);

            
        }

        if(stateInfo.fullPathHash == Slash3StateHash )
        {
            
            
        }
        //衝刺動畫可強制啟動環形斬
        if(isDashing)
        {
            attackRound = 3;
            anim.SetInteger(AttackRoundHash,attackRound);

            
        }
    }
    
    
    //攻擊偵測
    void AttackDetect()
    {
        
        //在攻擊動畫期間執行
        if(anim.GetBool("isAttacking"))
        {
            //偵測敵人
            hitEnemies = Physics.OverlapBox(attackPoint.position, attackArea, greatSwordHand.transform.rotation, enemyLayers);
        
            //傷害敵人
            foreach(Collider enemy in hitEnemies)
            {
                if(enemy.tag != "Hitted") 
                {
                    enemy.tag = "Hitted";
                    //this.gameObject.tag = "Hitted";
                    Debug.Log("We hit" + enemy.name);
                }
                
            }
        }
    }

    //繪製攻擊區域
    void OnDrawGizmosSelected()
    {
        if(attackPoint == null)
            return;

        Gizmos.DrawWireCube(attackPoint.position,attackArea);        
    }

    //獲取輸入與讀取動畫資訊
    void GetInfo()
    {
        //獲取水平(X軸)與垂直(Z軸)輸入
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        //調取動畫階段
        stateInfo = anim.GetCurrentAnimatorStateInfo(0);
    }

}
