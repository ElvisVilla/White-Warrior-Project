using UnityEngine;
using DG.Tweening;

[CreateAssetMenu(menuName = "Abilities/Melee")]
public class MeleeAttack : Ability
{
    Collider2D coll;
    Vector2 weaponProjection;

    public override void Init(Player player)
    {
        _timer = ColdDown;
        _abilityMode = AbilityMode.Melee;
        weaponProjection = new Vector2(Range, 1f);
    }

    public override void UpdateAction(Player player, Transform weapon)
    {
        _timer += Time.deltaTime;
        coll = Physics2D.OverlapBox(weapon.position, weaponProjection, 0f, _whatIsEnemy);
    }

    public override void Action(Player player)
    {
        //Comprobamos el cooldown, ejecutamos la animacion sin importar el efecto, la idea es aplicar el timer, resetear y establecer cooldown.
        if (_timer >= ColdDown)
        {
            player.Anim.CrossFade(AnimationName, 0f);
            //OnLogicAttack(player);
            _timer = 0f;
            player.Stats.Speed = _minValueSpeed;
            IsOnCoolDown = true;
            DOTween.To(() => player.Stats.Speed, x => player.Stats.Speed = x, player.Stats.MaxSpeed, _timeToTween);
        }
        else
            IsOnCoolDown = false;
    }

    public override void OnLogicAttack(Player player)
    {
        if (coll != null)
        {
            EnemyHealth health = coll.transform.GetComponent<EnemyHealth>();
            if (health != null && health.CurrentHealth >= 1f)
            {
                float damage = Effect;
                health.TakeDamage(damage);
                player.StartCoroutine(player.Motor.OnHit());
            }
        }
    }
}
