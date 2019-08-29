using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bissash.IA
{
    [CreateAssetMenu(menuName = "IA/States/PatrolState")]
    public class PatrolState : BaseState
    {
        public override void Enter(IABrain brain)
        {
            StateValues.InitRandomValues();
            behaviour.Init(brain, this);

            //Aun por revisar.
            brain.Sensor.StartCombat += CombatTrigger;
        }

        public override void Excecute(IABrain brain)
        {
            behaviour.BehaviourExcecute(brain);
        }

        public override void Exit(IABrain brain)
        {
            //Aun por revisar.
            brain.Sensor.StartCombat -= CombatTrigger;
        }

        #region Revisar
        //Aun por revisar.
        public override void Transitions(IABrain brain)
        {
            float distance = brain.Sensor.TargetDistance;

            if(distance > StateValues.MaxRange)
            {
                brain.SetState(StateType.Combat);
            }
        }

        //aun por revisar.
        private void CombatTrigger(IABrain brain)
        {
            brain.SetState(StateType.Combat);
        }
        #endregion
    }
}

