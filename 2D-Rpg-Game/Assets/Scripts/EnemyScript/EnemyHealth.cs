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
    public float CurrentHealth { get { return initialHealth; } set{ initialHealth = value; } }
    #endregion

    [Header("Set Health Bar")]
    [SerializeField] private int minInitialHealth = 39;
    [SerializeField] private int maxInitialHealth = 41;
    public float initialHealth;

    public GameObject floatingTextPrefab;
    Animator anim;
    SpriteRenderer sprite;
    Rigidbody2D body2D;

    // Use this for initialization
    void Awake ()
    {
        anim = GetComponent<Animator>();
        body2D = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        Physics2D.IgnoreLayerCollision(12, 9, false);

        initialHealth = UnityEngine.Random.Range(minInitialHealth, maxInitialHealth);
        CurrentHealth = initialHealth;
	}

    private void Update()
    {
        sprite.color = Color.white;
    }

    public void TakeDamage(float damageAmount)
    {
        initialHealth -= damageAmount;
        sprite.DOColor(Color.red, 0.1f);
        StartCoroutine(OnDamage()); //Evento.

        if (floatingTextPrefab != null)
        {
            ShowDamage(damageAmount);
        }
    }

    private void ShowDamage(float damage)
    {
        var instance = Instantiate(floatingTextPrefab, transform.position + new Vector3(0,1.3f,0), Quaternion.identity, transform);
        instance.GetComponent<TextMeshPro>().text = damage.ToString();
    }

    public void Die()
    {
        IsDead = true;
        anim.CrossFade("Enemy_Die", 0f);
        sprite.DOColor(new Color(1f, 1f, 1f, 0f), 8f);
        Physics2D.IgnoreLayerCollision(12, 9);
        Destroy(gameObject, 5.2f);
    }
}
