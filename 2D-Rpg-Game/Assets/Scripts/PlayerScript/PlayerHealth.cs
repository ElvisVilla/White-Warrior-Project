using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour, IHealth {

    #region Properties 
    public float CurrentHealth
    {
        get { return _currentHealth; }
        set
        {
            _currentHealth = Mathf.Clamp(_currentHealth, 0, 100);
            _currentHealth = value;
        }
    }

    public bool IsDead { get; private set; }
    #endregion

    #region Variables
    [Header("Set in Inspector")]
    [SerializeField] float initialHealth = 100;
    [SerializeField] Slider healthBar;
    float _currentHealth;

    public GameObject floatingTextPrefab;
    Player player;
    #endregion

    // Use this for initialization
    void Awake ()
    {
        player = GetComponent<Player>();

        _currentHealth = initialHealth;
        healthBar.maxValue = initialHealth;
        healthBar.value = _currentHealth;
	}

    //Debe aplicar nockback cuando es golpeado.
    public void TakeDamage (float damage)
    {
        CurrentHealth -= damage;
        healthBar.value = _currentHealth;
        StartCoroutine(player.Motor.OnHit());

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

        if (_currentHealth <= 0 && !IsDead)
        {
            Die();
        }
    }

    private void ShowDamage(float damage)
    {
        var instance = Instantiate(floatingTextPrefab, transform.position + new Vector3(0,1,0), Quaternion.identity, transform);
        instance.GetComponent<TextMeshPro>().text = damage.ToString();
    }

    public void TakeHeal(float effect)
    {
        CurrentHealth += effect;
        healthBar.value = CurrentHealth;
    }

    //De momento no la llamamos por fuera de la clase.
    public void Die()
    {
        IsDead = true;
        player.Anim.SetTrigger("Dying");
        Physics2D.IgnoreLayerCollision(12, 9);
        //Ejecutar escena GameOver.
    }
}
