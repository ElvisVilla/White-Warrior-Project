using UnityEngine;

public class  IACombatSystem : MonoBehaviour
{
    #region
    public PlayerHealth PlayerHealth { get; private set; }
    #endregion

    #region Variables
    private EnemyAttack enemyAttack;
    private GameObject playerObj;
    private Animator anim;

    [Header("Rango y frecuencia de ataque")]
    [SerializeField] private float timeAttack;
    [SerializeField] private float attackRange;

    [Header("Tiempo y distancia")]
    [SerializeField] private bool playerInRange;
    [SerializeField] private float timer;

    #endregion

    void Awake()
    {
        enemyAttack = GetComponentInChildren<EnemyAttack>();
        playerObj = GameObject.FindGameObjectWithTag("Player");
        PlayerHealth = playerObj.GetComponent<PlayerHealth>();
        anim = GetComponent<Animator>();

        playerInRange = false;
    }

    void FixedUpdate()
    {
        if (!PlayerHealth.IsDead) //Just update if player isn't dead.
        {
            TimerUpdate();
        }

        anim.SetFloat("TimeAttack", timer);
        anim.SetBool("InRange", playerInRange);

        if ((timer >= timeAttack) && playerInRange)
        {
            timer = 0f;             
        }

        if (enemyAttack.Impacted)
        {
            enemyAttack.Attack();
        }
    }

    private void TimerUpdate()
    {
        timer += Time.deltaTime;
        float distance = Vector2.Distance(playerObj.transform.position, transform.position);
        playerInRange = (distance < attackRange) ? true : false;
    }
}
