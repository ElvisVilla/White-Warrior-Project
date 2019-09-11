using UnityEngine;
using Bissash;

public class EnemyAttack : MonoBehaviour
{
    [TextArea(10,10)]
    [SerializeField] string developerInfo;
    #region Variables
    [SerializeField] private int _minDamage = 6;
    [SerializeField] private int _maxDamage = 12;
    [SerializeField] [Range(1f, 3f)] private float _minCoolDownAttack = 1f;
    [SerializeField] [Range(3f, 10)] private float _maxCoolDownAttack = 5f;
    [SerializeField] float _attackRange = 2f;
    [SerializeField] string _animationName = "Enemy_Attack";
    float _coolDown;
    int _damage;
    bool _playerInRange;
    Timer timerAttack;

    AnimationEvent anim;
    EnemyHealth ownHealth;
    Transform player;
    IDamageable playerHealth;

    //Cambios
    public float CoolDownSeconds => _coolDown;
    public float Range => _attackRange;
    #endregion

    void Awake()
    {
        _coolDown = Random.Range(_minCoolDownAttack, _maxCoolDownAttack);

        anim = GetComponentInChildren<AnimationEvent>();
        player = GameObject.FindGameObjectWithTag("Player").transform; //Debe cambiarse.
        playerHealth = player.GetComponent<PlayerHealth>(); //Debe quitarse de aqui.
        ownHealth = GetComponentInParent<EnemyHealth>();

        timerAttack = new Timer(_coolDown, _coolDown);
    }

    private void Update()
    {
        /*timerAttack.RunTimer();
        _playerInRange = (Vector2.Distance(player.position, transform.position) < _attackRange);

        if (timerAttack.TimeHasComplete() && _playerInRange)
        {
            PerformAttack();
        }*/
    }

    public void PerformAttack()
    {
        //Este codigo debe cambiar.
        if (!ownHealth.IsDead && playerHealth.CurrentHealth > 0)
        {
            _damage = Random.Range(_minDamage, _maxDamage);
            _coolDown = Random.Range(_minCoolDownAttack, _maxCoolDownAttack);
            //anim.PerformCrossFade(_animationName, 0f);
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
