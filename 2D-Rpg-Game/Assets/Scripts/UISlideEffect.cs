using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UISlideEffect : MonoBehaviour
{
    private Vector2 initialPosition;

    public float seconds;
    public Ease easyType;
    [SerializeField]private Vector2 toPosition;
    [SerializeField] private Vector2 fromPosition;
    private RectTransform ownRecTransform;

    private void Awake()
    {
        ownRecTransform = (RectTransform)transform;
        initialPosition = ownRecTransform.position;
    }

    private void OnEnable()
    {
        PerformSlide();
    }

    private void OnDisable()
    {
        ownRecTransform.position = fromPosition;
    }

    public void PerformSlide()
    {
        toPosition = initialPosition;
        ownRecTransform.DOKill();
        ownRecTransform.DOMove(toPosition, seconds).SetEase(easyType);
    }
}
