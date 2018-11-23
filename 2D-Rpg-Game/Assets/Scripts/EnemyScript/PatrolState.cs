using System;
using UnityEngine;

public class PatrolState : State
{
    public event Action OnCombatState;
    public event Action OnDeadState;

    public PatrolState(Enemy enemy) : base(enemy)
    {
    }

    public override void Enter()
    {
        enemyInfo.PlayerReference = null;
        bool movingRight = enemy.FacingRight;
        enemy.Speed = (movingRight) ? enemyInfo.patrolSpeed : -enemyInfo.patrolSpeed;
        motor.PerformMovement(enemy.Speed);
    }

    public override void Excecute()
    {
        var hitInfo = new Collider2D();
        if (enemyInfo.PlayerReference == null)
        hitInfo = Physics2D.OverlapCircle(enemy.transform.position, enemyInfo.patrolRange, enemyInfo.WhatIsPlayer);
        motor.PerformMovement(enemy.Speed);
        PerformTransition(hitInfo);
    }

    private void PerformTransition(Collider2D hitInfo)
    {
        if (hitInfo != null && hitInfo.transform.GetComponent<PlayerHealth>().CurrentHealth > 0)
            OnCombatState();

        if ((health.CurrentHealth <= 0) && health.IsDead == false)
            OnDeadState();
    }

    public override void Exit()
    {

    }
}
