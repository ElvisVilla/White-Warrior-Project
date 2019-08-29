using UnityEngine;
using Bissash.Util;

public class EnemyAttack : MonoBehaviour
{
    #region Variables
    [SerializeField] private int _minDamage = 6;
    [SerializeField] private int _maxDamage = 12;
    [SerializeField] [Range(1f, 3f)] private float _minCoolDownAttack = 1f;
    [SerializeField] [Range(3f, 10)] private float _maxCoolDownAttack = 5f;
    [SerializeField] float _attackRange = 2f;
    [SerializeField] string _animationName = "Enemy_Attack";
    int _animationHash;
    float _coolDown;
    int _damage;
    bool _playerInRange;
    Timer timerAttack;

    Animator anim;
    EnemyHealth ownHealth;
    Transform player;
    IDamageable playerHealth;
    #endregion

    void Awake()
    {
        _animationHash = Animator.StringToHash(_animationName);
        _coolDown = Random.Range(_minCoolDownAttack, _maxCoolDownAttack);

        anim = GetComponentInParent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform; //Debe cambiarse.
        playerHealth = player.GetComponent<PlayerHealth>(); //Debe quitarse de aqui.
        ownHealth = GetComponentInParent<EnemyHealth>();

        timerAttack = new Timer(_coolDown, _coolDown);
    }

    private void Update()
    {
        timerAttack.RunTimer();
        _playerInRange = (Vector2.Distance(player.position, transform.position) < _attackRange);

        if (timerAttack.TimeHasComplete() && _playerInRange)
        {
            Attack();
        }
    }

    public void Attack()
    {
        //Este codigo debe cambiar.
        if (!ownHealth.IsDead && playerHealth.CurrentHealth > 0)
        {
            _damage = Random.Range(_minDamage, _maxDamage);
            _coolDown = Random.Range(_minCoolDownAttack, _maxCoolDownAttack);
            anim.CrossFade(_animationHash, 0f);
            timerAttack.ResetTimer();
        }
    }

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerHealth = other.transform.GetComponent<PlayerHealth>();
            if(playerHealth != null)
            {
                playerHealth.TakeDamage(_damage, Vector2.zero);
            }
        }
    }
}
