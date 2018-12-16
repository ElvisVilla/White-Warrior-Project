using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneController : MonoBehaviour
{
    public Rune[] Runes;
    public int Count { get; set; } = 0;

    //Cuando golpee basico conecte con el enemigo RunesController debe establecer las runas en el orden especifico como cargadas
    //Esto ocurre solo cuando golpe basico conecta, osea que es un evento. RunesController debe llevar la cuenta de las runas ya activas
    //Y solo activar las no activas.
    public void ChargeRune()
    {        
        if (Count < Runes.Length)
        {
            Runes[Count].SetCharge();
            Count++;
        }
    }

    public void UseRunes(int totalRuneUsed)
    {
        int cero = 0;
        while(totalRuneUsed > cero) //Cuando totalRuneUsed llegue a 0 el while se quiebra.
        {
            totalRuneUsed--;
            if (Count > 0) //Count va disminuir partiendo desde la ultima runa cargada.
            {
                Count--;
                Runes[Count].SetUncharge();
            }
        }
    }

    public bool GotRunes(int runeCost)
    {
        return (Count >= runeCost);
    }
}
