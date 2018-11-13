using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(AudioSource))]
public class BushInteraction : MonoBehaviour
{
    [Header("Toggle for DrawGizmos")]
    [SerializeField] bool DrawGizmos;

    [Header("Physics data")]
    [SerializeField] Vector2 boxCollisionSize;
    [SerializeField] LayerMask whatIsEntities;

    [Header("Sound")]
    [SerializeField] AudioClip clip;
    [Range(0f, 1f)] [SerializeField] float volume;
    
    [Header("Sprite effect")]
    [SerializeField] float colorLerpingValue;
    [SerializeField] int orderInLayerIndex;

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

        sprite.sortingOrder = orderInLayerIndex;
    }

    private void Update()
    {
        sprite.color = new Color(1f, 1f, 1f, 1f);
        var hitInfo = Physics2D.OverlapBox(transform.position, boxCollisionSize, 0f, whatIsEntities);
        PerformInteraction(hitInfo);
    }

    private void PerformInteraction(Collider2D hitInfo)
    {
        if (hitInfo != null)
        {
            int sortingOrder = hitInfo.GetComponentInChildren<SpriteRenderer>().sortingOrder;

            if (sortingOrder < sprite.sortingOrder) //Los Arbustos que esten por detras de los entities no seran transparentes.
            {
                sprite.DOBlendableColor(new Color(1f, 1f, 1f, 0.5f), 0.5f);  
            }

            anim.SetTrigger(animID);

            if (!alreadyPlayed)
            {
                source.PlayOneShot(clip, volume);
                alreadyPlayed = true;
            }
        }
        else //When hitInfo comes to null then alreadyPlayed goes false that allow to Player the bush sound again
        {
            alreadyPlayed = false;
        }
    }

    private void OnDrawGizmos()
    {
        if(DrawGizmos)
        {
            Gizmos.color = Color.black;
            Gizmos.DrawWireCube(transform.position, boxCollisionSize);
        }
    }
}
