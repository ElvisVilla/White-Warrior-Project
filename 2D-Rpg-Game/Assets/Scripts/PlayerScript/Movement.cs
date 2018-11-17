using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class Movement
{
    #region Variables
    [Header("Establecer en Inspector")]
    [SerializeField] InputType inputMode;
    [SerializeField] GameObject tactileControls;
    [SerializeField] Joystick joystick;
    private float jumpForce = 6f;
    private float extraJumpForce = 4f;
    private int extraJumps;
    private bool facingRight = true;
    public PlayerState State { get; set; }

    [Header("Checar collision")]
    [SerializeField]private Transform groundCheck;
    [SerializeField]private LayerMask whatIsGround;
    private const float radius = 0.02f;
    private bool grounded = false;

    [Header("Para WallJump")]
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask whatIsWall;
    public float wallContactRadius;
    public bool wallContact;

    Rigidbody2D body2D;
    Animator anim;
    CharacterStats stats;
    #endregion

    public void Init(Player player)
    {
        body2D = player.Body2D;
        anim = player.Anim;
        stats = player.Stats;
    }

    public void MovementUpdate(Player player)
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, radius, whatIsGround);
        wallContact = Physics2D.OverlapCircle(wallCheck.position, wallContactRadius, whatIsWall);

        float deltaX = 0f;
        
        switch (State)
        {
            case PlayerState.OnControll:
                switch (inputMode)
                {
                    case InputType.Teclado:
                        tactileControls.SetActive(false);
                        deltaX = Input.GetAxisRaw("Horizontal") * stats.Speed;
                        break;

                    case InputType.Joystick:
                        tactileControls.SetActive(true);
                        if(joystick.Horizontal >= 0.1f)
                        {
                            deltaX = 1 * stats.Speed;
                        }
                        else if(joystick.Horizontal <= -0.1f)
                        {
                            deltaX = -1f * stats.Speed;
                        }
                        break;
                }
                break;
            case PlayerState.OnInteraction:
                deltaX = 0f;
                break;
            case PlayerState.OnDamage:
                deltaX = 0f;
                break;
        }

        //PlayerStates
        //Lo que haremos es definir stados en los que se ejecutara una logica y dependiendo de la logica, el input no tendra efecto.
        Move(deltaX);
        Jump();
        WallJump(player);
    }

    void Move(float speed)
    {
        //Controlador de movimiento de personaje.
        body2D.velocity = new Vector2(speed, body2D.velocity.y);
            
        
        //Girando el sprite.
        if (speed > 0 && !facingRight)
            Flip();
            
        else if (speed < 0 && facingRight)
            Flip();

        //Refresca la animacion SetBool(Falling);
        bool falling = !grounded;

        //animaciones
        anim.SetFloat("Speed", speed);
        anim.SetBool("Grounded", grounded);
        anim.SetBool("Falling", falling);
    }

    public IEnumerator OnHit(float nockbackDuration)
    {
        float timer = 0f;
        float knockback = (body2D.transform.localScale.x > 0) ? -1f: 1f;
        while (nockbackDuration > timer)
        {
            timer += Time.deltaTime;
            body2D.AddForce(new Vector2(200 * knockback, 0f));
        }
        yield return 0f;
    }

    public IEnumerator OnHit()
    {
        State = PlayerState.OnDamage;
        float knockback = (body2D.transform.localScale.x > 0) ? -1f : 1f;
        body2D.AddForce(new Vector2(1500 * knockback, 0f));
        yield return new WaitForSeconds(0.31f);
        State = PlayerState.OnControll;
    }

    void Jump()
    {
        //Cheking for firt jump.
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            anim.SetTrigger("Salto");
            body2D.velocity = Vector2.up * jumpForce;
            extraJumps = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && (extraJumps > 0) && !grounded)
        {
            anim.SetTrigger("Salto");
            body2D.velocity = Vector2.up * extraJumpForce;
            extraJumps--;
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector2 tempScale = body2D.transform.localScale;
        tempScale.x *= -1;
        body2D.transform.localScale = tempScale;
    }

    //Se llama desde el boton.
    public void JumpForTactil()
    {
        if (grounded)
        {
            anim.SetTrigger("Salto");
            body2D.velocity = Vector2.up * jumpForce;
            extraJumps = 1;
        }
        else if ((extraJumps > 0) && !grounded)
        {
            anim.SetTrigger("Salto");
            body2D.velocity = Vector2.up * extraJumpForce;
            extraJumps--;
        }
    }

    void WallJump(Player player)
    {
        if (wallContact && Input.GetKeyDown(KeyCode.K))
        {
            body2D.velocity = new Vector2(body2D.velocity.x, body2D.velocity.y * 7);
        }
    }

    public IEnumerator OnDamage()
    {
        State = PlayerState.OnDamage;
        yield return new WaitForSeconds(0.3f);

        State = PlayerState.OnControll;
    }

    public void OnInteraction(bool activation)
    {
        if (activation)
        {
            State = PlayerState.OnInteraction;
        }
        else
        {
            State = PlayerState.OnControll;
        }
    }

    public enum InputType
    {
        Teclado,
        Joystick,
    }

    public enum PlayerState
    {
        OnControll,
        OnDamage,
        OnInteraction,
    }


}