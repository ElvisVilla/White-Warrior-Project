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

        if (effects.ParticleEffect != null)
        {
            var particlesParent = player.transform.GetChildByName("Particles Effects");
            var ground = player.transform.GetChildByName("Ground Checker");
            particleInstance = Instantiate(effects.ParticleEffect, ground.position + effects.offset,
                effects.ParticleRotation, particlesParent);
            particleEmiter = particleInstance.GetComponent<ParticleEmiter>();
        }
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
            particleEmiter.Play();
        }
    }

    public override void OnCollisionLogic(Player player)
    {
        player.Movement.NonAllowedToMove(player, seconds: 0.25f);
        Effect = UnityEngine.Random.Range(_minEffect, _maxEffect);
        player.Health.TakeHeal(Effect, Vector2.zero);
        //OnCollisionLogicEvent.Raise(this);
    }

    public override void Remove()
    {
        
    }
}
