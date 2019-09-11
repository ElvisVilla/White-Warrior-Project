using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bissash.IA;
using Bissash;
using DG.Tweening;

[CreateAssetMenu(menuName = "IA/Behaviour/CombatBehaviour")]
public class CombatBehaviour : BaseBehaviour
{
    public float stopingDistance;
    [SerializeField] SimpleEnemyAttack attack;
    [SerializeField] EnemyHealth health;
    [SerializeField]IABrain brain;
    [SerializeField]int damageCount;

    public override void Init(IABrain brain, BaseState state)
    {
        attack = brain.GetComponent<SimpleEnemyAttack>();
        health = brain.GetComponent<EnemyHealth>();
        brain.timer.SetGoalSeconds(attack.GetCoolDown());
    }

    public override void BehaviourExcecute(IABrain brain, BaseState state)
    {
        //Lo hacemos aca para evitar asignaciones innecesarias.
        if (health.IsDead) return;

        brain.timer.RunTimer();

        this.brain = brain;
        StateValues values = state.StateValues;
        Sensor sensor = brain.Sensor;

        if (sensor.OnPlayerDetected(brain.Position, values.SensorDimension))
        {
            if (brain.combatMode == CombatMode.RegularCombat)
            {
                PerformCombat(brain, state);
            }
            else if(brain.combatMode == CombatMode.Stun)
            {
                //Efecto de particulas
                //Sonidos.
                brain.Motor.MoveAnimation(brain.Anim, "Speed", 0f); //Pasamos a idle (No tengo animacion de Stun).
            }
        }
        else
        {
            state.Transitions(brain);
        }
    }

    void PerformCombat(IABrain brain, BaseState state)
    {
        StateValues values = state.StateValues;
        Sensor sensor = brain.Sensor;
        float moveSpeed = values.MovementSpeed;
        float slowMoveSpeed = values.MinMoveSpeed;

        if (brain.timer.ElapsedSeconds() >= attack.GetCoolDown())
        {
            if(sensor.TargetDistance >= stopingDistance)
            {
                brain.Motor.Move(brain, sensor.TargetPosition, moveSpeed);
                brain.Motor.MoveAnimation(brain.Anim, values.AnimationName, moveSpeed);
            }
            
            if (sensor.TargetDistance <= attack.Range)
            {
                attack.PerformAttack(brain.Anim);
                brain.timer.ResetTimer();
            }else 

            return; //Mantenemos el moveSpeed hasta que el ataque entre en cooldown.
        }

        if (sensor.TargetDistance >= stopingDistance)
        {
            brain.Motor.Move(brain, sensor.TargetPosition, slowMoveSpeed);
           brain.Motor.MoveAnimation(brain.Anim, values.AnimationName, slowMoveSpeed);
        } else
        {
            brain.Motor.MoveAnimation(brain.Anim, values.AnimationName, 0f);
        }
    }

    //Lo llamamos desde un GameEventListener.
    public void OnDamage()
    {
        IEnumerator Damaged()
        {
            brain.brainDamageCount++;
            brain.combatMode = CombatMode.Stun;
            if(brain.brainDamageCount == 3)
            {
                yield return new WaitForSeconds(1.5f);
                brain.brainDamageCount = 0;
            }
            else
            {
                yield return new WaitForSeconds(0.2f);
            }

            brain.combatMode = CombatMode.RegularCombat;
        }

        brain.StartCoroutine(Damaged());
    }
}
