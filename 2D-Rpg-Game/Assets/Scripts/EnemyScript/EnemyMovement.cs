using System.Collections;
using UnityEngine;
using DG.Tweening;

public enum EnemyMovementState
{
    OnControl,
    NoControl,
}

public class EnemyMovement
{
    //Pendiente: Evaluar un posible patron de estrategia, para diferentes estilos de movimientos.
    Rigidbody2D body2D;
    Animator anim;
    EnemyInfo enemyInfo;
    Enemy enemy;

    EnemyMovementState state;
    int speedAnimatorHash = Animator.StringToHash("Speed");
    float knockbackAmount = 1f;
    float KnockbackTime = 0.06f;

    public EnemyMovement(Enemy enemy)
    {
        body2D = enemy.Body2D;
        anim = enemy.Anim;
        enemyInfo = enemy.EnemyInfo;
        this.enemy = enemy;
    }

    public void PerformNormalMovement(float speed)
    {
        Move(speed);
        FlipBehaviour();
    }

    void FlipBehaviour()
    {
        var groundInfo = Physics2D.Raycast(enemyInfo.groundDetector.position, Vector2.down, enemyInfo.rayDistance, enemyInfo.whatIsGround);

        if (groundInfo.collider == false)
        {
            if (enemy.FacingRight == true)
            {
                enemy.gameObject.transform.eulerAngles = new Vector2(0, -180);
                enemy.FacingRight = false;
                enemy.Speed = -1.2f;
            }
            else
            {
                enemy.gameObject.transform.eulerAngles = new Vector2(0, 0);
                enemy.FacingRight = true;
                enemy.Speed = 1.2f;
            }
        }
    }

    void Move(float speed)
    {
        anim.SetFloat(speedAnimatorHash, speed);
        body2D.MovePosition(body2D.position + Vector2.right * speed * Time.fixedDeltaTime);
    }

    public void PerfomCombatMovement(Vector2 playerPos, Vector2 enemyPos, float distance)
    {
        switch (state)
        {
            case EnemyMovementState.OnControl:
                CombatMovement(playerPos, enemyPos, distance);
                break;
            case EnemyMovementState.NoControl:
                anim.SetFloat(speedAnimatorHash, 0f);
                break;
        }
    }

    public IEnumerator OnKnockBack()
    {
        state = EnemyMovementState.NoControl;
        float knockBackDirection = (body2D.transform.rotation.y == 0) ? -1f : 1f;
        body2D.DOMoveX(body2D.position.x + knockbackAmount * knockBackDirection, KnockbackTime, false);
        yield return new WaitForSeconds(0.3f);
        state = EnemyMovementState.OnControl;
    }

    private void CombatMovement(Vector2 playerPos, Vector2 enemyPos, float distance)
    {
        if (distance > enemyInfo.stopingDistance)
        {
            if (playerPos.x < enemyPos.x)
            {
                enemy.Speed = -enemyInfo.combatSpeed;
                Move(enemy.Speed);
                enemy.FacingRight = true;

            }
            else if (playerPos.x > enemyPos.x)
            {
                enemy.Speed = enemyInfo.combatSpeed;
                Move(enemy.Speed);
                enemy.FacingRight = false;

            }

            if (enemy.FacingRight)
            {
                enemy.gameObject.transform.eulerAngles = new Vector2(0, -180f);
                enemy.FacingRight = false;
            }
            else
            {
                enemy.gameObject.transform.eulerAngles = new Vector2(0, 0);
                enemy.FacingRight = true;
            }
        }
        else if (distance < enemyInfo.stopingDistance || distance == 0f)
        {
            PerformNormalMovement(0);
        }
    }
}
