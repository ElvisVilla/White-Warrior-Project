using Bissash.Util;
using DG.Tweening;
using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Heal")]
public class Heal : Ability
{
    [SerializeField] protected float timeToMove;
    Vector3 actualPosition;

    public override void Init(Player player)
    {
        m_timer = new Timer(CoolDownSeconds);
        _abilityType = AbilityType.Magic;
        RuneController = FindObjectOfType<RuneController>();
    }

    public override void UpdateAction(Player player, Transform wapon)
    {
        m_timer.RunTimer();
    }

    public override void Action(Player player)
    {
        if (m_timer.ElapsedSeconds() >= CoolDownSeconds && RuneController.GotRunes(RuneCost))
        {
            player.Anim.CrossFade(AnimationName, 0f);
            OnLogicAttack(player);
            IsCoolDown = true;
            player.StartCoroutine(player.CameraManager.OnAbilityEffect(_abilityType));
            m_timer.ResetTimer();
            RuneController.UseRunes(RuneCost);
        }
        else
            IsCoolDown = false;    
    }

    public override void OnLogicAttack(Player player)
    {
        //al aplicar la curacion evitaremos que el jugador se mueva durante unos segundos.
        player.StartCoroutine(player.Motor.OnNonControl(0.25f));
        player.ParticlesController.HealInstantiate(4.5f);
        Effect = Random.Range(_minEffect, _maxEffect);
        player.Health.TakeHeal(Effect);
    }
}
