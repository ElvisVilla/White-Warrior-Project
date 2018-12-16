using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AudioSource))]
public class Player : MonoBehaviour
{
    [SerializeField] private Movement motor;
    [Header("")][SerializeField] private CharacterStats stats;
    [Header("")][SerializeField] private CombatActions combat;

    public Movement Motor => motor;
    public CharacterStats Stats => stats;
    public CombatActions Combat => combat;
    public PlayerHealth Health { get; private set; }
    public Animator Anim { get; private set; }
    public AudioSource Source { get; private set; }
    public Rigidbody2D Body2D { get; private set; }
    public CameraManager CameraManager { get; private set; }
    public ParticlesController ParticlesController { get; private set; }

    Ability ability;

    private void Awake()
    {
        Health = GetComponent<PlayerHealth>();
        Anim = GetComponent<Animator>();
        Body2D = GetComponent<Rigidbody2D>();
        Source = GetComponent<AudioSource>();
        CameraManager = FindObjectOfType<CameraManager>();
        ParticlesController = GetComponent<ParticlesController>();

        if (stats != null)
        {
            motor.Init(this);
            combat.Init(this);
        }
    }

    private void Update()
    {
        //Actualizar los componentes
        motor.MovementUpdate(this);
        combat.CombatActionsUpdate(this);
    }

    public void PerformJump()
    {
        if(!Health.IsDead)
            motor.JactileJump();
        ParticlesController.DustLandInstantiate(1f);
    }

    //Aca llamaremos la logica de la animacion de ataques fisicos.
    public void AnimationLogicEvent()
    {
        ability.OnLogicAttack(this);
    }

    public void SetAbilityToEvent(Ability ab)
    {
        ability = ab;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireCube(combat.weapon.position, new Vector2(1f, 1f));
    }
}
