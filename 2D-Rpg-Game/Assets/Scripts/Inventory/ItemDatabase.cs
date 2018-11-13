using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour {

    public List<Item> items = new List<Item>();

    private void Start()
    {
        Debug.Log(GetItem("Espada Maestra").ID);
    }

    public Item GetItem(int id)
    {
        return items.Find(item => item.ID == id);
    }

    public Item GetItem(string itemName)
    {
        return items.Find(item => item.Title == itemName);
    }
}
