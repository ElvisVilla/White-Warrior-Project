using UnityEngine;
using System;

namespace Bissash.IA
{
    [System.Serializable]
    public class Sensor
    {
        public event Action<IABrain> StartCombat = delegate { };

        private IABrain brain;
        [SerializeField] private Vector2 sensorDimention = Vector2.zero;
        [SerializeField] private LayerMask whatIsPlayer = new LayerMask();

        [Header("Debug values")]
        [SerializeField] private BaseState currentState = null;
        [SerializeField] private Player targetReference = null;

        public Player Target => targetReference;
        public Vector2 TargetPosition 
        {
            get
            {
                if (Target != null)
                {
                    return Target.transform.position;
                }
                else return Vector2.zero;
            }
        }
        public float TargetDistance => (TargetPosition - brain.Position).normalized.sqrMagnitude;
        public Vector2 Dimention => sensorDimention;

        public void Init(IABrain brain)
        {
            this.brain = brain;
        }

        public void Update(IABrain brain)
        {
            currentState = brain.StateMachine.CurrentState;

            Collider2D coll = Physics2D.OverlapBox(brain.Position, sensorDimention, 0, whatIsPlayer);
        }
        
        public void OnDetect<T>(Action<T> action, T value, LayerMask layers)
        {
            var coll = Physics2D.Raycast(brain.transform.GetChild(1).position, Vector2.down, 1f, layers);

            if(coll.collider == null)
                action(value);
        }

        public float Distance(Vector2 first, Vector2 second) => Vector2.Distance(first, second);
    }
}