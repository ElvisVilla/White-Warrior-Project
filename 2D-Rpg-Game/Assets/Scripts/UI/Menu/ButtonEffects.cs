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
    AudioSource source;

    public UnityEvent Event;



    private void Start()
    {
        backGroundImage = GetComponentInParent<Image>();
        rectransform = (RectTransform)transform;
        source = GetComponent<AudioSource>();

        backGroundImage.color = Color.clear;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        backGroundImage.color = color;
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
        rectransform.DOScale(1, 0.3f).SetEase(easeType);
        backGroundImage.color = Color.clear;
    }

    

    
    
}
