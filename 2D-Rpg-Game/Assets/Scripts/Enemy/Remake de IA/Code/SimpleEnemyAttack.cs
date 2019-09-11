using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemyAttack : MonoBehaviour
{

    [SerializeField] private float range = 2f;
    [SerializeField] private int minDamage = 3;
    [SerializeField] private int maxDamage = 6;
    [SerializeField] private float minCoolDownSeconds = 2;
    [SerializeField] private float maxCoolDownSeconds = 3;
    [SerializeField] private string animationName = null;
    [SerializeField] private Vector2 hitbox = Vector2.zero;
    [SerializeField] private Transform swordTransform = null;
    private float coolDown = 3;

    public LayerMask whatIsTarget;
    public int Damage => Random.Range(minDamage, maxDamage);
    public float Range => range;
    public string AnimationName => animationName;

    // Start is called before the first frame update
    void Awake()
    {
        coolDown = Random.Range(minCoolDownSeconds, maxCoolDownSeconds);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float GetCoolDown()
    {
        return coolDown;
    }

    public void PerformAttack(Animator anim)
    {
        anim.PerformCrossFade(animationName, 0f);
        //Particles effects.

        var coll = Physics2D.OverlapBox(swordTransform.position, hitbox, 0f, whatIsTarget);

        if(coll != null)
        {
            PlayerHealth health = coll.GetComponent<PlayerHealth>();
            health.TakeDamage(Damage, Vector2.zero);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireCube(swordTransform.position, hitbox);
    }
}
