﻿using UnityEngine;
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

    private Animator anim;
    private Movement movement;
    #endregion

    // Use this for initialization
    void Awake ()
    {
        anim = GetComponent<Animator>();
        movement = GetComponent<Player>().Motor;

        _currentHealth = initialHealth;
        healthBar.maxValue = initialHealth;
        healthBar.value = _currentHealth;
	}

    //Debe aplicar nockback cuando es golpeado.
    public void TakeDamage (float damage)
    {
        CurrentHealth -= damage;
        healthBar.value = _currentHealth;
        StartCoroutine(movement.OnHit());


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
        
        if(_currentHealth <= 0 && !IsDead)
        {
            Die();
        }
    }

    //De momento no la llamamos por fuera de la clase.
    public void Die()
    {
        IsDead = true;
        anim.SetTrigger("Dying");
        //Ejecutar escena GameOver.
    }
}