using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class CombatActions
{
    [SerializeField]
    private Transform weapon;
    public List<Ability> abilities = new List<Ability>();
    public readonly int totalSlot = 4;
    //public SpellBar bar;

    private Animator anim;
    private CharacterStats stats;

	public void Init (Player player)
    {
        for (int i = 0; i < abilities.Count; i++)
        {
            if (abilities[i] != null)
                abilities[i].Init(player);
        }
    }

    public void CombatActionsUpdate(Player player)
    {
        for (int i = 0; i < abilities.Count; i++)
        {
            if (abilities[i] != null)
                abilities[i].UpdateAction(player, weapon);
        }

    }

    public Ability GetAbilities(int index)
    {
        for (int i = 0; i < abilities.Count; i++)
        {
            if (index == i && abilities.Exists(item => item != null))
                return abilities[index];
        }

        return null;
    }

    public void SetAbilities (int index, Ability ability)
    {
        for (int i = 0; i < totalSlot; i++)
        {
            if (i == index)
            {
                if (ability != null)
                    abilities[i] = ability;
            }
        }
    }
}
