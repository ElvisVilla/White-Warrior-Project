using UnityEngine;

public class EnemyMovement
{
    //Pendiente: Evaluar un posible patron de estrategia, para diferentes estilos de movimientos.
    Rigidbody2D body2D;
    Animator anim;
    EnemyInfo enemyInfo;
    Enemy enemy;

    int speedAnimatorHash = Animator.StringToHash("Speed");
    //float variableSpeed = 0f;

    public EnemyMovement(Enemy enemy)
    {
        body2D = enemy.Body2D;
        anim = enemy.Anim;
        enemyInfo = enemy.EnemyInfo;
        this.enemy = enemy;
    }

    //Movimiento base para Chase y Patrol, estos 2 estados determinan hacia donde van.
    public void PerformMovement(float speed)
    {
        Move(speed);
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
                //enemyInfo.combatSpeed = -2.5f;
                //enemyInfo.facingRight = true;
                enemy.Speed = -enemyInfo.combatSpeed;
                Move(enemy.Speed);
                enemy.FacingRight = true;
                
            }
            else if (playerPos.x > enemyPos.x)
            {
                //enemyInfo.combatSpeed = 2.5f;
                //enemyInfo.facingRight = false;
                enemy.Speed = enemyInfo.combatSpeed;
                Move(enemy.Speed);
                enemy.FacingRight = false;
                
            }

            if (enemy.FacingRight)
            {
                enemy.gameObject.transform.eulerAngles = new Vector2(0, -180f);
                //enemyInfo.facingRight = false;
                enemy.FacingRight = false;
            }
            else
            {
                enemy.gameObject.transform.eulerAngles = new Vector2(0, 0);
                //enemyInfo.facingRight = true;
                enemy.FacingRight = true;
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
            if (enemy.FacingRight == true) //enemyInfo.facingRight == true
            {
                enemy.gameObject.transform.eulerAngles = new Vector2(0, -180);
                enemy.FacingRight = false; //enemyInfo.facingRight = false;
                enemy.Speed = -1.2f; //enemyInfo.normalSpeed = -1.2f;
            }
            else
            {
                enemy.gameObject.transform.eulerAngles = new Vector2(0, 0);
                enemy.FacingRight = true; //enemyInfo.facingRight = true;
                enemy.Speed = 1.2f; // enemyInfo.normalSpeed = 1.2f;
            }
        }
    }

    void Move(float speed)
    {
        anim.SetFloat(speedAnimatorHash, speed);
        body2D.MovePosition(body2D.position + Vector2.right * speed * Time.fixedDeltaTime);
    }
}
