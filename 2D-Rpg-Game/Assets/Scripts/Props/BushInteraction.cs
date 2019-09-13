using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(AudioSource))]
public class BushInteraction : MonoBehaviour
{
    [Header("Physics data")]
    [SerializeField] Vector2 boxCollisionSize = Vector2.zero;
    [SerializeField] LayerMask whatIsEntities = new LayerMask();

    [Header("Sound")]
    [SerializeField] AudioClip clip = null;
    [Range(0f, 1f)] [SerializeField] float volume = 0f;

    bool alreadyPlayed = false;
    float secondsToFade = 0.5f;

    Animator anim;
    SpriteRenderer sprite;
    AudioSource source;
    int? sortingOrder = null;
    bool isFaded = false;

    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
        alreadyPlayed = false;
    }

    private void Update()
    {
        var hitInfo = Physics2D.OverlapBox(transform.position, boxCollisionSize, 0f, whatIsEntities);
        PerformInteraction(hitInfo);
    }

    private void PerformInteraction(Collider2D hitInfo)
    {
        if (hitInfo != null)
        {
            anim.PerformTriggerAnimation("Move");

            if (sortingOrder == null)
            {
                sortingOrder = hitInfo.GetComponentInChildren<SpriteRenderer>().sortingOrder;
                if (sortingOrder < sprite.sortingOrder && isFaded == false)
                {
                    sprite.DOKill();
                    sprite.DOFade(0.6f, secondsToFade);
                    isFaded = true;
                }
            }

            if (!alreadyPlayed)
            {
                source.PlayOneShot(clip, volume);
                alreadyPlayed = true;
            }
        }
        else
        {
            alreadyPlayed = false;
            sortingOrder = null;

            if (isFaded)
            {
                sprite.DOKill();
                sprite.DOFade(1f, secondsToFade);
                isFaded = false;
            }            
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireCube(transform.position, boxCollisionSize);
    }
}
