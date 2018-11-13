using UnityEngine;
using DG.Tweening;

[CreateAssetMenu(menuName = "Abilities/Melee")]
public class MeleeAttack : Ability
{
    float timer;
    Collider2D coll;

    public override void Init(Player player)
    {
        timer = ColdDown;
        _abilityMode = AbilityMode.Melee;
    }

    public override void UpdateAction(Player player, Transform weapon)
    {
        timer += Time.deltaTime;
        coll = Physics2D.OverlapCircle(weapon.position, Range, _whatIsEnemy);
    }

    public override void Action(Player player)
    {
        if (timer >= ColdDown)
        {
            player.Anim.CrossFade(AnimationName, 0f);
            OnLogicAttack(player);
            timer = 0f;
            player.Stats.Speed = _minValueSpeed;
            IsOnCoolDown = true;
            DOTween.To(() => player.Stats.Speed, x => player.Stats.Speed = x, player.Stats.MaxSpeed, _timeToTween);
        }
        else
            IsOnCoolDown = false;
    }

    private void OnLogicAttack(Player player)
    {
        if (coll != null)
        {
            EnemyHealth health = coll.transform.GetComponent<EnemyHealth>();
            if (health != null)
            {
                float damage = Effect;
                health.TakeDamage(damage);
                player.StartCoroutine(player.Motor.OnHit(0.1f));
            }
        }
    }
}
