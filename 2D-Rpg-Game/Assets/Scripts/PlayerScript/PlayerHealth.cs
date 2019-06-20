using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerHealth : MonoBehaviour, IHealth {

    #region Properties 
    public int CurrentHealth
    {
        get { return _currentHealth; }
        set
        {
            _currentHealth = value;
        }
    }

    public bool IsDead { get; private set; }
    #endregion

    #region Variables
    [Header("Set in Inspector")]
    [SerializeField] int maxHealth = 50; //50 by default
    [SerializeField] Slider slider;
    [SerializeField] int _currentHealth;

    public GameObject floatingTextPrefab;
    public TextMeshProUGUI healBarTextAmount;
    Player player;
    #endregion

    // Use this for initialization
    void Awake ()
    {
        player = GetComponent<Player>();
        CurrentHealth = maxHealth;
        slider.maxValue = maxHealth;
        slider.value = CurrentHealth;
        healBarTextAmount.text = HealthText();
    }

    //Debe aplicar nockback cuando es golpeado.
    public void TakeDamage (int damage)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, maxHealth);
        CurrentHealth -= damage;
        slider.DOValue(CurrentHealth, 0.7f).SetEase(Ease.Linear);
        StartCoroutine(player.Motor.OnHit());
        healBarTextAmount.text = HealthText();

        int animHitParameter = Random.Range(1, 3);
        switch(animHitParameter)
        {
            case 1:
                player.Anim.SetTrigger("TakeHit");
                break;
            case 2:
                player.Anim.SetTrigger("TakeDamage");
                break;
        }

        if (floatingTextPrefab != null)
        {
            ShowDamage(damage);
        }

        if (CurrentHealth <= 0 && !IsDead)
        {
            Die();
        }
    }

    private void ShowDamage(int damage)
    {
        var instance = Instantiate(floatingTextPrefab, transform.position + new Vector3(0,1,0), Quaternion.identity, transform);
        instance.GetComponent<TextMeshPro>().text = damage.ToString();
    }

    public void TakeHeal(int effect)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, maxHealth);
        CurrentHealth += effect;
        slider.DOValue(CurrentHealth, 0.7f).SetEase(Ease.Linear);
        healBarTextAmount.text = HealthText();
    }

    //Revisar referencias.
    public void Die()
    {
        IsDead = true;
        player.Anim.SetTrigger("Dying");
        Physics2D.IgnoreLayerCollision(12, 9);
        //Ejecutar escena GameOver.
    }

    string HealthText()
    {
        return $"{CurrentHealth} / {maxHealth}";
    }
}
