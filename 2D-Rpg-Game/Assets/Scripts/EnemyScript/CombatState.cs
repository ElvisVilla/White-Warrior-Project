using System;
using UnityEngine;

public class CombatState : State
{
    //Llevan acabo el cambio de "Estado/Cartucho" de la StateMachine class.
    public event Action OnChaseState = delegate { };
    public event Action OnPatrolState = delegate { };
    public event Action OnDeadState = delegate { };

    public CombatState(Enemy enemy) : base(enemy)
    {
    }

    public override void Enter()
    {
    }

    public override void Excecute()
    {
        //Recuperamos la posicion del jugador mediante Physics.OverlapCircle, establecemos el LayerMask para el jugador solamente
        var playerPosition = Vector2.zero;
        var enemyPosition = enemy.transform.position;

        var hitInfo = Physics2D.OverlapCircle(enemy.transform.position, enemyInfo.combatRange, enemyInfo.WhatIsPlayer);

        if (hitInfo != null)
        {
            playerPosition = hitInfo.transform.position;
            enemyInfo.PlayerReference = hitInfo.gameObject;
        }

        var distance = Vector2.Distance(playerPosition, enemyPosition);
        
        motor.PerfomCombatMovement(playerPosition, enemyPosition, distance);
        PerformTransition(distance);
    }

    private void PerformTransition(float distance)
    {
        var groundInfo = Physics2D.Raycast(enemyInfo.groundDetector.position, Vector2.down, enemyInfo.rayDistance, enemyInfo.whatIsGround);

        if (groundInfo.collider == false)
            OnPatrolState();

        if (distance > enemyInfo.combatRange)
            OnChaseState();

        if((health.CurrentHealth <= 0) && !health.IsDead)
            OnDeadState();

        if (enemyInfo.PlayerReference.GetComponent<PlayerHealth>().CurrentHealth <= 0)
            OnPatrolState();
    }

    public override void Exit() { }
}
