using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SpellBar : MonoBehaviour
{
    [Header("Personaje Actual")]
    public GameObject actualPlayer;
    private Player player;
    private CombatActions combat;

    [Header("Panel de Habilidades.")]
    [SerializeField] private GameObject spellPrefab;
    private GameObject actionBarPanel;
    private List<GameObject> slotList = new List<GameObject>();
    private List<Ability> abilities = new List<Ability>();

    //Use this for initialization
    void Awake ()
    {
        PlayerSetup();
        SetupActionBar();
    }

    void PlayerSetup()
    {
        actualPlayer = GameObject.FindGameObjectWithTag("Player");

        if (actualPlayer != null)
        {
            player = actualPlayer.GetComponent<Player>();
            combat = player.Combat;
        }
        else
            Debug.LogError("No se ha encontrado el Personaje actual. Enlaza el Personaje en el inspector o asignale el tag de "
                + "Player" + "para corregir el error");
    }

    void SetupActionBar()
    {
        actionBarPanel = gameObject;

        for (int counter = 0; counter < actionBarPanel.transform.childCount; counter++)
        {
            slotList.Add(actionBarPanel.transform.GetChild(counter).gameObject);
            slotList[counter].GetComponent<Slot>().Index = counter;
            GameObject spellObject = Instantiate(spellPrefab);
            spellObject.transform.SetParent(slotList[counter].transform);
            spellObject.transform.localPosition = Vector3.zero;

            if (combat.abilities.Exists(item => item != null))
            {
                abilities.Add(combat.GetAbilities(counter));
                spellObject.GetComponent<Spell>().SetSpell(player, counter, abilities[counter]);
            }
        }
    }

    public void SwapSpell(Slot destinationSlot, Slot originalSlot)
    {
        //Los slots tienen un index y con ello puedo saber a que index debo cambiar.
        GameObject destinationSpell = destinationSlot.transform.GetChild(0).gameObject;
        GameObject originalSpell = originalSlot.transform.GetChild(0).gameObject;

        originalSpell.transform.SetParent(destinationSlot.transform);
        originalSpell.transform.localPosition = Vector3.zero;
        destinationSpell.transform.SetParent(originalSlot.transform);
        destinationSpell.transform.localPosition = Vector3.zero;
    }

    public Ability GetAbility(int index)
    {
        return abilities[index];
    }

    public List<Ability> GetAbilities()
    {
        return abilities;
    }

    public void SetAbilty(int index, Ability ab)
    {
        if (abilities[index].Equals(null))
            abilities.Insert(index, ab);

        else
        {
            abilities.RemoveAt(index);
            abilities.Insert(index, ab);
        }
    }


}
