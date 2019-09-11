using UnityEngine;

namespace Bissash.IA
{
    [System.Serializable]
    public class StateValues
    {
        [SerializeField] private float m_maxMovementSpeed = 0f;
        [SerializeField] private float m_minMovementSpeed = 0f;
        [SerializeField] private float m_maxRange = 0f;
        [SerializeField] private float m_minRange = 0f;
        [SerializeField] private Vector2 m_sensorDimension = Vector2.zero; 
        [SerializeField] private string animParameterName = null;
        [SerializeField] private LayerMask whatIsFloor = new LayerMask();

        public float MovementSpeed { get; set; }
        public float Range { get; set; }
        public float MinMoveSpeed => m_minMovementSpeed;
        public Vector2 SensorDimension => m_sensorDimension;
        public string AnimationName => animParameterName;
        public LayerMask WhatIsFloor => whatIsFloor;

        public void InitRandomValues()
        {
            MovementSpeed = Random.Range(m_minMovementSpeed, m_maxMovementSpeed);
            Range = Random.Range(m_minRange, m_maxRange);
        }
    }
}

