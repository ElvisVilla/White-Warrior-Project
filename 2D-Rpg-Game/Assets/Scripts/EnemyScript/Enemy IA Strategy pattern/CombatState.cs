using System;
using UnityEngine;

public class CombatState : IState
{
    //Llevan acabo el cambio de "Estado/Cartucho" de la StateMachine class.
    public static event Action OnChaseState = delegate { };
    public static event Action OnPatrolState = delegate { };
    public static event Action OnDeadState = delegate { };

    GameObject ownerObj;
    Rigidbody2D body2D;
    Animator anim;
    BaseMovement enemyMotor;
    EnemyInfo enemyInfo;
    EnemyHealth health;

    public CombatState(EnemyInfo info, GameObject ownerObj,
        Rigidbody2D rb, Animator animator, EnemyHealth enemyHealth)
    {
        enemyInfo = info;
        this.ownerObj = ownerObj;
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
        //Recuperamos la posicion del jugador mediante Physics.OverlapCircle, establecemos el LayerMask para el jugador solamente
        var playerTemporalPosistion = Vector2.zero;
        var enemyTemporalPosition = ownerObj.transform.position;

        var hitInfo = Physics2D.OverlapCircle(ownerObj.transform.position, 5f, enemyInfo.WhatIsPlayer);

        if (hitInfo != null)
            playerTemporalPosistion = hitInfo.transform.position;

        var distance = Vector2.Distance(playerTemporalPosistion, ownerObj.transform.position);
         
        //Comportamiento del combat state.
        //Queremos que el ataque se gestione desde aca utilizando el IACombatSystem
        
        //El comportamiento de movimiento se lleva acabo gracias al EnemyMotor.
        enemyMotor.CombatMovement(playerTemporalPosistion, enemyTemporalPosition, distance);

        //El cambio de estados.
        PerformChaseTransition(distance);
    }

    private void PerformChaseTransition(float distance)
    {
        var groundInfo = Physics2D.Raycast(enemyInfo.groundDetector.position, Vector2.down, enemyInfo.rayDistance, enemyInfo.whatIsGround);

        if (groundInfo.collider == false)
            OnPatrolState();

        if (distance > 5)
            OnChaseState();

        if((health.CurrentHealth <= 0) && !health.IsDead)
            OnDeadState();
    }

    public void Exit() { }
}
