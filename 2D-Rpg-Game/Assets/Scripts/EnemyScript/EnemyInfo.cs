using UnityEngine;
using System;

[Serializable]
public class EnemyInfo
{
    //Pendiente: Definir Interfases.
    //Valores por defecto.

    [Header("Set In Inspector")]
    public float patrolRange = 4f;
    public float combatRange = 5f;
    public float chaseRange = 7f;
    public float stopingDistance = 1.2f;
    public GameObject reference;

    [Header("Enemy speed")]
    public float patrolSpeed = 1.2f;
    public float combatSpeed = 2.8f;
    public float chaseSpeed = 3.4f;

    [Header("Enemy Sight")]
    public LayerMask WhatIsPlayer;
    public Transform SightOrigin;
    public float SightDistance = 2f;

    [Header("Ground Detection")]
    public LayerMask whatIsGround;
    public Transform groundDetector;
    public float rayDistance = 2f;

    public bool shouldStop { get; set; }
}
