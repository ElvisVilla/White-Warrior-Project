using System.Collections.Generic;
using UnityEngine;

namespace Bissash.IA
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class IABrain : MonoBehaviour
    {
        [SerializeField] private StateType m_initialStateType = StateType.Indefined;
        [SerializeField] private FacingMode m_facingMode = FacingMode.Right;
        [SerializeField] private List<BaseState> m_stateList;

        #region Properties
        public Animator Anim { get; private set; }
        public Rigidbody2D Body2D { get; private set; }

        public StateMachine StateMachine { get; private set; }
        public FacingMode FacingMode => m_facingMode;
        public List<BaseState> StateList => m_stateList; 
        #endregion

        // Start is called before the first frame update
        void Awake()
        {
            Anim = GetComponent<Animator>();
            Body2D = GetComponent<Rigidbody2D>();
            StateMachine = new StateMachine();
        }

        void Start()
        {
            ChangeState(m_initialStateType);
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            StateMachine.ExcecuteStateUpdate(this);
        }

        public void ChangeState(StateType stateType)
        {
            StateMachine.SetInitialState(brain: this, state: m_stateList.Find(state => state.StatesValues.StateType == stateType));
        }
    }
}

