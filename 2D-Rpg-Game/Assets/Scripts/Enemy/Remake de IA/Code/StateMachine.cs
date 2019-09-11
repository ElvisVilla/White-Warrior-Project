using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bissash.IA
{
    public class StateMachine
    {
        private BaseState currentlyRunningState;
        private BaseState previousState;

        public BaseState CurrentState => currentlyRunningState;

        public void SetInitialState(BaseState state, IABrain brain)
        {
            currentlyRunningState?.Exit(brain);

            previousState = currentlyRunningState;
            currentlyRunningState = state;
            currentlyRunningState.Enter(brain);
        }

        public void ExcecuteStateUpdate(IABrain brain)
        {
            var runningState = currentlyRunningState;
            runningState?.Excecute(brain);
        }

        public bool SameAsCurrentState(BaseState state)
        {
            return state == currentlyRunningState;
        }

    }
}

