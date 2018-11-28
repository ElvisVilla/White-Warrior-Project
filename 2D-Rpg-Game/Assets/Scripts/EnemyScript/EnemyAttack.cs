using UnityEngine;

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
    float _timer;
    float _coolDown;
    float _damage;
    bool _playerInRange;

    Animator anim;
    EnemyHealth ownHealth;
    Transform player;
    IHealth playerHealth;
    #endregion

    void Awake()
    {
        _animationHash = Animator.StringToHash(_animationName);
        _coolDown = Random.Range(_minCoolDownAttack, _maxCoolDownAttack);

        anim = GetComponentInParent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = player.GetComponent<PlayerHealth>();
        ownHealth = GetComponentInParent<EnemyHealth>();
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        _playerInRange = (Vector2.Distance(player.position, transform.position) < _attackRange);

        if ((_timer > _coolDown) && _playerInRange)
        {
            Attack();
        }
    }

    public void Attack()
    {
        if (!ownHealth.IsDead && playerHealth.CurrentHealth > 0)
        {
            _damage = Random.Range(_minDamage, _maxDamage);
            _coolDown = Random.Range(_minCoolDownAttack, _maxCoolDownAttack);
            anim.CrossFade(_animationHash, 0f);
            _timer = 0f;
        }
    }

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerHealth = other.transform.GetComponent<PlayerHealth>();
            if(playerHealth != null)
            {
                playerHealth.TakeDamage(_damage);
            }
        }
    }
}
