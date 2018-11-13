using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "item", menuName = "Items/Item")]
public class Item : ScriptableObject
{
    [SerializeField] Sprite icon;
    [SerializeField] int id;
    [SerializeField] string title;
    [TextArea] [SerializeField] string description;

    [Header("Atributes")]
    [SerializeField]float power;
    [SerializeField]float defence;
    [SerializeField]float sellValue;

    Dictionary<string, float> stats;

    public int ID => id;
    public string Title => title;
    public Sprite Icon => icon;

    private void OnEnable()
    {
        stats = new Dictionary<string, float>
        {
            {"Power", power},
            {"Defence", defence},
            {"SellValue", sellValue},
        };
    }

    //Constructor o Init dependiendo de si es ScriptableObject.
    /*public Item (Sprite icon, int ID, string title, string description, Dictionary<string, float> stats)
    {
        icon = Resources.Load<Sprite>("Sprites/Items/" + title);
        this.ID = ID;
        this.title = title;
        this.description = description;
        this.stats = stats;
    }

    public Item(Item item)
    {
        icon = Resources.Load<Sprite>("Sprites/Items/" + item.title);
        ID = item.ID;
        title = item.title;
        description = item.description;
        stats = item.stats;
    }
    */

}
