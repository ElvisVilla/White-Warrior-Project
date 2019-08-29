using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bissash.IA
{
    public class Transition
    {
        public BaseState current;

        public void Desitions(IABrain brain)
        {
            current.Transitions(brain);
        }
    }
}

