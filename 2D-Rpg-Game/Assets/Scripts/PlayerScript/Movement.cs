using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;

[Serializable]
public class Movement
{
    #region Variables
    [Header("Establecer en Inspector")]
    public InputType inputMode;
    Joystick joystick;
    private int extraJumps;
    private bool facingRight = true;
    public PlayerState State { get; set; }

    [Header("Checar collision")]
    [SerializeField]private Transform groundChecker;
    [SerializeField]private LayerMask whatIsGround;
    private const float radius = 0.02f;
    public bool grounded = false;

    [Header("Sound")]
    [SerializeField] private AudioClip groundContact;
    [Range(0f, 1f)] [SerializeField] private float volume;
    bool alreadyPlayed = false;

    AudioSource source;
    Rigidbody2D body2D;
    Animator anim;
    CharacterStats stats;
    PlayerHealth health;
    #endregion

    public void Init(Player player)
    {
        body2D = player.Body2D;
        anim = player.Anim;
        stats = player.Stats;
        source = player.Source;
        health = player.Health;
        joystick = GameObject.FindObjectOfType<Joystick>();
        player.Source.volume = volume;
        alreadyPlayed = false;
    }

    public void MovementUpdate(Player player)
    {
        grounded = Physics2D.OverlapCircle(groundChecker.position, radius, whatIsGround);
        float deltaX = 0f;
        deltaX = MovementState(player, deltaX);
        Move(deltaX, player);
        GroundedEffects(player);
    }

    private float MovementState(Player player, float deltaX)
    {
        switch (State)
        {
            case PlayerState.Controlable:
                Jump(player); //Si es controlable puede saltar.
                switch (inputMode)
                {
                    case InputType.Teclado:
                        joystick.gameObject.SetActive(false);
                        deltaX = Input.GetAxisRaw("Horizontal") * stats.Speed;
                        break;

                    case InputType.Joystick:
                        joystick.gameObject.SetActive(true);
                        if (joystick.Horizontal >= 0.1f)
                        {
                            deltaX = 1 * stats.Speed;
                        }
                        else if (joystick.Horizontal <= -0.1f)
                        {
                            deltaX = -1f * stats.Speed;
                        }
                        break;
                }
                break;
            case PlayerState.NonControlable:
                anim.SetFloat("Speed", 0f);
                break;
        }

        return deltaX;
    }

    void Move(float speed, Player player)
    {
        body2D.velocity = new Vector2(speed, body2D.velocity.y);
        player.StartCoroutine(player.ParticlesController.DustRunParticle(0.24f, speed, grounded, player));

        if (speed > 0 && !facingRight)
            Flip();

        else if (speed < 0 && facingRight)
            Flip();

        bool falling = !grounded;
        anim.SetFloat("Speed", speed);
        anim.SetBool("Grounded", grounded);
        anim.SetBool("Falling", falling);
    }
    
    void Jump(Player player)
    {
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            anim.SetTrigger("Salto");
            body2D.velocity = Vector2.up * stats.GetStat("jumpForce");
            extraJumps = 1;
            player.ParticlesController.DustLandInstantiate(1);
        }
        else if (Input.GetKeyDown(KeyCode.Space) && (extraJumps > 0) && !grounded)
        {
            anim.SetTrigger("Salto");
            body2D.velocity = Vector2.up * stats.GetStat("extraJumpForce");
            extraJumps--;
            player.ParticlesController.DustLandInstantiate(1);
        }
    }

    void GroundedEffects(Player player)
    {
        if (grounded && !alreadyPlayed)
        {
            source.PlayOneShot(groundContact, volume);
            player.ParticlesController.DustLandInstantiate(1f);
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

    public void JactileJump()
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

    public void OnInteraction(bool isInteract)
    {
        if (isInteract)
            State = PlayerState.NonControlable;
        else
            State = PlayerState.Controlable;
    }

    public IEnumerator OnHit()
    {
        State = PlayerState.NonControlable;
        float knockback = (body2D.transform.localScale.x > 0) ? -1f : 1f;
        body2D.DOMoveX(body2D.position.x + 0.5f * knockback, 0.15f, false);
        yield return new WaitForSeconds(0.28f);
        if (!health.IsDead)
        {
            State = PlayerState.Controlable;
        }
        else
            State = PlayerState.NonControlable;
    }

    public IEnumerator OnNonControl(float seconds)
    {
        State = PlayerState.NonControlable;
        yield return new WaitForSeconds(seconds);
        State = PlayerState.Controlable;
    }

    public enum PlayerState
    {
        Controlable,
        NonControlable,
    }

    public enum InputType
    {
        Teclado,
        Joystick,
    }
}