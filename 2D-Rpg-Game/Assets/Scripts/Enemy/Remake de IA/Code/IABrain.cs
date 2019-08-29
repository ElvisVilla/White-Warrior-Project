using System.Collections.Generic;
using UnityEngine;

namespace Bissash.IA
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class IABrain : MonoBehaviour
    {
        [SerializeField] private StateType m_initialStateType = StateType.Indefined;
        [SerializeField] private FacingSide m_facingSide = FacingSide.Right;
        [SerializeField] private List<BaseState> m_stateList = null;
        [SerializeField] Sensor m_sensor;
        [SerializeField] IAMotor m_motor;

        #region Properties
        public Animator Anim { get; private set; }
        public Rigidbody2D Body2D { get; private set; }

        //Meat.
        public StateMachine StateMachine { get; private set; }
        public Sensor Sensor => m_sensor;
        public IAMotor Motor => m_motor;


        public Vector2 Position => Body2D.position;
        #endregion

        private void Awake()
        {
            m_sensor = new Sensor();
            m_motor = new IAMotor();
            m_motor.SetFacingSide(m_facingSide);

            StateMachine = new StateMachine();
            Sensor.Init(this);

            Anim = GetComponent<Animator>();
            Body2D = GetComponent<Rigidbody2D>();
        }

        void Start()
        {
            SetState(m_initialStateType);
        }

        void FixedUpdate()
        {
            StateMachine.ExcecuteStateUpdate(this);
            Sensor.Update(this);
        }

        public void SetState(StateType stateType)
        {
            StateMachine.SetInitialState(brain: this, 
            state: m_stateList.Find(state => state.StateValues.StateType == stateType));
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(transform.position, Sensor.Dimention);
        }
    }
}

