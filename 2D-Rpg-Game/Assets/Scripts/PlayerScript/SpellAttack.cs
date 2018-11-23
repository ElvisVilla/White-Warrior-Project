using DG.Tweening;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Spell")]
public class SpellAttack : Ability
{
    [SerializeField] protected float timeToCast;
    [SerializeField] protected float timeToMove;
    [SerializeField] protected float timerCast;
    bool abilityPress = false;

    public override void Init(Player plater)
    {
        _timer = ColdDown;
        _abilityMode = AbilityMode.Magic;
    }

    public override void UpdateAction(Player player, Transform wapon)
    {
        _timer += Time.deltaTime;
        if (abilityPress)
        {
            while (timerCast <= timeToCast)
            {
                timerCast += Time.deltaTime;
            }
        }
    }

    public override void Action(Player player)
    {
        if (_timer >= ColdDown)
        {
            player.Anim.CrossFade(AnimationName, 0f);
            _timer = 0f;
            abilityPress = true;
            OnLogicAttack(player);
            player.Stats.Speed = _minValueSpeed;
            IsOnCoolDown = true;
            DOTween.To(() => player.Stats.Speed, x => player.Stats.Speed = x, player.Stats.MaxSpeed, _timeToTween);
        }
        else
            IsOnCoolDown = false;
    }

    public override void OnLogicAttack(Player player)
    {
        player.StartCoroutine(player.Motor.OnCasting(timeToMove));
        if (timerCast >= timeToCast)
        {
            PlayerHealth health = player.gameObject.GetComponent<PlayerHealth>();
            health.TakeHeal(Effect);
            timerCast = 0f;
            abilityPress = false;
        }
    }
}
