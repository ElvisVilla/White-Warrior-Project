using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class Movement
{
    #region Variables
    [Header("Establecer en Inspector")]
    [SerializeField] InputType inputMode;
    Joystick joystick;
    private int extraJumps;
    private bool facingRight = true;
    public PlayerState State { get; set; }

    [Header("Checar collision")]
    [SerializeField]private Transform groundChecker;
    [SerializeField]private LayerMask whatIsGround;
    private const float radius = 0.02f;
    private bool grounded = false;

    [Header("Sound")]
    [SerializeField] private AudioClip groundContact;
    [Range(0f, 1f)] [SerializeField] private float Volume;
    bool alreadyPlayed = false;

    AudioSource source;
    Rigidbody2D body2D;
    Animator anim;
    CharacterStats stats;
    #endregion

    public void Init(Player player)
    {
        body2D = player.Body2D;
        anim = player.Anim;
        stats = player.Stats;
        source = player.Source;
        joystick = GameObject.FindObjectOfType<Joystick>();
        alreadyPlayed = false;
    }

    public void MovementUpdate(Player player)
    {
        grounded = Physics2D.OverlapCircle(groundChecker.position, radius, whatIsGround);
        float deltaX = 0f;
        
        switch (State)
        {
            case PlayerState.OnControll:
                Jump();
                switch (inputMode)
                {
                    case InputType.Teclado:
                        joystick.gameObject.SetActive(false);
                        deltaX = Input.GetAxisRaw("Horizontal") * stats.Speed;
                        break;

                    case InputType.Joystick:
                        joystick.gameObject.SetActive(true);
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
            case PlayerState.OnTakeDamage:
                deltaX = 0f;
                break;
            case PlayerState.OnCasting:
                deltaX = 0f;
                break;
            case PlayerState.OnDead:
                deltaX = 0f;
                break;
        }

        Move(deltaX);
        jumpImpactSoud();
    }

    void Move(float speed)
    {
        body2D.velocity = new Vector2(speed, body2D.velocity.y);
            
        if (speed > 0 && !facingRight)
            Flip();
            
        else if (speed < 0 && facingRight)
            Flip();

        bool falling = !grounded;
        anim.SetFloat("Speed", speed);
        anim.SetBool("Grounded", grounded);
        anim.SetBool("Falling", falling);
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            anim.SetTrigger("Salto");
            body2D.velocity = Vector2.up * stats.GetStat("jumpForce");
            extraJumps = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && (extraJumps > 0) && !grounded)
        {
            anim.SetTrigger("Salto");
            body2D.velocity = Vector2.up * stats.GetStat("extraJumpForce");
            extraJumps--;
        }
    }

    void jumpImpactSoud()
    {
        if (grounded && !alreadyPlayed)
        {
            source.PlayOneShot(groundContact, Volume);
            alreadyPlayed = true;
        }
        if (!grounded)
        {
            alreadyPlayed = false;
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector2 tempScale = body2D.transform.localScale;
        tempScale.x *= -1;
        body2D.transform.localScale = tempScale;
    }

    public void JumpForTactil()
    {
        if (grounded)
        {
            anim.SetTrigger("Salto");
            body2D.velocity = Vector2.up * stats.GetStat("jumpForce");
            extraJumps = 1;
        }
        else if ((extraJumps > 0) && !grounded)
        {
            anim.SetTrigger("Salto");
            body2D.velocity = Vector2.up * stats.GetStat("extraJumpForce");
            extraJumps--;
        }
    }

    public IEnumerator OnTakeDamage()
    {
        State = PlayerState.OnTakeDamage;
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

    public IEnumerator OnHit()
    {
        State = PlayerState.OnTakeDamage;
        float knockback = (body2D.transform.localScale.x > 0) ? -1f : 1f;
        body2D.AddForce(new Vector2(1300 * knockback, 0f));
        yield return new WaitForSeconds(0.31f);
        State = PlayerState.OnControll;
    }

    public IEnumerator OnCasting(float timeToControl)
    { 
        State = PlayerState.OnCasting;
        yield return new WaitForSeconds(timeToControl);
        State = PlayerState.OnControll;
    }

    public IEnumerator OnDead(float time)
    {
        State = PlayerState.OnDead;
        yield return new WaitForSeconds(time);
        State = PlayerState.OnControll;
    }

    public enum PlayerState
    {
        OnControll,
        OnTakeDamage,
        OnInteraction,
        OnCasting,
        OnDead,
    }

    public enum InputType
    {
        Teclado,
        Joystick,
    }
}