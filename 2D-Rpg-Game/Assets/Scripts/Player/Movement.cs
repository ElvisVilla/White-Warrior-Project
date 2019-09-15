using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;
using Bissash;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour, PlayerControls.IMovementMapActions
{
    #region Variables
    float moveSpeed = 0f;
    private Vector2 direction = Vector2.zero;
    public MovementState movementState;
    private FacingSide facingSide;
    public FacingSide FacingSide { get { return facingSide; } set { facingSide = value; } }

    [Header("Physics resources")]
    [SerializeField] private Transform groundChecker = null;
    [SerializeField] private LayerMask groundLayer = new LayerMask();
    [SerializeField] private float fallMultiplier = 2.1f;
    bool grounded;
    private int extraJumps = 0;

    [Header("Set resources")]
    [SerializeField] private ParticleEmiter LandedEffect = null;
    [SerializeField] private ParticleEmiter RunningEffect = null;
    [SerializeField] private AudioClip groundContactClip = null;
    [Range(0f, 1f)] [SerializeField] private float volume = 0f;
    private bool groundedSoundAlreadyPlayed = false;

    //Components
    AudioSource source;
    Rigidbody2D body2D;
    Animator anim;
    CharacterStats stats;
    PlayerHealth health;
    [HideInInspector] public SpriteRenderer renderer2D;
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
        GroundedEffects();
    }

    void Move()
    {
        //El movimiento arbitrario del jugador.
        if (movementState == MovementState.Controllable)
        {
            moveSpeed = direction.x * stats.Speed;
        }

        //Movement.NonControllable, no habra movimiento asi se precionen los inputs.
        //Movement.AbilityBehaviour no permitira movimiento y sera guiado por el comportamiento de la habilidad.
        else if (movementState == MovementState.NonControllable)
            moveSpeed = 0f;

        renderer2D.OrientationSprite(moveSpeed, ref facingSide);
        body2D.velocity = new Vector2(moveSpeed, body2D.velocity.y);

        anim.PerformAnimation("Speed", moveSpeed);
        anim.PerformAnimation("Grounded", grounded);
        anim.PerformAnimation("Falling", !grounded);

    }

    public void Jump()
    {
        if (movementState == MovementState.Controllable)
        {
            if (grounded)
            {
                anim.PerformTriggerAnimation("Salto");
                body2D.velocity = Vector2.up * stats.GetStat("jumpForce");
                extraJumps = 1;
                LandedEffect.PlayOnce();
            }
            else if ((extraJumps > 0) && !grounded)
            {
                anim.PerformTriggerAnimation("Salto");
                body2D.velocity = Vector2.up * stats.GetStat("extraJumpForce");
                extraJumps--;
                LandedEffect.PlayOnce();
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

        grounded = Physics2D.Raycast(groundChecker.position, Vector2.down, 0.2f, groundLayer);

    }

    void GroundedEffects()
    {
        if (grounded && body2D.velocity != Vector2.zero)
        {
            RunningEffect.Play();
        }
        else
        {
            RunningEffect.Stop();
        }

        if (grounded && groundedSoundAlreadyPlayed == false)
        {
            source.PlayOneShot(groundContactClip);
            groundedSoundAlreadyPlayed = true;

        }
        else if (grounded == false)
            groundedSoundAlreadyPlayed = false;
    }

    public void KnockBackBehaviour()
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

        StartCoroutine(KnockBack());
    }

    public void NonAllowedToMove(float seconds)
    {
        IEnumerator NonControl(float sec)
        {
            movementState = MovementState.NonControllable;
            yield return new WaitForSeconds(sec);
            movementState = MovementState.Controllable;
        }

        StartCoroutine(NonControl(seconds));
    }

    public void PerformInvulnerable()
    {
        IEnumerator Invulnerable()
        {
            movementState = MovementState.PerformingAbility;
            yield return new WaitForSeconds(2f);
            movementState = MovementState.Controllable;
        }

        StartCoroutine(Invulnerable());
    }
    

    #region New Input System
    public void OnMovement(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        Jump();
    }
    #endregion

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(groundChecker.position, Vector3.down * 0.2f);
    }
}