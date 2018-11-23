using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealth
{
    float CurrentHealth { get; set; }
    void TakeDamage(float damageAmount);
    void Die();

}
