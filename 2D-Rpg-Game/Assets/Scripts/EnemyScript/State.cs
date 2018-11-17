using UnityEngine;

public abstract class State
{
    protected Rigidbody2D body2D;
    protected Animator anim; 
    protected EnemyInfo enemyInfo;
    protected EnemyHealth health;
    protected Enemy enemy;
    protected EnemyMovement motor;

    public State(Enemy enemyIA)
    {
        enemy = enemyIA;
        body2D = enemy.Body2D;
        anim = enemy.Anim;
        enemyInfo = enemy.EnemyInfo;
        health = enemy.Health;
        motor = new EnemyMovement(enemyIA);
    }

    public abstract void Enter();
    public abstract void Excecute();
    public abstract void Exit();
}
