using UnityEngine;

public abstract class State
{
    protected Rigidbody2D body2D;
    protected Animator anim; 
    protected EnemyInfo enemyInfo;
    protected EnemyHealth health;
    protected Enemy enemy;
    protected EnemyMovement motor;

    public State(Enemy IA)
    {
        enemy = IA;
        body2D = enemy.Body2D;
        anim = enemy.Anim;
        enemyInfo = enemy.EnemyInfo;
        health = enemy.Health;
        motor = enemy.Motor;
    }

    public abstract void Enter();
    public abstract void Excecute();
    public abstract void Exit();
}

