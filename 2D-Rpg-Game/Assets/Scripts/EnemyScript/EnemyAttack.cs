using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    #region Properties 
    public bool Impacted
    {
        get { return _impacted; }
        set { _impacted = value; }
    }
    #endregion

    #region Variables
    [SerializeField] private int _minDamage = 6;
    [SerializeField] private int _maxDamage = 12;
    private bool _impacted = false;

    private IACombatSystem combatSystem;
    #endregion

    void Awake ()
    {
        combatSystem = GetComponentInParent<IACombatSystem>();
    }

    public void Attack ()
    {
        int damage = Random.Range(_minDamage, _maxDamage);
        combatSystem.PlayerHealth.TakeDamage(damage);
        _impacted = false;
    }

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _impacted = true;
        }
    }
}
