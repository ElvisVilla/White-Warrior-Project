using UnityEngine;

namespace Bissash.IA
{
    public abstract class BaseState : ScriptableObject
    {
        [Header("")] [SerializeField] protected StateValues stateValues;
        [Header("Set Behaviour")]
        [Header("")] [SerializeField] protected BaseBehaviour behaviour;
        public StateValues StateValues => stateValues;
        public BaseBehaviour Behaviour => behaviour;

        public StateType StateType { get; protected set; }

        public abstract void Enter(IABrain brain);
        public abstract void Excecute(IABrain brain);
        public abstract void Exit(IABrain brain);

        //Por Revisar.
        public abstract void Transitions(IABrain brain);
    }
}
