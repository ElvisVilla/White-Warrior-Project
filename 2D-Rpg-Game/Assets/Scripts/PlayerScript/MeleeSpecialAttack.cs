using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Abilities/Melee/Special Attack")]
public class MeleeSpecialAttack: MeleeAttack {

    public override void Init(Player player)
    {
        base.Init(player);
    }

    public override void UpdateAction(Player player, Transform weapon)
    {
        base.UpdateAction(player, weapon);
    }

    public override void Action(Player player)
    {
        //base.Action(player);
        if (_timer >= CoolDownSeconds && RuneController.GotRunes(RuneCost)) //Los ataques especiales requieren comprobar la cantidad de runas.
        {
            player.Anim.CrossFade(AnimationName, 0f);
            _timer = 0f;
            player.Stats.Speed = _minValueSpeed;
            IsCoolDown = true;
            DOTween.To(() => player.Stats.Speed, x => player.Stats.Speed = x, player.Stats.MaxSpeed, _timeToTween);
            RuneController.UseRunes(RuneCost);
        }
        else
            IsCoolDown = false;
    }

    public override void OnLogicAttack(Player player)
    {
        if (coll != null)
        {
            EnemyHealth health = coll.transform.GetComponent<EnemyHealth>();
            if (health != null && health.CurrentHealth >= 1f)
            {
                Effect = (int)Random.Range(_minEffect, _maxEffect);
                health.TakeDamage(Effect);
                player.StartCoroutine(player.Motor.OnHit());
                player.StartCoroutine(player.CameraManager.OnAbilityEffect(_abilityType));
            }
        }
    }
}
