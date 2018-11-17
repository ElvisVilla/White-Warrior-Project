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

    [SerializeField] private int minInitialHealth = 39;
    [SerializeField] private int maxInitialHealth = 41;
    [SerializeField] GameObject healthbar;
    private Slider healthSlider;
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


        healthSlider = healthbar.GetComponentInChildren<Slider>();
        health = Random.Range(minInitialHealth, maxInitialHealth);
        healthSlider.maxValue = CurrentHealth;
        healthSlider.value = CurrentHealth;
	}

    private void Update()
    {
        sprite.color = Color.white;
    }

    //Aplicar nockback.
    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        healthSlider.value = CurrentHealth;
        body2D.AddForce(new Vector2(body2D.velocity.x * -2f, 0f));
        sprite.DOColor(Color.red, 0.1f);
    }

    //EnemyIA decide cuando ejecutar el metodo Dead(); basado en sus estados.
    public void Die()
    {
        IsDead = true;
        anim.SetTrigger(dieHashID);
        healthbar.SetActive(false);
        sprite.DOColor(new Color(1f, 1f, 1f, 0f), 5f);
        Destroy(gameObject, 5.2f);
    }
}
