using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    #region Variables
    [SerializeField] private int _minDamage = 6;
    [SerializeField] private int _maxDamage = 12;
    [SerializeField] [Range(1f, 3f)] private float _minCoolDownAttack;
    [SerializeField] [Range(3f, 10)] private float _maxCoolDownAttack;
    [SerializeField] float _attackRange;
    [SerializeField] string _animationName;
    int _animationHash;
    float _timer;
    float _coolDown;
    float _damage;
    bool _playerInRange;

    Animator anim;
    Transform player;
    PlayerHealth playerhealth;
    EnemyHealth ownHealth;
    #endregion

    void Awake ()
    {
        _animationHash = Animator.StringToHash(_animationName);
        _coolDown = Random.Range(_minCoolDownAttack, _maxCoolDownAttack);
        _damage = Random.Range(_minDamage, _maxDamage);

        anim = GetComponentInParent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerhealth = player.GetComponent<PlayerHealth>();
        ownHealth = GetComponentInParent<EnemyHealth>();
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        _playerInRange = (Vector2.Distance(player.position, transform.position) < _attackRange);

        if((_timer > _coolDown) && _playerInRange)
        {
            Attack();
        }
    }

    public void Attack()
    {
        if (!ownHealth.IsDead)
        {
            float damage = Random.Range(_minDamage, _maxDamage);
            anim.CrossFade(_animationHash, 0f);
            _timer = 0f;
        }
    }

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            IHealth health = other.transform.GetComponent<IHealth>();
            if(health != null)
            {
                health.TakeDamage(_damage);
            }
        }
    }
}
