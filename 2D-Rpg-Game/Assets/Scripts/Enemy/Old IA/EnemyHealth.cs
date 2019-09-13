using UnityEngine;
using System;
using TMPro;
using System.Collections;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    public event Action OnDamage = delegate { };

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
        anim = GetComponentInChildren<Animator>();
        coll = GetComponent<CapsuleCollider2D>();

        initialHealth = UnityEngine.Random.Range(minInitialHealth, maxInitialHealth);
        coll.isTrigger = false;
    }

    public int GetCurrentHealth()
    {
        return maxInitialHealth;
    }

    public void TakeDamage(int damageAmount, Vector2 point)
    {
        if(!IsDead)
            CurrentHealth -= damageAmount;


        if (CurrentHealth <= 0)
        {
            Die();
            CurrentHealth = 0;
            return;
        }

        if (floatingTextPrefab != null)
        {
            ShowDamage(damageAmount);
        }

        OnDamage?.Invoke();
    }

    //TODO: Pooling.
    private void ShowDamage(int damage)
    {
        var instance = Instantiate(floatingTextPrefab, transform.position + new Vector3(0,1.3f,0), Quaternion.identity, transform);
        instance.GetComponent<TextMeshPro>().text = damage.ToString();
    }

    void Die()
    {
        IsDead = true;
        anim.PerformCrossFade("Enemy_Die", 0f);
        Destroy(gameObject, 5.2f);
        coll.isTrigger  = true;
    }
}
