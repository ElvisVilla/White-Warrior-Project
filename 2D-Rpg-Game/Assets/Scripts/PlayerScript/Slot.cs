using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//El slot necesita guardar la informacion que tiene actualmente el item que lo esta ocupando y esto solo va ocurrir cuando se deba actualizar, pero para actualizar
//Primero debemos saber cuando cambia la informacion de la barra de poderes.
//Al soltar el item podemos activar la actualizacion, sin importar de si el item cambia.
//El metodo de actualizacion debe estar en el spellbar.

public class Slot : MonoBehaviour, IDropHandler
{
    public int Index { get; set; }
    GameObject currentSpell;
    GameObject droppedSpell;

    // Use this for initialization
    void Start()
    {

    }

    public void ChangeItem(int index, GameObject droped)
    {
        //GameObject temporal = currentSpell;
        //currentSpell = droped;

    }

    public void OnDrop(PointerEventData eventData)
    {
        DragHandler dragHandler = null;
        dragHandler = eventData.pointerDrag.GetComponent<DragHandler>();
        dragHandler.destinationSlot = this;
        Debug.Log(dragHandler.name);
    }
}
