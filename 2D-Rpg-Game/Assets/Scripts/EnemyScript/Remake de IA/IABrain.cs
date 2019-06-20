using System.Collections.Generic;
using UnityEngine;

namespace Bissash.IA
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class IABrain : MonoBehaviour
    {
        [SerializeField] private StateType m_initialStateType = StateType.Indefined;
        [SerializeField] public SideMode m_facingSide;
        [SerializeField] private List<BaseState> m_stateList;
        [SerializeField] Sensor m_sensor;
        public Vector3 initialPosition;
        public Vector3 actualPosition;
        public float distance;
        public Vector3 playerPos;

        #region Properties
        public Animator Anim { get; private set; }
        public Rigidbody2D Body2D { get; private set; }
        public EnemyHealth Health { get; private set; }
        public StateMachine StateMachine { get; private set; }
        public Sensor Sensor => m_sensor;
        public List<BaseState> StateList => m_stateList;
        public Vector3 Position => Body2D.position;
        #endregion

        // Start is called before the first frame update
        void Awake()
        {
            Anim = GetComponent<Animator>();
            Body2D = GetComponent<Rigidbody2D>();
            Health = GetComponent<EnemyHealth>();
            StateMachine = new StateMachine();
            Sensor.Init(this);
        }

        void Start()
        {
            SetState(m_initialStateType);
            //initialPosition = transform.position;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            StateMachine.ExcecuteStateUpdate(this);
            Sensor.Update(this);
            /*actualPosition = transform.position;
            distance = Vector3.Distance(initialPosition, actualPosition);*/
            //playerPos = Sensor.TargetPosition;
        }

        public void SetState(StateType stateType)
        {
            StateMachine.SetInitialState(brain: this, 
            state: m_stateList.Find(state => state.StatesValues.StateType == stateType));
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position, Sensor.sensorDimensions);
        }
    }
}

