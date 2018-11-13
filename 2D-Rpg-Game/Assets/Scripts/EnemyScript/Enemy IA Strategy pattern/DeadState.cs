using System;
using UnityEngine;


public class DeadState : IState
{
    GameObject ownerObj;
    Rigidbody2D body2D;
    Animator anim;
    BaseMovement enemyMotor;
    EnemyInfo enemyInfo;
    EnemyHealth health;

    public DeadState(EnemyInfo ownerInfo, GameObject owner, Rigidbody2D rb, Animator animator, EnemyHealth enemyHealth)
    {
        enemyInfo = ownerInfo;
        ownerObj = owner;
        body2D = rb;
        anim = animator;
        health = enemyHealth;
    }

    public void Enter()
    {
        enemyMotor = new BaseMovement(enemyInfo, ownerObj, body2D, anim);
    }

    public void Excecute()
    {
        enemyMotor.Move(0f);

        if(health.IsDead == false)
            health.Die();
    }

    public void Exit()
    {
    }
}
