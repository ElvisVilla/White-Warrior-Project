using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bissash.IA
{
    [CreateAssetMenu(menuName = "IA/States/Combat")]
    public class Combat : BaseState
    {
        //Vector3 startPosition;
        public override void Enter(IABrain brain)
        {
            //StatesValues.startPosition = brain.Position;
            Debug.Log("Entramos");
        }

        public override void Excecute(IABrain brain)
        {
            
        }

        public override void Exit(IABrain brain)
        {

        }

        public override void Transitions(IABrain brain)
        {
            /*if(StatesValues.MaxRange < Vector3.Distance(StatesValues.startPosition,
                brain.Sensor.TargetPosition))
            {
                brain.SetState(StateType.Patrol);
            }*/

           // if(brain.Healtj)
        }
    }
}

