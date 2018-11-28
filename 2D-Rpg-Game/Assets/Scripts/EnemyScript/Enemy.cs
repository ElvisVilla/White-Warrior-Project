using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour
{
    public EnemyMovement Motor { get; private set; }
    public EnemyHealth Health { get; private set; }
    public Rigidbody2D Body2D { get; private set; }
    public Animator Anim { get; private set; }
    public EnemyInfo EnemyInfo => enemyInfo;
    public bool FacingRight { get { return facingRight; } set { facingRight = value; } }
    public float Speed { get; set; }

    [SerializeField]private bool facingRight = true;
    [SerializeField] private EnemyInfo enemyInfo;

    StateMachine stateMachine;
    PatrolState patrolState;
    CombatState combatState;
    ChaseState chaseState;
    DeadState deadState;

    private void Awake()
    {
        Health = GetComponent < EnemyHealth>();
        Anim = GetComponent<Animator>();
        Body2D = GetComponent<Rigidbody2D>();

        Motor = new EnemyMovement(this);
        patrolState = new PatrolState(this);
        combatState = new CombatState(this);
        chaseState = new ChaseState(this);
        deadState = new DeadState(this);

        stateMachine = new StateMachine();
        stateMachine.ChangeState(patrolState);        

        if(FacingRight)
        {
            transform.eulerAngles = Vector2.zero;
        }else
        {
            transform.eulerAngles = new Vector2(0, 180);
        }
    }

    private void FixedUpdate()
    {
        stateMachine.ExcecuteStateUpdate();
    }

    private void OnEnable()
    {
        //Damage Event.
        Health.OnDamage += Motor.OnKnockBack;

        //Patrol transitions.
        patrolState.OnCombatState += CombatAction;
        patrolState.OnDeadState += DeadAction;

        //Combat transitions.
        combatState.OnChaseState += ChaseAction;
        combatState.OnPatrolState += PatrolAction;
        combatState.OnDeadState += DeadAction;

        //Chase transitions.
        chaseState.OnPatrolState += PatrolAction;
        chaseState.OnCombatState += CombatAction;
        chaseState.OnDeadState += DeadAction;
    }

    private void OnDisable()
    {
        Health.OnDamage -= Motor.OnKnockBack;

        chaseState.OnPatrolState -= PatrolAction;
        chaseState.OnCombatState -= CombatAction;
        chaseState.OnDeadState -= DeadAction;

        combatState.OnChaseState -= ChaseAction;
        combatState.OnPatrolState -= PatrolAction;
        combatState.OnDeadState -= DeadAction;

        patrolState.OnCombatState -= CombatAction;
        patrolState.OnDeadState -= DeadAction;
    }

    //Eventos.
    #region EventMethods 

    void PatrolAction() => stateMachine.ChangeState(patrolState);

    void CombatAction() => stateMachine.ChangeState(combatState);

    void ChaseAction() => stateMachine.ChangeState(chaseState);

    void DeadAction() => stateMachine.ChangeState(deadState);
    
    #endregion

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, enemyInfo.patrolRange);
    }
}
