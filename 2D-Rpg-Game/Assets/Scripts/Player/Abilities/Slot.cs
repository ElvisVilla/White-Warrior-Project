using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//El slot necesita guardar la informacion que tiene actualmente el item que lo esta ocupando y esto solo va ocurrir cuando se deba actualizar, pero para actualizar
//Primero debemos saber cuando cambia la informacion de la barra de poderes.
//Al soltar el item podemos activar la actualizacion, sin importar de si el item cambia.
//El metodo de actualizacion debe estar en el spellbar.

public class Slot : MonoBehaviour
{
    public int Index { get; set; }

}
