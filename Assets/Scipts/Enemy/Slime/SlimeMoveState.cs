using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeMoveState : EnemyState
{

    private Enemy_Slime enemy;

    private Vector3 _targetPos;
    private Vector3 _direction;

    public SlimeMoveState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Slime _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();

        _targetPos = GetRandomPointInCircle();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        _direction = (_targetPos - enemy.transform.position).normalized;

        enemy.MoveEnemy(_direction * enemy.RandomMovementSpeed);

        if((enemy.transform.position - _targetPos).sqrMagnitude < 0.01f)
        {
            _targetPos = GetRandomPointInCircle();
        }
    }

    private Vector3 GetRandomPointInCircle()
    {
        return enemy.transform.position + (Vector3)UnityEngine.Random.insideUnitCircle * enemy.RandomMovementRange;
    }
}
