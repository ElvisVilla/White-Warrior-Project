using UnityEngine;
using DG.Tweening;

public class EnemyHealth : MonoBehaviour, IHealth
{
    #region Properties
    //public float CurrentHealth { get { return health; } private set { health = value; } }
    public bool IsDead { get; set; }

    public float CurrentHealth
    {
        get
        {
            return health;
        }

        set
        {
            health = value;
        }
    }

    #endregion

    [Header("Set Health Bar")]
    [SerializeField] private int minInitialHealth = 39;
    [SerializeField] private int maxInitialHealth = 41;
    public float health;

    Animator anim;
    SpriteRenderer sprite;
    Rigidbody2D body2D;

    // Use this for initialization
    void Awake ()
    {
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        body2D = GetComponent<Rigidbody2D>();

        health = Random.Range(minInitialHealth, maxInitialHealth);
        CurrentHealth = health;
	}

    private void Update()
    {
        sprite.color = Color.white;
    }

    //Aplicar nockback.
    public void TakeDamage(float damageAmount)
    {
        //Mostrar el daño aplicado en texto flotante.
        health -= damageAmount;
        sprite.DOColor(Color.red, 0.1f);
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
