using System;
using UnityEngine;

public class ChaseState : State
{
    //Llevan acabo el cambio de "Estado/Cartucho" de la StateMachine class.
    public event Action OnPatrolState = delegate { };
    public event Action OnCombatState = delegate { };
    public event Action OnDeadState = delegate { };

    public ChaseState(Enemy ia) : base(ia)
    {
    }

    public override void Enter()
    {
        bool movingRight = enemy.FacingRight;
        enemy.Speed = (movingRight) ? enemyInfo.chaseSpeed : -enemyInfo.chaseSpeed;
        motor.PerformNormalMovement(enemy.Speed);
    }

    public override void Excecute()
    {
        float distance = Vector2.Distance(enemyInfo.PlayerReference.transform.position, enemy.transform.position);

        PerformTransition(distance);
        motor.PerformNormalMovement(enemy.Speed);
    }

    private void PerformTransition(float distance)
    {
        if (distance > enemyInfo.chaseRange)
            OnPatrolState();

        else if (distance < 4)
            OnCombatState();

        if ((health.CurrentHealth <= 0) && !health.IsDead)
            OnDeadState();
    }

    public override void Exit()
    {
    }
}
