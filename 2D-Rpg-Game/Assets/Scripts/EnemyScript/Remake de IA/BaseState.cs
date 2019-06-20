using UnityEngine;

namespace Bissash.IA
{
    public abstract class BaseState : ScriptableObject
    {
        [Header("")] [SerializeField] protected StateValues stateValues;
        [Header("")] [SerializeField] protected BaseBehaviour behaviour;
        protected Sensor sensor;
        public StateValues StatesValues => stateValues;

        public abstract void Enter(IABrain brain);
        public abstract void Excecute(IABrain brain);
        public abstract void Exit(IABrain brain);

        public abstract void Transitions(IABrain brain);
    }
}
