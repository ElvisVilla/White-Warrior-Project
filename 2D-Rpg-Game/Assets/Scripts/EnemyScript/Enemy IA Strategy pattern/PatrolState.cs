using System;
using UnityEngine;

public class PatrolState : IState
{
    //Llevan acabo el cambio de "Estado/Cartucho" de la StateMachine class.
    public static event Action OnCombatState;
    public static event Action OnDeadState; //Eliminarse cuando se vaya el estado de Dead.

    GameObject ownerObj;
    Rigidbody2D body2D;
    Animator anim;
    BaseMovement enemyMotor;
    EnemyInfo enemyInfo;
    EnemyHealth health;

    public PatrolState(EnemyInfo ownerInfo, GameObject owner, Rigidbody2D rb, Animator animator, EnemyHealth enemyHealth)
    {
        enemyInfo = ownerInfo;
        ownerObj = owner;
        body2D = rb;
        anim = animator;
        health = enemyHealth;
    }

    public void Enter()
    {
        //Determinamos la direccion y la velocidad de movimiento de la IA al inicio del estado.
        //Es necesario determinar estos datos porque CombatState controla la direccion y velocidad de la IA a voluntad por razones de combate.
        
        bool movingRight = enemyInfo.facingRight;
        enemyMotor = new BaseMovement(enemyInfo, ownerObj, body2D, anim);
        enemyInfo.speed = (movingRight) ? enemyInfo.patrolSpeed : -enemyInfo.patrolSpeed;
        enemyMotor.Move(enemyInfo.speed);
    }

    public void Excecute()
    {
        var hitInfo = Physics2D.OverlapCircle(ownerObj.transform.position, enemyInfo.patrolRange, enemyInfo.necesaryLayers);
        enemyMotor.Move(enemyInfo.speed);
        PerformTransition(hitInfo);
    }

    private void PerformTransition(Collider2D hitInfo)
    {
        if (hitInfo != null)
            OnCombatState();


        //Debe eliminarse cuando elimine el estado de Dead.
        if ((health.CurrentHealth <= 0) && health.IsDead == false)
            OnDeadState();
    }

    public void Exit()
    {
    }
}
