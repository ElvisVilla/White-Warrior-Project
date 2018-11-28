using UnityEngine;
using Cinemachine;

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

    CinemachineVirtualCamera CinemachineVirtualCam;
    CinemachineBasicMultiChannelPerlin virtualCamNoise;
    Ability ability;
    float timer;

    private void Awake()
    {
        Health = GetComponent<PlayerHealth>();
        Anim = GetComponent<Animator>();
        Body2D = GetComponent<Rigidbody2D>();
        Source = GetComponent<AudioSource>();

        CinemachineVirtualCam = GameObject.FindGameObjectWithTag("PlayerCam").GetComponent<CinemachineVirtualCamera>();
        virtualCamNoise = CinemachineVirtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        timer = 1f;
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

        timer += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Q))
        {
            timer = 0;
        }

        if (timer < 0.8f)
        {
            virtualCamNoise.m_AmplitudeGain = 1.2f;
            virtualCamNoise.m_FrequencyGain = 2f;
        }
        else
        {
            virtualCamNoise.m_AmplitudeGain = 0f;
        }
    }

    public void PerformJump()
    {
        if(!Health.IsDead)
            motor.JumpForTactil();
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
