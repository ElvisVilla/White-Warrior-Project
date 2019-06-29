using UnityEngine;
using System;
using Bissash.Util;

public enum AbilityType
{
    Melee,
    Range,
    Magic,
    Buff,
}

public abstract class Ability : ScriptableObject
{
    [SerializeField] protected Sprite _icon;
    [SerializeField] protected LayerMask _whatIsEnemy;
    [SerializeField] protected Color _natureColor;
    [SerializeField] protected int _minEffect;
    [SerializeField] protected int _maxEffect;
    [SerializeField] protected int _runeCost;
    [SerializeField] protected float _range;
    [SerializeField] protected float _coldDown;
    [SerializeField] protected float _minValueSpeed;
    [SerializeField] protected float _timeToTween;
    [SerializeField] protected string _animationName;
    protected AbilityType _abilityType;
    protected RuneController _runeController;
    protected Timer m_timer;

    public Sprite Icon => _icon;
    public LayerMask WhatIsEnemy => _whatIsEnemy;
    public Color NatureColor => _natureColor;
    public AbilityType AbilityType => _abilityType;
    public int Effect { get; set; }
    public int RuneCost => _runeCost;
    public string AnimationName => _animationName;
    public float Range { get { return _range; } set { _range = value; } }
    public float CoolDownSeconds { get { return _coldDown; } set { _coldDown = value; } }
    public bool IsCoolDown { get; protected set; }
    public RuneController RuneController { get; protected set; }

    public abstract void Init(Player player);
    public abstract void UpdateAction(Player player, Transform wapon);
    public abstract void Action(Player player);
    public abstract void OnLogicAttack(Player player);
}
