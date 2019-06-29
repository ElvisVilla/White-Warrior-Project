using UnityEngine;
using System;
using DG.Tweening;
using TMPro;
using System.Collections;

public class EnemyHealth : MonoBehaviour, IHealth
{
    public event Func<IEnumerator> OnDamage;

    #region Properties
    public bool IsDead { get; set; }
    public int CurrentHealth { get { return initialHealth; } set{ initialHealth = value; } }
    #endregion

    [Header("Set Health Bar")]
    [SerializeField] private int minInitialHealth = 39;
    [SerializeField] private int maxInitialHealth = 41;
    public int initialHealth;

    public GameObject floatingTextPrefab;
    Animator anim;
    SpriteRenderer sprite;
    CapsuleCollider2D coll;

    // Use this for initialization
    void Awake ()
    {
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        coll = GetComponent<CapsuleCollider2D>();
	}

    void Start()
    {
        initialHealth = UnityEngine.Random.Range(minInitialHealth, maxInitialHealth);
        CurrentHealth = initialHealth;
        coll.isTrigger = false;
    }

    private void Update()
    {
        sprite.color = Color.white;
    }

    public void TakeDamage(int damageAmount)
    {
        initialHealth -= damageAmount;
        if(CurrentHealth > 4)
            sprite.DOColor(Color.red, 0.1f);

        if(OnDamage != null)
        {
            StartCoroutine(OnDamage());
        }

        if (floatingTextPrefab != null)
        {
            ShowDamage(damageAmount);
        }
    }

    private void ShowDamage(int damage)
    {
        var instance = Instantiate(floatingTextPrefab, transform.position + new Vector3(0,1.3f,0), Quaternion.identity, transform);
        instance.GetComponent<TextMeshPro>().text = damage.ToString();
    }

    public void Die()
    {
        IsDead = true;
        anim.CrossFade("Enemy_Die", 0f);
        sprite.DOColor(new Color(1f, 1f, 1f, 0f), 8f);
        Destroy(gameObject, 5.2f);
        coll.isTrigger  = true;
    }
}
