using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
class EnemyIA : MonoBehaviour
{
    StateMachine stateMachine;
    EnemyHealth health;
    Rigidbody2D body2D;
    Animator anim;

    [Tooltip("Enemy Info es una clase que almacena toda la informacion que necesitan los estados para saber como actuar")]
    [SerializeField] private EnemyInfo enemyInfo;

    private void Awake()
    {
        health = GetComponent < EnemyHealth>();
        anim = GetComponent<Animator>();
        body2D = GetComponent<Rigidbody2D>();

        stateMachine = new StateMachine();
        stateMachine.ChangeState(new PatrolState(enemyInfo, gameObject, body2D, anim, health));
    }

    private void OnEnable()
    {
        //Chase transitions.
        ChaseState.OnPatrolState += PatrolAction;
        ChaseState.OnCombatState += CombatAction;
        ChaseState.OnDeadState += DeadAction;

        //Combat transitions.
        CombatState.OnChaseState += ChaseAction;
        CombatState.OnPatrolState += PatrolAction;
        CombatState.OnDeadState += DeadAction;

        //Patrol transitions.
        PatrolState.OnCombatState += CombatAction;
        PatrolState.OnDeadState += DeadAction;
    }

    private void OnDisable()
    {
        ChaseState.OnPatrolState -= PatrolAction;
        ChaseState.OnCombatState -= CombatAction;
        ChaseState.OnDeadState -= DeadAction;

        CombatState.OnChaseState -= ChaseAction;
        CombatState.OnPatrolState -= PatrolAction;
        CombatState.OnDeadState -= DeadAction;

        PatrolState.OnCombatState -= CombatAction;
        PatrolState.OnDeadState -= DeadAction;
    }

    private void FixedUpdate()
    {
        stateMachine.ExcecuteStateUpdate();
    }

    //Eventos.
    #region EventMethods 

    void CombatAction()
    {
        stateMachine.ChangeState(new CombatState(enemyInfo, gameObject, body2D, anim, health));
    }

    void ChaseAction()
    {
        stateMachine.ChangeState(new ChaseState(enemyInfo, gameObject, body2D, anim, health));
    }

    void PatrolAction ()
    {
        stateMachine.ChangeState(new PatrolState(enemyInfo, gameObject, body2D, anim, health));
    }

    void DeadAction ()
    {
        stateMachine.ChangeState(new DeadState(enemyInfo, gameObject, body2D, anim, health));
    }
    
    #endregion

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, enemyInfo.patrolRange);
    }
}
