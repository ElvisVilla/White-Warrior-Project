using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SpellBar : MonoBehaviour
{
    [Header("Personaje Actual")]
    public Player player = null;
    int indexCounter = -1;

    [Header("Spell")]
    [SerializeField] GameObject spellPrefab = null;

    private List<Transform> slotList = new List<Transform>();
    private List<Ability> abilities = new List<Ability>();

    //Use this for initialization
    void Awake ()
    {
        SetupActionBar();
    }

    void SetupActionBar()
    {
        slotList = transform.GetChildTransforms();
        abilities = player.abilities;

        foreach (var item in slotList)
        {
            GameObject spellObject = Instantiate(spellPrefab);
            spellObject.transform.SetParent(item.transform);
            spellObject.transform.SetLocalPositionToZero();
            indexCounter++;
            spellObject.GetComponent<Spell>().SetSpell(player, indexCounter, abilities[indexCounter]);
        }
    }
}
