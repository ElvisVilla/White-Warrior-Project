using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bissash.IA
{
    public class StateMachine
    {
        private BaseState currentlyRunningState;
        private BaseState previousState;

        public void SetInitialState(BaseState state, IABrain brain)
        {
            if (currentlyRunningState != null)
                currentlyRunningState.Exit(brain);

            previousState = currentlyRunningState;
            currentlyRunningState = state;
            currentlyRunningState.Enter(brain);
        }

        public void ExcecuteStateUpdate(IABrain brain)
        {
            var runningState = currentlyRunningState;
            if (runningState != null)
                runningState.Excecute(brain);
        }
    }
}

