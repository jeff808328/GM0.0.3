using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetect : CommonDetect
{
    [Header("翻轉偵測")]
    public float FlipDetectLength;// 碰牆翻轉
    public float FlipDetectXrayOffset;
    public float FlipDetectYrayOffset;
    [SerializeField] private Vector2 FlipDetectSource;

    [Header("角色視野設定")]
    public LayerMask Player;

    public float PlayerDetectLength;
    public float PlayerDetectXrayOffset;
    public float PlayerDetectYrayOffset;
    private Vector2 PlayerDetectSource;

    private Vector2 ViewBoxPos;
    private Vector2 ViewBoxSize;
    private Rigidbody2D ViewBox;

    public float ViewBoxHeight;
    public float ViewBoxWidth;
    public float ViewBoxHeightOffset;
    public float ViewBoxWidthOffset;

    [Header("玩家距離計算")]
    public float MidMini; // 中間段最小值
    private float MidMiniOri;

    public float MidMax; // 中間段最大值
    private float MidMaxOri;

    public float RamdonValue; // 調整用亂數
    public float Buffer; // 中間段長度最小值

    private Vector3 PlayerPos;
    [SerializeField] private float Distance;

    private Vector3 MidMiniPos;
    private Vector3 MidMaxPos; //show 距離偵測狀態

    private float LastReactTime;

    [Header("角色普通攻擊觸發設定")]
    public float AtkTriggerBoxHeight;
    public float AtkTriggerBoxWidth;
    public float AtkTriggerBoxHeightOffset;
    public float AtkTriggerBoxWidthOffset;

    private Vector2 AtkTriggerBoxPos;
    private Vector2 AtkTriggerBoxSize;
    private Rigidbody2D AtkTriggerBox;

    [Header("角色特殊攻擊觸發設定")]
    public float SpTriggerBoxHeight;
    public float SpTriggerBoxWidth;
    public float SpTriggerBoxHeightOffset;
    public float SpTriggerBoxWidthOffset;

    private Vector2 SpTriggerBoxPos;
    private Vector2 SpTriggerBoxSize;
    private Rigidbody2D SpTriggerBox;

    private EnemyState EnemyState;

    void Start()
    {
        CommonState = this.GetComponent<EnemyState>();
        EnemyState = this.GetComponent<EnemyState>();

        EnemyInitSet();
    }

    void FixedUpdate()
    {
        CalculationPlayerDistance();

        BaseDetectBoxUpdate();

        EnemyDetectBoxUpdate();

        EnemyDetectRayUpdate();

        //if (EnemyState.PlayerInView & LastReactTime + EnemyState.ReactionTime < Time.time)
        //{
        //    CalculationPlayerDistance();
        //}



        Debug.DrawRay(FlipDetectSource, new Vector2(FlipDetectLength * EnemyState.MoveDirection, 0), Color.white, 0.1f);

        Debug.DrawRay(PlayerDetectSource, new Vector3(PlayerDetectLength * EnemyState.MoveDirection, 0, 0), Color.green, 0.1f);
    }

    private void EnemyInitSet()
    {
        MidMiniOri = MidMini;
        MidMaxOri = MidMax;

        LastReactTime = Time.time - EnemyState.ReactionTime - 1;
    }

    private void EnemyDetectBoxUpdate()
    {
        ViewBoxSize = new Vector2(ViewBoxWidth, transform.lossyScale.y * ViewBoxHeight);
        AtkTriggerBoxSize = new Vector2(AtkTriggerBoxWidth, transform.lossyScale.y * AtkTriggerBoxHeight);
        SpTriggerBoxSize = new Vector2(SpTriggerBoxWidth, transform.lossyScale.y * SpTriggerBoxHeight);

        ViewBoxPos = new Vector2(this.transform.position.x + ViewBoxWidthOffset * EnemyState.MoveDirection, this.transform.position.y + ViewBoxHeightOffset);
        AtkTriggerBoxPos = new Vector2(this.transform.position.x + AtkTriggerBoxWidthOffset * EnemyState.MoveDirection, this.transform.position.y + AtkTriggerBoxHeightOffset);
        SpTriggerBoxPos = new Vector2(this.transform.position.x + SpTriggerBoxWidthOffset * EnemyState.MoveDirection, this.transform.position.y + SpTriggerBoxHeightOffset);

        EnemyState.PlayerInView = Physics2D.OverlapBox(ViewBoxPos, ViewBoxSize, 0, Player);
        EnemyState.PlayerInAttakRange = Physics2D.OverlapBox(AtkTriggerBoxPos, AtkTriggerBoxSize, 0, Player);
        EnemyState.PlayerInSpAttackRange = Physics2D.OverlapBox(SpTriggerBoxPos, SpTriggerBoxSize, 0, Player);

        if (EnemyState.PlayerInView)
        {
            var pos = Physics2D.OverlapBox(ViewBoxPos, ViewBoxSize, 0, Player);

            PlayerPos = pos.gameObject.transform.position;

            //   Debug.Log("find player");
        }
        else
        {
            Debug.Log("lost player");
        }
    }

    private void EnemyDetectRayUpdate()
    {
        // 牆壁偵測 
        FlipDetectSource = new Vector2(transform.position.x + FlipDetectXrayOffset * EnemyState.MoveDirection, transform.position.y + FlipDetectYrayOffset);

        RaycastHit2D FlipDetect = Physics2D.Raycast(FlipDetectSource, new Vector2(EnemyState.MoveDirection * FlipDetectLength, 0), 1);


        if (FlipDetect.collider.tag == "Wall" & FlipDetect.collider.gameObject != null )
        {
            EnemyState.NearingWall = true;
        }
        else
        {
            EnemyState.NearingWall = false;
        }

        // 玩家觸地偵測
        PlayerDetectSource = new Vector2(transform.position.x + PlayerDetectXrayOffset * EnemyState.MoveDirection, transform.position.y + PlayerDetectYrayOffset);

        RaycastHit2D PlayerGroundDetect = Physics2D.Raycast(PlayerDetectSource, new Vector2(EnemyState.MoveDirection, 0), Mathf.Infinity);

        if (PlayerGroundDetect.collider.tag == "Player")
        {
            EnemyState.PlayerOnGround = true;
        }
        else
        {
            EnemyState.PlayerOnGround = false;
        }
        // caldistance的drawray還是要有
    }

    private void CalculationPlayerDistance()
    {
       // Debug.Log("rr");

        MidMax = MidMax + Random.Range(-RamdonValue - 1, RamdonValue + 1);
        MidMini = MidMini + Random.Range(-RamdonValue - 1, RamdonValue + 1);

        Distance = Mathf.Abs(PlayerPos.x - transform.position.x);

        if (Distance > MidMax)
            EnemyState.PlayerDistanceIndex = 2;
        else if (Distance < MidMini)
            EnemyState.PlayerDistanceIndex = 0;
        else
            EnemyState.PlayerDistanceIndex = 1;

        MidMax = MidMaxOri;
        MidMini = MidMiniOri;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(ViewBoxPos, ViewBoxSize);

        Gizmos.color = Color.gray;
        Gizmos.DrawWireCube(AtkTriggerBoxPos, AtkTriggerBoxSize);

        Gizmos.color = Color.gray;
        Gizmos.DrawWireCube(SpTriggerBoxPos, SpTriggerBoxSize);

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(GroundBoxPos, GroundBoxSize);
        Gizmos.DrawWireCube(RightWallBoxPos, WallBoxSize);
        Gizmos.DrawWireCube(LeftWallBoxPos, WallBoxSize);
    }
}
