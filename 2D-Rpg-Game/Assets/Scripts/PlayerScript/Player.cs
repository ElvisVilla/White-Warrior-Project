using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Campos de Player")]
    [Header("Movimiento del jugador")]
    [SerializeField] private Movement motor;
    [Header("Stadisticas del jugador")]
    [SerializeField] private CharacterStats stats;
    [Header("Habilidades del jugador")]
    [SerializeField] private CombatActions combat;

    public Movement Motor => motor;
    public CharacterStats Stats => stats;
    public CombatActions Combat => combat;
    public Animator Anim { get; private set; }
    public AudioSource Source { get; private set; }
    public Rigidbody2D Body2D { get; private set; }

    private void Awake()
    {
        Anim = GetComponent<Animator>();
        Body2D = GetComponent<Rigidbody2D>();
        Source = GetComponent<AudioSource>();


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
        //motor.OnInteraction(Interaction);
    }

    public void PerformJump()
    {
        motor.JumpForTactil();
    }

    //Aca llamaremos la logica de la animacion de ataques fisicos.
    public void AnimationLogicBasicAttack()
    {
        Ability ability = combat.GetAbilities(3);
        ability.OnLogicAttack(this);
    }

    public void AnimationLogicSecondAttack()
    {
        Ability ability = combat.GetAbilities(2);
        ability.OnLogicAttack(this);
    }
    public void AnimationLogicThirdAttack()
    {
        Ability ability = combat.GetAbilities(1);
        ability.OnLogicAttack(this);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireCube(combat.weapon.position, new Vector2(1f, 1f));
    }
}
