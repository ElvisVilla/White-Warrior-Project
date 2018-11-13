using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

public class EnemyHealth : MonoBehaviour, IHealth
{
    #region Properties
    public float CurrentHealth { get { return health; } }
    public bool IsDead { get; set; }
    #endregion

    [Header("Set Health Bar")]
    [SerializeField] private Slider healthBar;
    [SerializeField] private int minInitialHealth = 39;
    [SerializeField] private int maxInitialHealth = 41;
    public float health;

    int dieHashID = Animator.StringToHash("Dead");

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
        healthBar.maxValue = CurrentHealth;
        healthBar.value = CurrentHealth;
	}

    //Aplicar nockback.
    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;//Mathf.Lerp(0f, damageAmount, 2f * Time.deltaTime);
        healthBar.value = CurrentHealth;
        body2D.AddForce(new Vector2(body2D.velocity.x * -2f, 0f));

        //EnemyIA decide cuando ejecutar el metodo Dead(); basado en sus estados.
    }

    public void Die()
    {
        IsDead = true;
        anim.SetTrigger(dieHashID);
        //sprite.color = Color.Lerp(sprite.color, new Color(1f, 1f, 1f, 0f), 0.5f * Time.deltaTime);
        sprite.DOColor(new Color(1f, 1f, 1f, 0), 10f);
        Physics2D.IgnoreLayerCollision(12, 9);
        Destroy(gameObject, 9f);
    }
}
