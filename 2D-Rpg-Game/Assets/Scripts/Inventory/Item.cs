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
}
