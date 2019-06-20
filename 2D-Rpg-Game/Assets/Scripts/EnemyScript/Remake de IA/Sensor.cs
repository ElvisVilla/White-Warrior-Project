using UnityEngine;
using System;

namespace Bissash.IA
{
    [System.Serializable]
    public class Sensor
    {
        public event Action<IABrain> StartCombat = delegate { };

        public Vector2 sensorDimensions;
        [SerializeField] private BaseState currentState;
        [SerializeField] private LayerMask whatIsPlayer;
        [SerializeField] private Player targetReference;
        private IABrain brain;

        public Player Target => targetReference;
        public Vector3 TargetPosition 
        {
            get
            {
                if (Target != null)
                {
                    return Target.transform.position;
                }
                else return Vector3.zero;
            }
        }
        public float TargetDistance => (TargetPosition - brain.Position).sqrMagnitude;

        public void Init(IABrain brain)
        {
            this.brain = brain;
        }

        public void Update(IABrain brain)
        {
            this.brain = brain;
            currentState = brain.StateMachine.CurrentState;
            currentState.Transitions(brain); //Aun por definir.

            Collider2D coll = Detected(brain);
            SetPlayerReference(coll);

            if(coll != null && currentState is PatrolState)
            {
                StartCombat?.Invoke(brain); 
            }
        }

        private void SetPlayerReference(Collider2D coll)
        {
            if (currentState is PatrolState || currentState is Dead)
            {
                targetReference = null;
            }
            else if (currentState is Combat)
            {
                targetReference = null ?? coll?.GetComponent<Player>();
            }
        }

        private Collider2D Detected(IABrain brain)
        {
           return Physics2D.OverlapBox(brain.Position, sensorDimensions, 0, whatIsPlayer);
        }        
    }
}