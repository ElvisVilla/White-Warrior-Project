using System;
using UnityEngine;


public class DeadState : State
{

    public DeadState(Enemy ia) : base(ia)
    {

    }

    public override void Enter()
    {
    }

    public override void Excecute()
    {
        motor.PerformNormalMovement(0f);
        if(!health.IsDead)
            health.Die();
    }

    public override void Exit()
    {
    }
}
