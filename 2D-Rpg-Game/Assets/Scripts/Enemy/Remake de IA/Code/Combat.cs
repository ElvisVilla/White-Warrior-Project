using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bissash.IA
{
    [CreateAssetMenu(menuName = "IA/States/Combat")]
    public class Combat : BaseState
    {
        private void OnEnable()
        {
            StateType = StateType.Combat;
            stateValues.InitRandomValues();
        }

        public override void Enter(IABrain brain)
        {
            behaviour.Init(brain, this);
        }

        public override void Excecute(IABrain brain)
        {
            behaviour.BehaviourExcecute(brain, this);
        }

        public override void Exit(IABrain brain)
        {

        }

        public override void Transitions(IABrain brain)
        {
            brain.SetState(StateType.Patrol);
        }
    }
}

