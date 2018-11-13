using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;

public enum InputType
{
    Teclado,
    Joystick,
}

[Serializable]
public class Movement
{
    #region Variables
    [Header("Establecer en Inspector")]
    [SerializeField] InputType inputMode;
    [SerializeField] GameObject tactileControls;
    [SerializeField] Joystick joystick;
    //private float speed { get; set; }
    //private float maxSpeed;
    private float jumpForce = 6f;
    private float extraJumpForce = 4f;
    private int extraJumps;
    private bool facingRight = true;
    public bool hit;
    //AbilityMode abilityMode;

    [Header("Checar collision")]
    [SerializeField]private Transform groundCheck;
    [SerializeField]private LayerMask whatIsGround;
    private const float radius = 0.02f;
    private bool grounded = false;

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

    public void MovementUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, radius, whatIsGround);
        float deltaX = 0f;

        switch (inputMode)
        {
            case InputType.Teclado:
                tactileControls.SetActive(false);
                deltaX = Input.GetAxisRaw("Horizontal") * stats.Speed;
                Move(deltaX);
                break;

            case InputType.Joystick:
                tactileControls.SetActive(true);
                deltaX = joystick.Horizontal * stats.Speed;
                Move(deltaX);
                break;
        }

        Jump();
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
            Debug.Log(timer);
            body2D.AddForce(new Vector2( 300 * knockback, 0f));
        }
        yield return 0f;                
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
}