using UnityEngine;
using System;
using Bissash;

public enum AbilityType
{
    Undefined,
    Melee,
    AreaMelee,
    Magic,
}

//Crear una clase normal que me permita hacer un custom editor que sirva para habilitar las opciones random de las habilidades.
public abstract class Ability : ScriptableObject
{
    [SerializeField] protected Sprite _icon;
    [SerializeField] protected LayerMask _whatIsEnemy;
    [SerializeField] protected int _minEffect;
    [SerializeField] protected int _maxEffect;
    [SerializeField] protected int _runeCost;
    [SerializeField] protected float _range;
    [SerializeField] protected float _coldDown;
    [SerializeField] protected float _minValueSpeed;
    [SerializeField] protected float _timeToTween;
    [SerializeField] protected string _animationName;
    [SerializeField] protected AbilityType _abilityType;
    protected Timer m_timer;
    public Effects effects;

    public Sprite Icon => _icon;
    public LayerMask WhatIsEnemy => _whatIsEnemy;
    public AbilityType AbilityType => _abilityType;
    public int Effect { get; protected set; }
    public int RuneCost => _runeCost;
    public string AnimationName => _animationName;
    public float Range { get { return _range; } set { _range = value; } }
    public float CoolDownSeconds { get { return _coldDown; } set { _coldDown = value; } }
    public bool IsCoolDown { get; protected set; }
    public RuneController RuneController { get; protected set; }

    public virtual void Init(Player player, RuneController runeController)
    {
        m_timer = new Timer(CoolDownSeconds, CoolDownSeconds);
        RuneController = runeController;
    }

    public virtual void AbilityUpdate(Player player) => m_timer.RunTimer();


    public abstract void OnAbilityPressed(Player player, Action action);
    public abstract void OnCollisionLogic(Player player);

    public abstract void Remove();
}

[Serializable]
public struct Effects
{
    public GameObject ParticleEffect;
    public AudioClip SoundEffect;
    public Vector3 offset;

    public Quaternion ParticleRotation => ParticleEffect.transform.rotation;
}
