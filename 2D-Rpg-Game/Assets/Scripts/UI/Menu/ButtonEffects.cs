using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class ButtonEffects : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerExitHandler
{
    public float seconds;
    public Ease easeType;
    public AudioClip transitionSound;
    public AudioClip clickSound;
    public Color color;

    RectTransform rectransform;
    Image backGroundImage;

    [Tooltip("El Audio Source debe ser obtenido de un objeto que no se desactive y que pueda emitir el sonido")]
    [SerializeField] AudioSource source; 

    public UnityEvent Event;

    private void Start()
    {
        backGroundImage = GetComponent<Image>();
        rectransform = (RectTransform)transform;

        backGroundImage.color = Color.clear;
    }

    private void OnDisable()
    {
        backGroundImage.color = Color.clear;
        rectransform.DOScale(1, 0f);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        backGroundImage.color = color;
        rectransform.DOKill();
        rectransform.DOScale(1.2f, 0.3f).SetEase(easeType);
        source.PlayOneShot(transitionSound);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        source.PlayOneShot(clickSound);
        Event?.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        rectransform.DOKill();
        rectransform.DOScale(1, 0.3f).SetEase(easeType);
        backGroundImage.color = Color.clear;
    }
}
