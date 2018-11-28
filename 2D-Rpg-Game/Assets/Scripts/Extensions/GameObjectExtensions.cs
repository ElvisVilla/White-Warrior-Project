using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameObjectExtensions
{

    static bool IsActive(this GameObject gameObject)
    {
        return gameObject.activeInHierarchy;
    }
	
}
