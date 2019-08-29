using UnityEngine;
using DG.Tweening;
using Bissash.Util;
using Bissash.IA;
using System;

[CreateAssetMenu(menuName = "Abilities/Melee")]
public class MeleeAttack : Ability
{
    //Physics variables
    protected Vector2 weaponProjection;
    protected Collider2D[] colliders;

    //Effects variables
    private GameObject particleInstance;
    private ParticleEmiter particleEmiter;

    public override void Init(Player player, RuneController runeController)
    {
        base.Init(player, runeController);
        weaponProjection.Set(Range, 1.5f);

        //Mover este codigo a otra clase que se encargue de instanciar el efecto a la posicion y al transform correcto,
        //Tambien que se encargue de removerlo y de aplicar pooling si hace falta.
        if(effects.ParticleEffect != null)
        {
            //Particles Effects es Empty GO que contiene a todos los efectos
            var particlesParent = player.transform.GetChildByName("Particles Effects");
            var desiredPosition = player.transform.GetChildByName("Ground Checker").position;
            particleInstance = Instantiate(effects.ParticleEffect, desiredPosition + effects.offset,
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
            //Refactorizar.
            //TODO: Crear una clase intermedia entre abilities y movement que se encargue de la logica del movimiento para las habilidades.
            AttackDirection(player);
            player.Stats.Speed = _minValueSpeed;
            DOTween.Complete(player.Stats.Speed);
            DOTween.To(() => player.Stats.Speed, x => player.Stats.Speed = x, player.Stats.MaxSpeed, _timeToTween);

            //De momento este codigo esta bien.
            OnAbilityPressedEvent.Raise(this);
            m_timer.ResetTimer();
            particleEmiter?.Play();
            action?.Invoke();
        }
    }

    public override void OnCollisionLogic(Player player)
    {
        colliders = Physics2D.OverlapBoxAll(player.weapon.position, weaponProjection, 0f, _whatIsEnemy);

        foreach(var coll in colliders)
        {
            if (coll != null)
            {
                IDamageable enemyHealth = coll.GetComponent<EnemyHealth>();

                if (enemyHealth?.CurrentHealth >= 1f)
                {
                    int randomDamage = UnityEngine.Random.Range(_minEffect, _maxEffect);
                    enemyHealth.TakeDamage(randomDamage, Vector2.zero);
                    OnCollisionLogicEvent.Raise(this);

                    if (RuneCost == 0) RuneController.ChargeRune();
                }
            }

        }
    }

    //Mover este codigo a una clase encargada del estado de combate.
    protected virtual void AttackDirection(Player player)
    {
        Vector2 weaponPos = Vector2.zero;

        if (AbilityType == AbilityType.Melee)
        {
            weaponPos = (player.Movement.FacingSide == FacingSide.Right) ? Vector2.right : -Vector2.right;
            player.weapon.localPosition = weaponPos;
        }
        else if(AbilityType == AbilityType.AreaMelee)
        {
            player.weapon.localPosition = weaponPos;
        }
    }

    //Este metodo debe llamarse cuando la habilidad es removida del panel de hechizos activos.
    //Dado que el efecto de particula solo se instancia 1 vez al ser establecido en el panel de hechizos activos,
    //no veo la necesidad de realizar pooling.
    public override void Remove()
    {
        Destroy(particleInstance);
    }
}
