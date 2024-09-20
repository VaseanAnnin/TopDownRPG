using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Slime : Enemy
{

    public SlimeIdleState idleState {  get; private set; }
    public SlimeMoveState moveState { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        idleState = new SlimeIdleState(this, stateMachine, "Idle", this);
        moveState = new SlimeMoveState(this, stateMachine, "Move", this);
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();

     
        

       
        collisionChecks();

        FlipControllerHorizontal();

        OnDrawGizmos();
    }
}
