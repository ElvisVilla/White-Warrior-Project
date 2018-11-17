using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(AudioSource))]
public class BushInteraction : MonoBehaviour
{
    [Header("Physics data")]
    [SerializeField] Vector2 boxCollisionSize;
    [SerializeField] LayerMask whatIsEntities;

    [Header("Sound")]
    [SerializeField] AudioClip clip;
    [Range(0f, 1f)] [SerializeField] float volume;

    int animID = Animator.StringToHash("Move");
    bool alreadyPlayed = false;

    Animator anim;
    SpriteRenderer sprite;
    AudioSource source;

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
            int sortingOrder = hitInfo.GetComponent<SpriteRenderer>().sortingOrder;

            if (sortingOrder < sprite.sortingOrder)
            {
                sprite.DOFade(0.6f, 0.7f);
            }

            anim.SetTrigger(animID);

            if (!alreadyPlayed)
            {
                source.PlayOneShot(clip, volume);
                alreadyPlayed = true;
            }
        }
        else
        {
            alreadyPlayed = false;
            sprite.DOFade(1f, 0.4f);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireCube(transform.position, boxCollisionSize);
    }
}
