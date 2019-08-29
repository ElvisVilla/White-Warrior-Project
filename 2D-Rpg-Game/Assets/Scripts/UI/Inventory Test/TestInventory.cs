using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInventory : MonoBehaviour
{
    public List<Transform> slots;

    private void Start()
    {
        slots = transform.GetChildTransforms();
    }

    public void AddItem()
    {
        
    }

    public void RemoveItem()
    {

    }
}
