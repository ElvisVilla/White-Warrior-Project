using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SpellBar : MonoBehaviour
{
    [Header("Personaje Actual")]
    public GameObject actualPlayer;
    private List<Ability> Abilities = new List<Ability>();
    private Player player;
    private CombatActions combat;

    [Header("Panel de Habilidades.")]
    [SerializeField] private GameObject Panel;
    [SerializeField] private GameObject spellPrefab;
    private List<GameObject> slots = new List<GameObject>();

	// Use this for initialization
	void Awake ()
    {
        PlayerSetup();

        for (int i = 0; i < Panel.transform.childCount; i++)
        {
            slots.Add(Panel.transform.GetChild(i).gameObject);
            GameObject spell = Instantiate(spellPrefab);
            spell.transform.SetParent(slots[i].transform);
            spell.transform.localPosition = Vector3.zero;
            
            if(combat.abilities.Exists(item => item != null))
            {
                Abilities.Add(combat.GetAbilities(i));
                if(Abilities[i] != null)
                    spell.GetComponent<SpellAction>().SetSpellAbility(player, i, Abilities[i]);
            }
        } 
    }

    //Lo que el jugador arrastre a la barra de poderes debe actualizarse en las habilidades adentro de la barra de poderes y luego enviarse a combatActions.
    void Update()
    {
        /*for (int i = 0; i < Panel.transform.childCount; i++)
        {
            Abilities.Add(combat.GetAbilities(i));
            GameObject spell = slots[i].transform.GetChild(0).gameObject;
            if(Abilities.Exists(item => item != null))
            {
                //spell.GetComponent<SpellAction>().SetSpellAbility(player, i, Abilities[i]);
            }
        }*/
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
        {
            Debug.LogError("No se ha encontrado el Personaje actual. Enlaza el Personaje en el inspector o asignale el tag de "
                + "Player" + "para corregir el error");
            return;
        }
    }

    //Este metodo debe estar en spellBar y dependiendo el index en el tenga ese spellGO en el paren determinaremos la posicion del ataque que deseamos obtener.
    //una vez obtenemos el ataque es cuando realizamos metodo action, que por cierto recibe un Player.
    public Ability GetActionFromAbilities(Player player, int index)
    {
        //Si conocemos el index de la lista de poderes accederemos al mismo poder o esa es la idea :'V.
        return Abilities[index];
    }
}
