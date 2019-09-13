using Bissash;
using DG.Tweening;
using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Heal")]
public class Heal : Ability
{
    private GameObject particleInstance;
    private ParticleEmiter particleEmiter;

    public override void Init(Player player, RuneController runeController)
    {
        base.Init(player, runeController);
        effects.Init(player);
    }

    public override void AbilityUpdate(Player player)
    {
        base.AbilityUpdate(player);
    }

    public override void OnAbilityPressed(Player player, Action action)
    {
        if (m_timer.TimeHasComplete())
        {
            OnCollisionLogic(player);

            //OnAbilityPressedEvent.Raise(this);
            player.CameraManager.OnAbilityCameraEffect(this);
            player.Anim.PerformCrossFade(AnimationName, 0f);
            m_timer.ResetTimer();
            action?.Invoke();
            effects.PlayParticles();
        }
    }

    public override void OnCollisionLogic(Player player)
    {
        player.Movement.NonAllowedToMove(seconds: 0.25f);
        Effect = UnityEngine.Random.Range(_minEffect, _maxEffect);
        player.Health.TakeHeal(Effect, Vector2.zero);
    }

    public override void Remove()
    {
        effects.CleanParcilesOnRemove();
    }
}
