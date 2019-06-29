using UnityEngine;
using DG.Tweening;
using Bissash.Util;

namespace Bissash.IA
{
    [CreateAssetMenu(menuName = "IA/Behaviour/SoldierPatrolBehaviour")]
    public class SoldierPatrolBehaviour : BaseBehaviour
    {
        [Header("Debug values")]
        [SerializeField]Vector3 m_direction;
        [SerializeField]Vector3 initialPos;
        [SerializeField]float initialPositionDistance;
        Timer waitTimer;

        //Variables for wait behaviour.
        float initialSpeed;
        Timer timer;
        bool canMove;

        public override void Init(IABrain brain, PatrolState state)
        {
            OrientationUpdate(brain, state);
            initialPos = brain.transform.position;
            initialSpeed = state.StatesValues.MovementSpeed;

            timer = new Timer();
            waitTimer = new Timer();
        }

        public override void BehaviourExcecute(IABrain brain, PatrolState state)
        {
            Move(brain, state);
            initialPositionDistance = brain.Sensor.MeasureDistance(initialPos, brain.Position);
        }

        void OrientationUpdate(IABrain brain, PatrolState state)
        {
            m_direction = brain.IsFacingSide(SideMode.Left) ? Vector3.up * 180f : Vector3.zero;
            brain.transform.localEulerAngles = m_direction;
        }

        void PatrolRotation(IABrain brain, PatrolState state, float waitForRotation)
        {
            //this avoid rotation always that is in max distance between initial position.
            bool canRotate = timer.IsTimeCompleted(waitForRotation, TimeMode.LessEqualsMode);

            if (initialPositionDistance > state.StatesValues.Range && brain.IsFacingSide(SideMode.Left) && canRotate)
            {
                brain.SetFacingSide(SideMode.Right);
            }
            else if (initialPositionDistance > state.StatesValues.Range && brain.IsFacingSide(SideMode.Right) && canRotate)
            {
                brain.SetFacingSide(SideMode.Left);
            }
        }

        void Move(IABrain brain, PatrolState state)
        {
            OrientationUpdate(brain, state);
            PatrolRotation(brain, state, 1f);

            float speed = brain.IsFacingSide(SideMode.Right) ? state.StatesValues.MovementSpeed : -state.StatesValues.MovementSpeed;
            brain.Anim.SetFloat("Speed", speed);
            brain.Body2D.MovePosition(brain.Body2D.position + Vector2.right * speed * Time.fixedDeltaTime);
            brain.Sensor.DetectedEvent(RotateWhenNoFloor, brain, state.StatesValues.whatIsFloor);

            //Testeandose aun.
            waitTimer.OnTimerEvent(RandomWait, state, Random.Range(2f, 8f), TimeMode.LessEqualsMode); 
        }

        void RotateWhenNoFloor(IABrain brain)
        {
            if (brain.IsFacingSide(SideMode.Right))
                brain.SetFacingSide(SideMode.Left);
            else if (brain.IsFacingSide(SideMode.Left))
                brain.SetFacingSide(SideMode.Right);
        }

        //Falta trabajarlo un poco mas.
        void RandomWait(PatrolState state)
        {
            canMove = !canMove;
            state.StatesValues.MovementSpeed = (canMove) ? initialSpeed : 0f;
        }
    }
}

