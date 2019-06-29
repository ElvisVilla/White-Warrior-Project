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
            DoBehaviour(brain, behaviour.Init);
            brain.Sensor.StartCombat += CombatTrigger;
            StatesValues.InitRandomValues();
        }

        public override void Excecute(IABrain brain)
        {
            DoBehaviour(brain, behaviour.BehaviourExcecute);
        }

        public override void Exit(IABrain brain)
        {
            brain.Sensor.StartCombat -= CombatTrigger;
        }

        public override void Transitions(IABrain brain)
        {
            /*float distance = brain.Sensor.TargetDistance;

            if(distance > StatesValues.MaxRange)
            {
                brain.SetState(StateType.Combat);
            }*/
        }

        public void DoBehaviour (IABrain brain, Action<IABrain, PatrolState> action)
        {
            action(brain, this);
        }

        private void CombatTrigger(IABrain brain)
        {
            brain.SetState(StateType.Combat);
        }
    }
}

