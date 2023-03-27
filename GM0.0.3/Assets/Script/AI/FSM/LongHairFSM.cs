using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongHairFSM : BaseFSM
{
    [HideInInspector] public LongHairAttack EnemyAttack;

    private LongHairBaseState CurrentState;

    public LongHairIdle Idle = new LongHairIdle();
    public LongHairWalk Walk = new LongHairWalk();
    public LongHairDash Dash = new LongHairDash();
    public LongHairFight Attack = new LongHairFight();
    public LongHairSingleThron SingleThron = new LongHairSingleThron();

    public LongHairMutipleThron MultipleThron = new LongHairMutipleThron();
    public LongHairAirUmi AirUmi = new LongHairAirUmi();
    public LongHairGroundUmi GroundUmi = new LongHairGroundUmi();
    public LongHairHurt Hurt = new LongHairHurt();

    [Header("招式參數")]
    public float ThronAttackCD;
    public float UmiCD;

    [HideInInspector] public float LastUmiAttackTime;
    [HideInInspector] public float LastThronAttackTime;

    void Start()
    {
        BaseInitSet();

        EnemyAttack = this.GetComponent<LongHairAttack>();

        CurrentState = Idle;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CurrentState.UpdateState(this);
    }

    public void StateSwitch(LongHairBaseState NextState)
    {
        CurrentState = NextState;

        CurrentState.EnterState(this);
    }

    private void LongHairInitSet()
    {
        CurrentState = Idle;
        CurrentState.EnterState(this);

        LastUmiAttackTime = Time.time - UmiCD;
        LastThronAttackTime = Time.time - ThronAttackCD;
    }

    public void CallBrake(float Length)
    {
        StartCoroutine(EnemyMove.SuddenlyBrake(Length));
    }

    public void CallDash()
    {
        StartCoroutine(EnemyMove.Roll(EnemyState.MoveDirection,EnemyState.RollAniLength, EnemyMove.CharacterData.MaxMoveSpeed * EnemyMove.DashAdjust));
    }

    // 待優化,action layer的設定完全沒有用到

    // flip CD應該調整, 戰鬥狀態下0.3 非戰鬥3 

}
