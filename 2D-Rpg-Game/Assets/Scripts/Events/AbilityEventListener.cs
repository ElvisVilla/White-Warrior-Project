using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

[Serializable]
public class AbilityAction : UnityEvent<Ability>
{

}

public class AbilityEventListener : MonoBehaviour
{
    [Tooltip("Event to register with.")]
    public AbilityEvent Event;

    [Tooltip("Response to invoke when Event is raised.")]
    public AbilityAction Response;

    private void OnEnable()
    {
        Event.AddListener(this);
    }

    private void OnDisable()
    {
        Event.RemoveListener(this);
    }

    public void OnEventRaised(Ability ability)
    {
        Response.Invoke(ability);
    }
}
