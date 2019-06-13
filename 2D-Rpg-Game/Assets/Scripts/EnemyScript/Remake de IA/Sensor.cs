using UnityEngine;

namespace Bissash.IA
{
    [System.Serializable]
    public class Sensor
    {
        private Vector2 sensorDimensions;

        public Collider2D Detected(IABrain brain)
        {
           return Physics2D.OverlapBox(brain.transform.position, sensorDimensions, 0);
        }
    }
}