using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealth
{
    int CurrentHealth { get; set; }
    void TakeDamage(int damageAmount);
    void Die();

}
