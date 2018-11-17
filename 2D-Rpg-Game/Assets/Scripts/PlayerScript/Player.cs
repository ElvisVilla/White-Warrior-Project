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
    public bool Interaction;

    public Movement Motor => motor;
    public CharacterStats Stats => stats;
    public CombatActions Combat => combat;
    public Animator Anim { get; private set; }
    public Rigidbody2D Body2D { get; private set; }

    private void Awake()
    {
        Anim = GetComponentInChildren<Animator>();
        Body2D = GetComponent<Rigidbody2D>();
        
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
    public void AnimationLogicEventForMeleeAttacks()
    {
        Ability ability = combat.GetAbilities(3);
        ability.OnLogicAttack(this);
    }

    //Problemas de logica que iran adentro del scriptableObject.
    //1- El problema seria tener que programar la logica de cada animation event, pero de ello ya lo podemos hacer en el scriptableObject
    //2- Podemos agregar un enumerado que defina el tipo de ataque y esto nos permite definir la logica de si es daño en AOE con tiempo o solo fisico y de un solo hit.

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireCube(combat.weapon.position, new Vector2(combat.GetAbilities(0).Range, 1f));
    }
}
