using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CharacterStats")]
public class CharacterStats : ScriptableObject
{
    //Coleccion de atributos.
    Dictionary<string, float> stats;

    //Variables de movilidad.
    //[SerializeField] private float speed = 4.5f;
    [SerializeField] private float speed = 4.5f;
    public float Speed { get { return speed; } set { speed = value; } }
    public float MaxSpeed => 4f;
    [SerializeField] private float slowedSpeed = 1.5f;


    [SerializeField] private float health = 3f; //Buscar una variable que defina la salud inicial y otra que se gestione para la progresion.

    [SerializeField] private float fuerza = 10f; //10 pts de fuerza equivalen a 1 de daño.
    [SerializeField] private float damage = 1f; //Damage va ser fuerza / 10.

    [SerializeField] private float armor = 3f;
    [SerializeField] private float defence = 1f; //Defence va ser armor / 10.

    [SerializeField] private float magic = 10f;
    [SerializeField] private float abilityPower = 2f; //abilityPower va ser magic / 10;
    
    private void OnEnable()
    {
        stats = new Dictionary<string, float>
        {
            {"speed", Speed},
            {"slowedSpeed", slowedSpeed},
            {"health", health},
            {"strange", fuerza},
            {"damage", damage},
            {"armor", armor},
            {"magic", magic},
            {"AbilityPower", abilityPower},
        };
    }

    //Corrutina para gestionar el PowerUp, en los stats de armor, fuerza y health.
    //Tambien llevara acabo animacion de PowerUp dependiendo cual reciba.
    //El tiempo de effecto del powerUp se llevara acabo en la corrutina.
    //Enviaremos por parametro, el item que es precionado.
    public IEnumerator PowerUp(float seconds) 
    {
        //Aplicando las mejoras.
        yield return new WaitForSeconds(seconds);
        //Eliminando las mejoras.
    }

    public float GetStat(string statName)
    {
        return stats[statName];
    }

    //Este metodo se llama desde afuera cada vez que el jugador sube de nivel o que simplemente es necesario mejorar las estadisticas.
    public void SetStat(string statName, float value)
    { 
        if (stats.ContainsKey(statName))
        {
            float amount = stats[statName] + value;
            stats.Add(statName, amount);
        }
    }
}
