using UnityEngine;

public class BaseMovement
{
    //Pendiente: Evaluar un posible patron de estrategia, para diferentes estilos de movimientos.

    Rigidbody2D body2D;
    Animator anim;
    EnemyInfo enemyInfo;
    GameObject ownerObj;

    int speedAnimatorHash = Animator.StringToHash("Speed");

    public BaseMovement(EnemyInfo ownerInfo, GameObject owner,Rigidbody2D rb, Animator animator)
    {
        body2D = rb;
        anim = animator;
        enemyInfo = ownerInfo;
        ownerObj = owner;
    }

    //Movimiento base para Chase y Patrol, estos 2 estados determinan hacia donde van.
    public void Move(float speed)
    {
        PerformMovement(speed);
        FlipBehaviour();
    }

    //Movimiento especial para CombatState.
    //Pendiente: Revisar las desiciones y evaluar un posible patron de estrategia.
    public void CombatMovement(Vector2 playerPos, Vector2 enemyPos, float distance)
    {
        if (distance > enemyInfo.stopingDistance)
        {
            if (playerPos.x < enemyPos.x)
            {
                enemyInfo.combatSpeed = -2.3f;
                PerformMovement(enemyInfo.combatSpeed);
                enemyInfo.facingRight = true;
            }
            else if (playerPos.x > enemyPos.x)
            {
                enemyInfo.combatSpeed = 2.3f;
                PerformMovement(enemyInfo.combatSpeed);
                enemyInfo.facingRight = false;
            }

            if (enemyInfo.facingRight)
            {
                anim.transform.eulerAngles = new Vector2(0, -180f);
                enemyInfo.facingRight = false;
            }
            else
            {
                ownerObj.transform.eulerAngles = new Vector2(0, 0);
                enemyInfo.facingRight = true;
            }
        }
        else if (distance < enemyInfo.stopingDistance || distance == 0f)
        {
            PerformMovement(0);
        }
    }

    //Comportamiento de giro de sprite.
    void FlipBehaviour()
    {
        var groundInfo = Physics2D.Raycast(enemyInfo.groundDetector.position, Vector2.down, enemyInfo.rayDistance, enemyInfo.whatIsGround);

        if (groundInfo.collider == false)
        {
            if (enemyInfo.facingRight == true)
            {
                ownerObj.transform.eulerAngles = new Vector2(0, -180);
                enemyInfo.facingRight = false;
                enemyInfo.speed = -1.2f;
            }
            else
            {
                ownerObj.transform.eulerAngles = new Vector2(0, 0);
                enemyInfo.facingRight = true;
                enemyInfo.speed = 1.2f;
            }
        }
    }

    void PerformMovement(float speed)
    {
        anim.SetFloat(speedAnimatorHash, speed);
        body2D.MovePosition(body2D.position + Vector2.right * speed * Time.fixedDeltaTime);
    }
}
