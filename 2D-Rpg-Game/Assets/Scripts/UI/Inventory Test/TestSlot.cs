using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TestSlot : MonoBehaviour, IDropHandler
{
    public TestItem item;
    public bool hasItem;

    public void OnDrop(PointerEventData eventData)
    {
        
    }
}
