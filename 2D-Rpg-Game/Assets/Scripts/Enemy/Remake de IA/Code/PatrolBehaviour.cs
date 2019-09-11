using UnityEngine;
using System.Collections;

namespace Bissash.IA
{
    [CreateAssetMenu(menuName = "IA/Behaviour/PatrolBehaviour")]
    public class PatrolBehaviour : BaseBehaviour
    {
        //El error podria encontrarse en los campos del estado.
        /*[SerializeField]private Transform[] waypoints = null;
        [SerializeField]private Transform waypointTarget = null;
        [SerializeField]private IAMotor motor;
        [SerializeField]private int index;
        [SerializeField]private bool noFloor = false;

        [SerializeField] private float reachedDistance = 0.5f;*/

        public override void Init(IABrain brain, BaseState state)
        {
            /*if (brain.GetWaypoints() != null)
                waypoints = brain.GetWaypoints();
            else
                throw new System.Exception("La lista de waypoints esta vacia");

            //motor = brain.Motor;
            //index = (int)motor.GetFacingSide(); //initiamos el index con la direccion establecida en inspector.
            //waypointTarget = waypoints[index];*/
        }

        public override void BehaviourExcecute(IABrain brain, BaseState state)
        {
            WaypointsUpdate(brain);
            PatrolMovement(brain, state);
        }

        void WaypointsUpdate(IABrain brain)
        {
            if (brain.GetLenght() == 0) return;

            brain.distance = Vector2.Distance(brain.GetTargetWithIndex(brain.brainIndex).position, brain.Position);
            if (brain.distance < brain.reachDistance || brain.brainNofloor == true)
            {
                brain.brainIndex++;
                brain.brainNofloor = false;

                if (brain.brainIndex >= brain.GetLenght())
                {
                    brain.brainIndex = 0;
                }

                brain.waypointPosition = brain.GetTargetWithIndex(brain.brainIndex).position;
            }
        }

        void PatrolMovement(IABrain brain, BaseState state)
        {
            //StateValues values = state.StateValues;
            //Vector2 targetPosition = brain.waypointTarget.localPosition;

            brain.Motor.MoveAnimation(brain.Anim, state.StateValues.AnimationName, state.StateValues.MovementSpeed);
            brain.Motor.Move(brain, brain.waypointPosition, state.StateValues.MovementSpeed);
            brain.Sensor.OnNoFlorDetected(()=> OnNoFloor(brain), state.StateValues.WhatIsFloor, false);
        }

        void OnNoFloor(IABrain brain)
        {
            var facingSide = brain.Motor.IsFacingSide(FacingSide.Right) ? FacingSide.Left : FacingSide.Right;
            brain.Motor.SetFacingSide(facingSide, brain);
            brain.brainNofloor = true;
        }
    }
}

