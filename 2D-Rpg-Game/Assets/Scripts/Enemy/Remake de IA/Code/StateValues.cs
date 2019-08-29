using System;
using UnityEngine;

namespace Bissash.IA
{
    [Serializable]
    public class StateValues
    {
        [SerializeField] private float m_maxMovementSpeed = 0f;
        [SerializeField] private float m_minMovementSpeed = 0f;
        [SerializeField] private float m_movementSpeed = 0f;
        [SerializeField] private float m_range = 0f;
        [SerializeField] private float m_maxRange = 0f;
        [SerializeField] private float m_minRange = 0f;
        [SerializeField] private string m_animationName = null;
        [SerializeField] private StateType m_stateType = StateType.Indefined;
        public LayerMask whatIsFloor;

        public float MaxMovementSpeed => m_maxMovementSpeed;
        public float MinMovementSpeed => m_minMovementSpeed;
        public float MovementSpeed { get { return m_movementSpeed; } set { m_movementSpeed = value; } }
        public float Range => m_range; 
        public float MaxRange => m_maxRange;
        public float MinRange => m_minRange;
        public string AnimationName => m_animationName;
        public StateType StateType => m_stateType;

        public void InitRandomValues()
        {
            m_movementSpeed = UnityEngine.Random.Range(m_minMovementSpeed, m_maxMovementSpeed);
            m_range = UnityEngine.Random.Range(MinRange, MaxRange);
        }
    }
}

