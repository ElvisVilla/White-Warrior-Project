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

    int animID = Animator.StringToHash("Move");
    bool alreadyPlayed = false;

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
            if(sortingOrder == null)
            {
                sortingOrder = hitInfo.GetComponentInChildren<SpriteRenderer>().sortingOrder;
                if (sortingOrder < sprite.sortingOrder && isFaded == false)
                {
                    sprite.DOFade(0.6f, 0.7f);
                    isFaded = true;
                }
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
            sortingOrder = null;
            if (isFaded)
            {
                sprite.DOFade(1f, 0.4f);
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
