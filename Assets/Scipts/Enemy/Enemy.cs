using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Rigidbody2D rb { get; private set; }
    public Animator anim { get; private set; }

    public EnemyStateMachine stateMachine { get; private set; }


<<<<<<< HEAD
    protected override void Awake()
=======
    private void Awake()
>>>>>>> parent of a0aba26 (Merge branch 'Backup' into New-Head)
    {
        stateMachine = new EnemyStateMachine();
    }

    void Update()
    {
        stateMachine.currentState.Update();
    }
}
