using UnityEngine;
using DG.Tweening;
using Bissash;
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
    private GhostTrail ghostTrail;

    public override void Init(Player player, RuneController runeController)
    {
        base.Init(player, runeController);
        effects.Init(player);

        ghostTrail = FindObjectOfType<GhostTrail>();
        weaponProjection.Set(Range, 1.5f);
    }

    public override void AbilityUpdate(Player player)
    {
        base.AbilityUpdate(player);
    }

    public override void OnAbilityPressed(Player player, Action action)
    {
        if (m_timer.TimeHasComplete())
        {
            /*TODO: Crear una clase intermedia entre abilities y movement que se encargue de la logica del movimiento 
             * para las habilidades. Esa clase debe gestionar los impulsos de movimiento (si los hay)
             * o detener el movimiento del jugador para los ataques que lo requieran o detener el movimiento del
             * jugador al colisionar con el enemigo.*/

            AttackDirection(player);

            //Realizar esto con DoMove()
            player.Stats.Speed = _minValueSpeed;
            DOTween.To(() => player.Stats.Speed, x => player.Stats.Speed = x, player.Stats.MaxSpeed, _timeToTween);

            if (RuneCost > 0)
            {
                ghostTrail.ShowGhost();
            }

            //De momento este codigo esta bien.
            player.Anim.PerformCrossFade(AnimationName, 0f);
            m_timer.ResetTimer();
            effects.PlayParticles();
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
                IDamageable enemyHealth = coll.GetComponent<IDamageable>();

                if (enemyHealth?.CurrentHealth >= 1f) //Cambiar la interfaz para que admita IsDead.
                {
                    int randomDamage = UnityEngine.Random.Range(_minEffect, _maxEffect);
                    enemyHealth.TakeDamage(randomDamage, Vector2.zero);
                    player.CameraManager.OnAbilityCameraEffect(this);

                    //Evitamos cargar las runas cuando usamos un ataque que consuma runas.
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
        effects.CleanParcilesOnRemove();
    }
}
