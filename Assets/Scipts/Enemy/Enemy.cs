using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
   

    public EnemyStateMachine stateMachine { get; private set; }

    public float RandomMovementRange = 5f;
    public float RandomMovementSpeed = 1f;


    protected override void Awake()
    {
        base.Awake();
        stateMachine = new EnemyStateMachine();
    }

    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
    }

    public void MoveEnemy(Vector2 velocity)
    {
        rb.velocity = velocity;
    }
}
