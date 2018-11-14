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
        motor.MovementUpdate();
        combat.CombatActionsUpdate(this);
    }

}
