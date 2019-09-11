using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using System.Collections;

public class AnimationEvent : MonoBehaviour
{
    public UnityEvent events;
    private CapsuleCollider2D coll2D;
    public Transform groundDetector;
    Vector2 savedSize;
    Vector2 savedPosition;

    private void Awake()
    {
        coll2D = GetComponentInParent<CapsuleCollider2D>();

        savedSize = coll2D.size;
        savedPosition = groundDetector.localPosition;
    }

    //Este metodo es llamado en los clip de animacion de los ataques.
    public void AnimEvent()
    {
        events.Invoke();
    }

    public void ResizeColliderOnJump()
    {
        IEnumerator Resize()
        {
            groundDetector.localPosition = new Vector3(0f, -0.37f, 0f);
            coll2D.size = new Vector2(0.5f, 0.8f);
            yield return new WaitForSeconds(0.5f);
            groundDetector.localPosition = savedPosition;
            coll2D.size = savedSize;
        }

        StartCoroutine(Resize());
    }
}