using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour
{
    [Tooltip("Enemy Info es una clase que almacena toda la informacion que necesitan los estados para saber como actuar")]
    [SerializeField] private EnemyInfo enemyInfo;

    private StateMachine stateMachine;
    public EnemyHealth Health { get; private set; }
    public Rigidbody2D Body2D { get; private set; }
    public Animator Anim { get; private set; }
    public EnemyInfo EnemyInfo => enemyInfo;
    [SerializeField]private bool facing = true;
    public bool FacingRight { get { return facing; } set { facing = value; } }
    public float Speed { get; set; }

    PatrolState patrolState;
    CombatState combatState;
    ChaseState chaseState;
    DeadState deadState;

    private void Awake()
    {
        Health = GetComponent < EnemyHealth>();
        Anim = GetComponent<Animator>();
        Body2D = GetComponent<Rigidbody2D>();

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

    private void OnEnable()
    {

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
        chaseState.OnPatrolState -= PatrolAction;
        chaseState.OnCombatState -= CombatAction;
        chaseState.OnDeadState -= DeadAction;

        combatState.OnChaseState -= ChaseAction;
        combatState.OnPatrolState -= PatrolAction;
        combatState.OnDeadState -= DeadAction;

        patrolState.OnCombatState -= CombatAction;
        patrolState.OnDeadState -= DeadAction;
    }

    private void FixedUpdate()
    {
        stateMachine.ExcecuteStateUpdate();
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
