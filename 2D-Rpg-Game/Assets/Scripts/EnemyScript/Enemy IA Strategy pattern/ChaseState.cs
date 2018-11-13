using System;
using UnityEngine;

public class ChaseState : IState
{
    //Llevan acabo el cambio de "Estado/Cartucho" de la StateMachine class.
    public static event Action OnPatrolState = delegate { };
    public static event Action OnCombatState = delegate { };
    public static event Action OnDeadState = delegate { };

    GameObject ownerObj;
    Rigidbody2D body2D;
    Animator anim;
    BaseMovement enemyMotor;
    EnemyInfo enemyInfo;
    EnemyHealth health;

    //enemyInfo ya tiene una referencia del jugador.
    GameObject playerObj;

    public ChaseState(EnemyInfo ownerInfo, GameObject owner, Rigidbody2D rb, Animator animator, EnemyHealth enemyHealth)
    {
        enemyInfo = ownerInfo;
        ownerObj = owner;
        body2D = rb;
        anim = animator;
        health = enemyHealth;
        playerObj = enemyInfo.playerReference;
    }

    public void Enter()
    {
        //Determinamos la direccion y la velocidad de movimiento de la IA al inicio del estado.
        //Es necesario determinar estos datos porque CombatState controla la direccion y velocidad de la IA a voluntad por razones de combate.

        bool movingRight = enemyInfo.facingRight;
        enemyInfo.speed = (movingRight) ? enemyInfo.chaseSpeed : -enemyInfo.chaseSpeed;
        enemyMotor = new BaseMovement(enemyInfo, ownerObj, body2D, anim);
        enemyMotor.Move(enemyInfo.speed);
    }

    public void Excecute()
    {
        //En este estado no obtenemos la distancia con la fisica porque los otros estados ya lo hacen y genera comportamientos extraños.
        float distance = Vector2.Distance(playerObj.transform.position, ownerObj.transform.position);

        PerformTransition(distance);
        enemyMotor.Move(enemyInfo.speed);
    }

    private void PerformTransition(float distance)
    {
        if (distance > 10)
            OnPatrolState();

        else if (distance < 4f)
            OnCombatState();

        if ((health.CurrentHealth <= 0) && !health.IsDead)
            OnDeadState();
    }

    public void Exit()
    {
    }
}
