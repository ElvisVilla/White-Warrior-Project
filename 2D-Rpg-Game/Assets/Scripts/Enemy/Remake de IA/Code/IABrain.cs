using System;
using System.Collections.Generic;
using UnityEngine;

namespace Bissash.IA
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class IABrain : MonoBehaviour
    {
        [SerializeField] private StateType m_initialStateType = StateType.Patrol;
        [SerializeField] private FacingSide m_initialFacingSide = FacingSide.Right;

        public Vector2 waypointPosition = new Vector2();
        public int brainIndex = 0;
        public bool brainNofloor = false;
        public float distance = 0f;
        public float reachDistance = 0.5f;

        [Header("Combat Values")]
        public CombatMode combatMode = CombatMode.RegularCombat;
        public int brainDamageCount;
        public Timer timer = new Timer();

        [SerializeField] Sensor m_sensor = new Sensor();
        [SerializeField] private List<BaseState> m_stateList = null;
        [SerializeField] private Transform[] m_waypoints = null;
        private StateMachine stateMachine;
        [SerializeField] private IAMotor motor;

        #region Properties
        public Animator Anim { get; private set; }
        public Rigidbody2D Body2D { get; private set; }
        public IAMotor Motor => motor;
        public SimpleEnemyAttack Attack { get; private set; }
        public EnemyHealth Health { get; private set; }

        //Meat.
        public Sensor Sensor => m_sensor;
        public Vector2 Position => Body2D.position;
        public BaseState CurrentState => stateMachine.CurrentState;
        public FacingSide Side => m_initialFacingSide;

        #endregion

        void Awake()
        {
            motor = new IAMotor();
            motor.SetFacingSide(m_initialFacingSide, this);

            brainIndex = (int)m_initialFacingSide;
            waypointPosition = GetTargetWithIndex(brainIndex).position;

            stateMachine = new StateMachine();
            Sensor.Init(this);

            Anim = GetComponentInChildren<Animator>();
            Body2D = GetComponent<Rigidbody2D>();
            Attack = GetComponent<SimpleEnemyAttack>();
            Health = GetComponent<EnemyHealth>();

            SetState(m_initialStateType);
        }

        void FixedUpdate()
        {
            stateMachine.ExcecuteStateUpdate(this);
            Sensor.Update(this);
        }


        public Transform GetTargetWithIndex(int index)
        {
            return m_waypoints[index];
        }

        public int GetLenght()
        {
            return m_waypoints.Length;
        }

        /// <summary>
        /// Establece los estados en el statemachine.
        /// </summary>
        /// <param name="stateType"></param>
        public void SetState(StateType stateType)
        {
            stateMachine.SetInitialState(brain: this, 
            state: m_stateList.Find(state => state.StateType == stateType));
        }

        #region Events
        private void OnEnable()
        {
            if (!m_stateList.Exists(state => state.StateType == StateType.Combat))
                return;

            Combat combat = m_stateList.Find(state => state.StateType == StateType.Combat) as Combat;
            CombatBehaviour behaviour = combat.Behaviour as CombatBehaviour;
            Health.OnDamage += behaviour.OnDamage;

        }

        private void OnDisable()
        {
            if (!m_stateList.Exists(state => state.StateType == StateType.Combat))
                return;

            Combat combat = m_stateList.Find(state => state.StateType == StateType.Combat) as Combat;
            CombatBehaviour behaviour = combat.Behaviour as CombatBehaviour;
            Health.OnDamage -= behaviour.OnDamage;
        }
        #endregion

        private void OnDrawGizmos()
        {
            if(stateMachine == null)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawWireCube(transform.position, Sensor.Dimention);
            }
            else if(stateMachine != null)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawWireCube(transform.position, stateMachine.CurrentState.StateValues.SensorDimension);
            }
        }
    }
}

