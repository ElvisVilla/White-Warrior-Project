using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(EnemyHealth))]
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
    [SerializeField] private EnemyInfo enemyInfo = new EnemyInfo();

    //StateMachine stateMachine;
    /*PatrolState patrolState;
    CombatState combatState;
    ChaseState chaseState;
    DeadState deadState;*/

    private void Awake()
    {
        Health = GetComponent < EnemyHealth>();
        Anim = GetComponent<Animator>();
        Body2D = GetComponent<Rigidbody2D>();

        Motor = new EnemyMovement(this);
        /*patrolState = new PatrolState(this);
        combatState = new CombatState(this);
        chaseState = new ChaseState(this);
        deadState = new DeadState(this);*/

        //stateMachine = new StateMachine();
        //stateMachine.SetState(patrolState);        

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
        //stateMachine.ExcecuteStateUpdate();
    }

    /*#region ActionMethods
    void PatrolAction()
    {
        stateMachine.SetState(patrolState);
    }

    void CombatAction()
    {
        stateMachine.SetState(combatState);
    }

    void ChaseAction()
    {
        stateMachine.SetState(chaseState);
    }

    void DeadAction()
    {
        stateMachine.SetState(deadState);
    }
    #endregion

    #region EventSubscriptions
    private void SetAllPatrolEvents()
    {
        patrolState.OnCombatState += CombatAction;
        patrolState.OnDeadState += DeadAction;
    }

    private void SetAllCombatEvents()
    {
        combatState.OnChaseState += ChaseAction;
        combatState.OnPatrolState += PatrolAction;
        combatState.OnDeadState += DeadAction;
    }

    private void SetAllChaseEvents()
    {
        chaseState.OnPatrolState += PatrolAction;
        chaseState.OnCombatState += CombatAction;
        chaseState.OnDeadState += DeadAction;
    }

    private void OnEnable()
    {
        //Damage Event.
        Health.OnDamage += Motor.OnKnockBack;

        //Patrol events.
        SetAllPatrolEvents();

        //Combat events.
        SetAllCombatEvents();

        //Chase events.
        SetAllChaseEvents();

    }
    #endregion

    #region UnsubscribeEvents
    private void UnsubscribePatrolEvents()
    {
        patrolState.OnCombatState -= CombatAction;
        patrolState.OnDeadState -= DeadAction;
    }

    public void UnsubscribeCombatevents()
    {
        combatState.OnChaseState -= ChaseAction;
        combatState.OnPatrolState -= PatrolAction;
        combatState.OnDeadState -= DeadAction;
    }

    public void UnsubscribeChaseEvents()
    {
        chaseState.OnPatrolState -= PatrolAction;
        chaseState.OnCombatState -= CombatAction;
        chaseState.OnDeadState -= DeadAction;
    }

    private void OnDisable()
    {
        Health.OnDamage -= Motor.OnKnockBack;

        //Patrol events.
        UnsubscribePatrolEvents();

        //Combat events.
        UnsubscribeCombatevents();

        //Chase events.
        UnsubscribeChaseEvents();
    }
    #endregion*/
}
