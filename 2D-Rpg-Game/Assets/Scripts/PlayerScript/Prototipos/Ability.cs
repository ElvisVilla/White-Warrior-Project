using UnityEngine;
using System;

public enum AbilityMode
{
    Melee,
    Range,
    Magic,
    Buff,
    AOEMelee,
    AOEMagic,
    AOEHeal,
    AOERange,
}

public abstract class Ability : ScriptableObject
{
    [SerializeField] protected Sprite _icon;
    [SerializeField] protected LayerMask _whatIsEnemy;
    [SerializeField] protected float _effect;
    [SerializeField] protected float _energyConsumtion;
    [SerializeField] protected float _range;
    [SerializeField] protected float _coldDown;
    [SerializeField] protected float _minValueSpeed;
    [SerializeField] protected float _timeToTween;
    [SerializeField] protected string _animationName;
    protected AbilityMode _abilityMode;

    public Sprite Icon => _icon;
    public LayerMask WhatIsEnemy => _whatIsEnemy;
    public AbilityMode AbilityMode => _abilityMode;
    public float Effect => _effect;
    public float EnergyComsumption => _energyConsumtion;
    public string AnimationName => _animationName;
    public float Range { get { return _range; } set { _range = value; } }
    public float ColdDown { get { return _coldDown; } set { _coldDown = value; } }
    public bool IsOnCoolDown { get; protected set; }

    public abstract void Init(Player plater);
    public abstract void UpdateAction(Player player, Transform wapon);
    public abstract void Action(Player player);
}
