using UnityEngine;
using System;
using TMPro;
using System.Collections;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    //public event Func<IEnumerator> OnDamage;
    #region Properties
    public bool IsDead { get; private set; }
    public int CurrentHealth { get { return initialHealth; } set{ initialHealth = value; } }
    #endregion

    public GameObject floatingTextPrefab;
    [SerializeField] private int initialHealth = 0;
    [SerializeField] private int minInitialHealth = 39;
    [SerializeField] private int maxInitialHealth = 41;


    Animator anim;
    CapsuleCollider2D coll;

    // Use this for initialization
    void Awake ()
    {
        anim = GetComponent<Animator>();
        coll = GetComponent<CapsuleCollider2D>();
	}

    void Start()
    {
        initialHealth = UnityEngine.Random.Range(minInitialHealth, maxInitialHealth);
        CurrentHealth = initialHealth;
        coll.isTrigger = false;
    }

    public void TakeDamage(int damageAmount, Vector2 point)
    {
        CurrentHealth -= damageAmount;

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
        Destroy(gameObject, 5.2f);
        coll.isTrigger  = true;
    }
}
