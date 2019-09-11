using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bissash.IA
{
    [CreateAssetMenu(menuName = "IA/States/Dead")]
    public class Dead : BaseState
    {
        private void OnEnable()
        {
            StateType = StateType.Dead;
        }

        public override void Enter(IABrain brain)
        {
            throw new System.NotImplementedException();
        }

        public override void Excecute(IABrain brain)
        {
            throw new System.NotImplementedException();
        }

        public override void Exit(IABrain brain)
        {
            throw new System.NotImplementedException();
        }

        public override void Transitions(IABrain brain)
        {
            throw new System.NotImplementedException();
        }
    }
}

