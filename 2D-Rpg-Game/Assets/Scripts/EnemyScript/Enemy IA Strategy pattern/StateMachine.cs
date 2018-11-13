using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    private IState currentlyRunningState;
    private IState previousState;

	public void ChangeState(IState newState)
    {
        if(currentlyRunningState != null)
            currentlyRunningState.Exit();

        previousState = currentlyRunningState;
        currentlyRunningState = newState;
        currentlyRunningState.Enter();
    }

    public void ExcecuteStateUpdate()
    {
        var runningState = currentlyRunningState;
        if (runningState != null)
            runningState.Excecute();
    }

    public void SwitchToPreviousState()
    {
        currentlyRunningState.Exit();
        currentlyRunningState = previousState;
        currentlyRunningState.Enter();
    }
}
