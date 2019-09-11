using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bissash.IA
{
    [CreateAssetMenu(menuName = "IA/States/Patrol")]
    public class Patrol : BaseState
    {
        private void OnEnable()
        {
            StateType = StateType.Patrol;
            stateValues.InitRandomValues();
        }

        public override void Enter(IABrain brain)
        {
            behaviour.Init(brain, this);
        }

        public override void Excecute(IABrain brain)
        {
            behaviour.BehaviourExcecute(brain, this);
            Transitions(brain);
        }

        public override void Exit(IABrain brain)
        {

        }

        public override void Transitions(IABrain brain)
        {
            //Buscamos al player.
            brain.Sensor.OnDetectBox2D(brain.Position, stateValues.SensorDimension,
                () => brain.SetState(StateType.Combat));
        }
    }
}

