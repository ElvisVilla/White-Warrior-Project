using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneController : MonoBehaviour
{
    Rune[] runesCollection;
    public int cargedRunesCount { get; set; } = 0; //Lleva la cuenta de las runas cargadas.

    private void Awake()
    {
        runesCollection = GetComponentsInChildren<Rune>();
    }

    /// <summary>
    /// Cuando golpee basico conecte con el enemigo RunesController debe establecer las runas en el orden especifico como cargadas
    /// Esto ocurre solo cuando golpe basico conecta, osea que es un evento. RunesController debe llevar la cuenta de las runas ya activas
    /// Y solo activar las no activas.
    /// </summary>
    public void ChargeRune()
    {        
        if (cargedRunesCount < runesCollection.Length)
        {
            runesCollection[cargedRunesCount].SetCharge();
            cargedRunesCount++;
        }
    }

    /// <summary>
    /// El metodo se encarga de gartar las runas cargadas, partiendo de la cantidad de runas que tiene para usar, lo realiza de forma
    /// inversa, hasta llegar a cero.
    /// </summary>
    /// <param name="totalRuneUsed"></param>
    public void UseRunes(int totalRuneUsed)
    {
        //int cero = 0;
        while(totalRuneUsed > 0)
        {
            totalRuneUsed--;
            if (cargedRunesCount > 0) //Count va disminuir partiendo desde la ultima runa cargada.
            {
                cargedRunesCount--;
                runesCollection[cargedRunesCount].UnCharge();
            }
        }
    }

    public bool GotRunes(int runeCost)
    {
        return (cargedRunesCount >= runeCost);
    }
}
