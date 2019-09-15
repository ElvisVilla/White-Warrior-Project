using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class AbilityEvent : ScriptableObject
{
    readonly List<AbilityEventListener> eventListeners = 
        new List<AbilityEventListener>();

    public void Raise(Ability ability)
    {
        for (int i = eventListeners.Count - 1; i >= 0; i--)
            eventListeners[i].OnEventRaised(ability);
    }

    public void AddListener(AbilityEventListener listener)
    {
        if (!eventListeners.Contains(listener))
        {
            eventListeners.Add(listener);
        }
    }

    public void RemoveListener(AbilityEventListener listener)
    {
        if (eventListeners.Contains(listener))
        {
            eventListeners.Remove(listener);
        }
    }
}

