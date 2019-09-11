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
        [SerializeField] private Transform groundChecker;
        [SerializeField] private BaseState currentState = null;
        [SerializeField] private Player targetReference = null;

        public Player Target => targetReference;

        /// <summary>
        /// Retorna la posicion del Target Actual.
        /// </summary>
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
        public float TargetDistance => Vector2.Distance(TargetPosition, brain.Position)/*.normalized.sqrMagnitude*/;
        public Vector2 Dimention => sensorDimention;

        public void Init(IABrain brain)
        {
            this.brain = brain;
        }

        public void Update(IABrain brain)
        {
            if(currentState != brain.CurrentState)
                currentState = brain.CurrentState;
        }

        public void OnNoFlorDetected(Action action, LayerMask layer, bool valueToCompare)
        {
            var coll = Physics2D.Raycast(groundChecker.position, Vector2.down, 1f, layer);

            if(coll == valueToCompare)
                action?.Invoke();
        }

        public void OnDetectBox2D(Vector2 position, Vector2 size, Action actionEvent)
        {
            var coll = Physics2D.OverlapBox(position, size, 0f, whatIsPlayer);

            if (coll != null)
            {
                coll.transform.GetComponentOnce(ref targetReference); //Ontiene la referencia de target una sola vez.
                actionEvent?.Invoke();
            }
        }

        public bool OnPlayerDetected(Vector2 position, Vector2 size)
        {
            var coll = Physics2D.OverlapBox(position, size, 0f, whatIsPlayer);

            if (coll != null)
            {
                coll.transform.GetComponentOnce(ref targetReference);
                return true;
            }

            return false;
        }
    }
}