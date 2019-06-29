using UnityEngine;
using System;

namespace Bissash.IA
{
    [System.Serializable]
    public class Sensor
    {
        public event Action<IABrain> StartCombat = delegate { };

        private IABrain brain;
        [SerializeField] private Vector2 sensorDimention;
        [SerializeField] private LayerMask whatIsPlayer;

        [Header("Debug values")]
        [SerializeField] private BaseState currentState;
        [SerializeField] private Player targetReference;

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
        public float TargetDistance => (TargetPosition - brain.Position).sqrMagnitude; //Por testear.
        public Vector2 Dimention => sensorDimention;

        public void Init(IABrain brain)
        {
            this.brain = brain;
        }

        public void Update(IABrain brain)
        {
            this.brain = brain;
            currentState = brain.StateMachine.CurrentState;
            currentState.Transitions(brain); //Aun por definir la logica y el lugar de transicion.

            Collider2D coll = Detecting(brain);
            SetPlayerReference(coll);

            if(coll != null && currentState is PatrolState)
            {
                //StartCombat?.Invoke(brain); 
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

        private Collider2D Detecting(IABrain brain)
        {
           return Physics2D.OverlapBox(brain.Position, sensorDimention, 0, whatIsPlayer);
        }
        
        public void DetectedEvent<T>(Action<T> action, T pValue, LayerMask layer)
        {
            var coll = Physics2D.Raycast(brain.transform.GetChild(1).position, Vector2.down, 1f, layer);

            if(coll.collider == null)
                action(pValue);
        }

        public float MeasureDistance(Vector2 first, Vector2 second)
        {
            return Vector2.Distance(first, second);
        }
    }
}