using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public enum TransitionType
{
    Slide,
    Fade,
}

public class SceneTransition : MonoBehaviour
{
    public Image slideEffect;
    public Image fadeEffect;
    public TransitionType transition;
    public Ease ease;
    public float slideSeconds;
    public float fadeSeconds;

    // Start is called before the first frame update
    void Awake()
    {
        if(transition == TransitionType.Slide)
        {
            slideEffect.enabled = true;
            slideEffect.transform.DOMoveX(-2000, slideSeconds).SetEase(ease);
        }
        else if(transition == TransitionType.Fade)
        {
            fadeEffect.enabled = true;
            fadeEffect.DOFade(0f, fadeSeconds).SetEase(ease);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
