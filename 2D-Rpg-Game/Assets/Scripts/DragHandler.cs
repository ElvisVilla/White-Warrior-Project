using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Transform canvas;
    private Transform startingPosition;
    private SpellBar spellbar;
    private Slot originalSlot;
    public Slot destinationSlot;
    //private Image ownImage;

    private void Start()
    {
        //ownImage = GetComponent<Image>();
        canvas = transform.parent.parent.parent;
        startingPosition = transform;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        startingPosition = transform;
        originalSlot = transform.GetComponentInParent<Slot>();
        transform.SetParent(canvas);
        transform.position = eventData.position;
        //ownImage.raycastTarget = false;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GetComponentInChildren<CanvasGroup>().blocksRaycasts = true;

        if (destinationSlot != null)
        {
            if (destinationSlot.Index != originalSlot.Index)
            {
                spellbar.SwapSpell(destinationSlot, originalSlot);
            }
            else if (destinationSlot.Equals(originalSlot))
            {
                transform.SetParent(startingPosition);
            }
        }
        else
        {
            transform.SetParent(startingPosition); //Si es null posiblemente queramos remover el hechizo.
        }
    }

    void Update()
    {

    }
}
