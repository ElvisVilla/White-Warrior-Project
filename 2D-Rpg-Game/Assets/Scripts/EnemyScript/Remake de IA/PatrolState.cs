using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bissash.IA
{
    [CreateAssetMenu(menuName = "IA/States/PatrolState")]
    public class PatrolState : BaseState
    {

        public override void Enter(IABrain brain)
        {
            patrolBehaviour.Init(brain, this);
        }

        public override void Excecute(IABrain brain)
        {
        }

        public override void Exit(IABrain brain)
        {
        }
    }
}

