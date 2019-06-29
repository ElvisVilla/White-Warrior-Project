using System.Collections.Generic;
using UnityEngine;

namespace Bissash.IA
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class IABrain : MonoBehaviour
    {
        [SerializeField] private StateType m_initialStateType = StateType.Indefined;
        [SerializeField] public SideMode facingSide;
        [SerializeField] private List<BaseState> m_stateList;
        [SerializeField] Sensor m_sensor;
        //[SerializeField] float distance;

        #region Properties
        public Animator Anim { get; private set; }
        public Rigidbody2D Body2D { get; private set; }
        public EnemyHealth Health { get; private set; }
        public StateMachine StateMachine { get; private set; }
        public Sensor Sensor => m_sensor;
        public Vector3 Position => Body2D.position;
        #endregion

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
        }

        void FixedUpdate()
        {
            StateMachine.ExcecuteStateUpdate(this);
            Sensor.Update(this);
        }

        public void SetState(StateType stateType)
        {
            StateMachine.SetInitialState(brain: this, 
            state: m_stateList.Find(state => state.StatesValues.StateType == stateType));
        }

        public SideMode GetFacingSide()
        {
            return facingSide;
        }

        public void SetFacingSide(SideMode side)
        {
            facingSide = side;
        }

        public bool IsFacingSide(SideMode side)
        {
            if (facingSide == side)
                return true;

            return false;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position, Sensor.Dimention);
        }
    }
}

