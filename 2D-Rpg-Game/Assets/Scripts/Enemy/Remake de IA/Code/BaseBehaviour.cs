using UnityEngine;

namespace Bissash.IA
{
    public abstract class BaseBehaviour : ScriptableObject
    {
        public abstract void Init(IABrain brain, BaseState state);
        public abstract void BehaviourExcecute(IABrain brain, BaseState state);
    }
}