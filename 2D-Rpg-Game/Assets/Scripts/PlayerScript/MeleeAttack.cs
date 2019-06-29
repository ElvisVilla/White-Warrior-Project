using UnityEngine;
using DG.Tweening;
using Bissash.Util;

[CreateAssetMenu(menuName = "Abilities/Melee/Basic Attack")]
public class MeleeAttack : Ability
{
    protected Collider2D coll;
    protected Vector2 weaponProjection;

    public override void Init(Player player)
    {
        m_timer = new Timer(CoolDownSeconds);
        _abilityType = AbilityType.Melee;
        weaponProjection = new Vector2(Range, 1f);
        RuneController = FindObjectOfType<RuneController>();
    }

    public override void UpdateAction(Player player, Transform weapon)
    {
        m_timer.RunTimer();
        coll = Physics2D.OverlapBox(weapon.position, weaponProjection, 0f, _whatIsEnemy);
    }

    public override void Action(Player player)
    {
        if (m_timer.ElapsedSeconds() >= CoolDownSeconds)
        {
            player.Anim.CrossFade(AnimationName, 0f);
            m_timer.ResetTimer();
            player.Stats.Speed = _minValueSpeed;
            IsCoolDown = true;
            DOTween.To(() => player.Stats.Speed, x => player.Stats.Speed = x, player.Stats.MaxSpeed, _timeToTween);
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
                int randomDamage = Random.Range(_minEffect, _maxEffect);
                health.TakeDamage(randomDamage);
                player.StartCoroutine(player.Motor.OnHit());
                player.StartCoroutine(player.CameraManager.OnAbilityEffect(_abilityType));
                //player.ParticlesController.HitInstantiate(1f, player.Combat.weapon.localPosition);
                RuneController.ChargeRune();
            }
        }
    }
}
