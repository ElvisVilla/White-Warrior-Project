using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;
using Bissash.IA;
using UnityEngine.InputSystem;

public enum MovementState
{
    Controllable,
    NonControllable,
    AbilityBehaviour,
}

[Serializable]
public class Movement : MonoBehaviour, PlayerControls.IMovementMapActions
{
    #region Variables

    //Movement Variables.
    private Vector2 direction = Vector2.zero;
    private MovementState movementState;
    public FacingSide FacingSide { get; set; }

    [Header("Physics resources")]
    [SerializeField] private Transform groundChecker = null;
    [SerializeField] private LayerMask groundLayer = new LayerMask();
    [SerializeField] private float fallMultiplier = 2.1f;
    private const float radius = 0.02f;
    bool grounded = false;
    private int extraJumps = 0;
    public float speed = 0f;

    [Header("Set resources")]
    [SerializeField] private ParticleEmiter LandedEffect = null;
    [SerializeField] private ParticleEmiter RunningEffect = null;
    [SerializeField] private AudioClip groundContactClip = null;
    [Range(0f, 1f)] [SerializeField] private float volume = 0f;

    //Components
    AudioSource source;
    Rigidbody2D body2D;
    Animator anim;
    CharacterStats stats;
    PlayerHealth health;
    SpriteRenderer renderer2D;

    Player player;
    #endregion

    private void Awake()
    {
        player = GetComponent<Player>();
        stats = player.Stats;

        body2D = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        source = GetComponent<AudioSource>();
        health = GetComponent<PlayerHealth>();
        renderer2D = GetComponentInChildren<SpriteRenderer>();

       source.volume = volume;
    }

    private void Update()
    {
        Move();
        GroundedParticlesEffects();
    }

    void Move()
    {
        //Falta el MovementState.AbilityBehaviour
        //El movimiento arbitrario del jugador.
        if (movementState == MovementState.Controllable)
            speed = direction.x * stats.Speed;

        //Movement.NonControllable, no habra movimiento asi se precionen los inputs.
        //Movement.AbilityBehaviour no permitira movimiento y sera guiado por el comportamiento de la habilidad.
        else if (movementState == MovementState.NonControllable || movementState == MovementState.AbilityBehaviour)
            speed = 0f;

        OrientationSprite(speed);
        body2D.velocity = new Vector2(speed, body2D.velocity.y);

        anim.SetFloat("Speed", speed);
        anim.SetBool("Grounded", grounded);
        anim.SetBool("Falling", !grounded);
    }
    
    //Al trabajar la rotacion con el sprite evitamos que la direccion del ataque sea afectado con la rotacion del personaje.
    //Esto nos da libertad de dirigir el ataque respecto a la direccion que el jugador tenia al momento de presionar la habilidad.
    void OrientationSprite(float speed)
    {
        if (speed > 0f)
        {
            FacingSide = FacingSide.Right;
            renderer2D.flipX = false;
        }
        else if (speed < 0f)
        {
            FacingSide = FacingSide.Left;
            renderer2D.flipX = true;
        }
        else
        {
            if(FacingSide == FacingSide.Left)
            {
                renderer2D.flipX = true;
            }
            else if(FacingSide == FacingSide.Right)
            {
                renderer2D.flipX = false;
            }
        }
    }

    public void Jump()
    {
        if(movementState == MovementState.Controllable)
        {
            if (grounded)
            {
                anim.SetTrigger("Salto");
                body2D.velocity = Vector2.up * stats.GetStat("jumpForce");
                extraJumps = 1;
                LandedEffect.Play();
            }
            else if ((extraJumps > 0) && !grounded)
            {
                anim.SetTrigger("Salto");
                body2D.velocity = Vector2.up * stats.GetStat("extraJumpForce");
                extraJumps--;
                LandedEffect.Play();
            }
        }
    }

    private void FixedUpdate()
    {
        //Better Jump Code.
        void BetterJump()
        {
            if (body2D.velocity.y < 0f)
            {
                body2D.gravityScale = fallMultiplier;
            }
            else if (body2D.velocity.y < 0 && !grounded)
            {
                body2D.gravityScale = 1.5f;
            }
            else
            {
                body2D.gravityScale = 1.1f;
            }
        }

        BetterJump();
        grounded = Physics2D.OverlapCircle(groundChecker.position, radius, groundLayer);
    }

    void GroundedParticlesEffects()
    {
        if (grounded  && body2D.velocity != Vector2.zero)
        {
            RunningEffect.Play();
        }
        else
        {
            RunningEffect.Stop();
        }
    }

    public void KnockBackDamage(Player player)
    {
        IEnumerator KnockBack()
        {
            movementState = MovementState.NonControllable;
            float knockback = (!renderer2D.flipX) ? -0.5f : 0.5f;
            body2D.DOMoveX(body2D.position.x + knockback, duration: 0.15f, snapping: false);

            yield return new WaitForSeconds(0.28f);

            if (!health.IsDead)
                movementState = MovementState.Controllable;
            else
                movementState = MovementState.NonControllable;
        }

        player.StartCoroutine(KnockBack());
    }
    
    public void NonAllowedToMove(Player player, float seconds)
    {
        IEnumerator OnNonControl(float sec)
        {
            movementState = MovementState.NonControllable;
            yield return new WaitForSeconds(sec);
            movementState = MovementState.Controllable;
        }

        player.StartCoroutine(OnNonControl(seconds));
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<Vector2>();     
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        Jump();
    }
}