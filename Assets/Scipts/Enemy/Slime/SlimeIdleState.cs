using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeIdleState : EnemyState
{

    private Enemy_Slime enemy;

    public SlimeIdleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Slime _enemy) : base(_enemy, _stateMachine, _animBoolName)
    {

        enemy = _enemy;

    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = 5f;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if(stateTimer < 0)
            stateMachine.ChangeState(enemy.moveState);
    }
}
