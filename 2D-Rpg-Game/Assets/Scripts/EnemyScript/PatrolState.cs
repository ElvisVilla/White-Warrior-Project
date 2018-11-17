using System;
using UnityEngine;

public class PatrolState : State
{
    public event Action OnCombatState;
    public event Action OnDeadState;

    //float speed;

    public PatrolState(Enemy enemy) : base(enemy)
    {
    }

    public override void Enter()
    {
        //motor.FacingRight = enemyInfo.facingRight;
        bool movingRight = enemy.FacingRight;
        enemy.Speed = (movingRight) ? enemyInfo.patrolSpeed : -enemyInfo.patrolSpeed;
        motor.PerformMovement(enemy.Speed);
    }

    public override void Excecute()
    {
        var hitInfo = Physics2D.OverlapCircle(enemy.transform.position, enemyInfo.patrolRange, enemyInfo.WhatIsPlayer);
        motor.PerformMovement(enemy.Speed);
        PerformTransition(hitInfo);
    }

    private void PerformTransition(Collider2D hitInfo)
    {
        if (hitInfo != null)
            OnCombatState();

        if ((health.CurrentHealth <= 0) && health.IsDead == false)
            OnDeadState();
    }

    public override void Exit()
    {
    }
}
