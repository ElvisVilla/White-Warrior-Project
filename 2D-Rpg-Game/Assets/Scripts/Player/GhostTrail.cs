using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GhostTrail : MonoBehaviour
{
    private Movement move;
    public Color trailColor;
    public Color fadeColor;
    public float ghostInterval;
    public float fadeTime;

    private void Start()
    {
        move = FindObjectOfType<Movement>();
    }

    public void ShowGhost()
    {
        Sequence secuence = DOTween.Sequence();

        transform.GetChildTransforms().ForEach(item =>
        {
            secuence.AppendCallback(() => item.position = move.transform.position);
            secuence.AppendCallback(() => item.GetComponent<SpriteRenderer>().enabled = true);
            secuence.AppendCallback(() => item.GetComponent<SpriteRenderer>().flipX = move.renderer2D.flipX);
            secuence.AppendCallback(() => item.GetComponent<SpriteRenderer>().sprite = move.renderer2D.sprite);
            secuence.Append(item.GetComponent<SpriteRenderer>().material.DOColor(trailColor, 0));
            secuence.AppendCallback(() => FadeSprite(item));
            secuence.AppendInterval(ghostInterval);
        });
    }

    private void FadeSprite(Transform current)
    {
        current.GetComponent<SpriteRenderer>().material.DOKill();
        current.GetComponent<SpriteRenderer>().material.DOColor(fadeColor, fadeTime);
    }
}
