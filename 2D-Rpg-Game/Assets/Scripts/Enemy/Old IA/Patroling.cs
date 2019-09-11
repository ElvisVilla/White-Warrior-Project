using System;
using UnityEngine;

public class Patroling : State
{
    public event Action OnCombatState;
    public event Action OnDeadState;
    PlayerHealth playerHealth;

    public Patroling(Enemy enemy) : base(enemy)
    {
    }

    public override void Enter()
    {
        enemyInfo.PlayerReference = null;
        bool movingRight = enemy.FacingRight;
        enemy.Speed = (movingRight) ? enemyInfo.patrolSpeed : -enemyInfo.patrolSpeed;
        motor.PerformNormalMovement(enemy.Speed);
    }

    public override void Excecute()
    {
        var hitInfo = new Collider2D();
        if (enemyInfo.PlayerReference == null)
        hitInfo = Physics2D.OverlapCircle(enemy.transform.position, enemyInfo.patrolRange, enemyInfo.WhatIsPlayer);
        motor.PerformNormalMovement(enemy.Speed);
        PerformTransition(hitInfo);
    }

    private void PerformTransition(Collider2D hitInfo)
    {
        //Establece el componente una sola vez.
        hitInfo?.transform.GetComponentOnce(ref playerHealth);

        if (hitInfo != null && playerHealth.CurrentHealth > 0)
            OnCombatState();

        if ((health.CurrentHealth <= 0) && health.IsDead == false)
            OnDeadState();
    }

    public override void Exit()
    {
    }
}
