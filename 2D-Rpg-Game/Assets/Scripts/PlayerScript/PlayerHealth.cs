using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

    #region Properties 
    public int CurrentHealth
    {
        get { return currentHealth; }
        set
        {
            currentHealth = Mathf.Clamp(currentHealth, 0, 100);
            currentHealth = value;
        }
    }

    public bool IsDead { get; private set; }
    #endregion

    #region Variables
    [Header("Set in Inspector")]
    [SerializeField] int initialHealth = 100;
    [SerializeField] Slider healthBar;
    int currentHealth;    

    private PlayerController playerController;
    private PlayerCombatSystem playerCombatSystem;
    private Animator anim;
    #endregion

    // Use this for initialization
    void Awake ()
    {
        anim = GetComponentInChildren<Animator>();
        playerController = GetComponent<PlayerController>();
        playerCombatSystem = GetComponentInChildren<PlayerCombatSystem>();

        currentHealth = initialHealth;
        healthBar.value = currentHealth;
	}

    //Debe aplicar nockback cuando es golpeado.
    public void TakeDamage (int damage)
    {
        CurrentHealth -= damage;

        int animHitParameter = Random.Range(1, 3);
        switch(animHitParameter)
        {
            case 1:
                anim.SetTrigger("TakeHit");
                break;
            case 2:
                anim.SetTrigger("TakeDamage");
                break;
        }
        
        healthBar.value = currentHealth;
        if(currentHealth <= 0 && !IsDead)
        {
            Death();
        }
    }

    void Death ()
    {
        IsDead = true;
        anim.SetTrigger("Dying");

        playerCombatSystem.enabled = false;
        playerController.enabled = false;
        //Ejecutar escena GameOver.

    }
}
