using System;
using UnityEngine;

namespace Bissash.IA
{
    [Serializable]
    public class StateValues
    {
        [SerializeField] private float m_maxMovementSpeed;
        [SerializeField] private float m_minMovementSpeed;
        [SerializeField] private float m_movementSpeed;
        [SerializeField] private float m_maxRange;
        [SerializeField] private float m_minRange;
        [SerializeField] private string m_animationName;
        [SerializeField] private StateType m_stateType;
        public LayerMask whatIsFloor;

        public float MaxMovementSpeed => m_maxMovementSpeed;
        public float MinMovementSpeed => m_minMovementSpeed;
        public float MovementSpeed => m_movementSpeed = UnityEngine.Random.Range(m_minMovementSpeed, m_maxMovementSpeed);
        public float MaxRange => m_maxRange;
        public float MinRange => m_minRange;
        public string AnimationName => m_animationName;
        public StateType StateType => m_stateType;
    }
}

