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

    [Header("©Û¦¡°Ñ¼Æ")]
    public float ThronAttackCD;
    public float UmiCD;

    [HideInInspector] public float LastUmiAttackTime;
    [HideInInspector] public float LastThronAttackTime;

    void Start()
    {
        BaseInitSet();

        EnemyAttack = this.GetComponent<LongHairAttack>();

    }

    // Update is called once per frame
    void Update()
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
}
