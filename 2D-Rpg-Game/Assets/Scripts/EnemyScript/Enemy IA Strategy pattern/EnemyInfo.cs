using UnityEngine;
using System;

[Serializable]
public class EnemyInfo
{
    //Pendiente: Definir Interfases.

    [Header("Set In Inspector")]
    public LayerMask necesaryLayers;
    public float patrolRange;
    public float stopingDistance;
    public GameObject playerReference;

    [Header("Enemy speed")]
    public float speed;
    public float patrolSpeed;
    public float combatSpeed;
    public float chaseSpeed;

    [HideInInspector] public bool facingRight = true;
    [HideInInspector] public bool shouldStop;

    [Header("Enemy Sight")]
    public LayerMask WhatIsPlayer;
    public Transform SightOrigin;
    public float SightDistance;

    [Header("Ground Detection")]
    public LayerMask whatIsGround;
    public Transform groundDetector;
    public float rayDistance;
}
