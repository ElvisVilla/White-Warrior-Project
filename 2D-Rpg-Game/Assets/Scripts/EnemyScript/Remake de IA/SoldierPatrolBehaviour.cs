using UnityEngine;
using DG.Tweening;

namespace Bissash.IA
{
    [CreateAssetMenu(menuName = "IA/Behaviour/SoldierPatrolBehaviour")]
    public class SoldierPatrolBehaviour : BaseBehaviour
    {
        [SerializeField]Vector3 m_direction;
        [SerializeField]Vector3 initialPos;
        [SerializeField]Vector3 actualPos;
        [SerializeField]float distanceBetweenInitialPos;

        public override void Init(IABrain brain, PatrolState state)
        {
            OrientationUpdate(brain);
            initialPos = brain.transform.position;
            brain.initialPosition = initialPos; //Test
        }

        public override void BehaviourExcecute(IABrain brain, PatrolState state)
        {
            OrientationUpdate(brain);
            brain.Anim.SetFloat("Speed", state.StatesValues.MovementSpeed);

            var hitInfo = Physics2D.Raycast(brain.transform.GetChild(1).position, Vector2.down, 1f,
                state.StatesValues.whatIsFloor);

            if (brain.m_facingSide == SideMode.Left)
            {
                Move(brain, -state.StatesValues.MovementSpeed, hitInfo, SideMode.Right);
            }
            else if (brain.m_facingSide == SideMode.Right)
            {
                Move(brain, state.StatesValues.MovementSpeed, hitInfo, SideMode.Left);
            }

            actualPos = brain.transform.position;
            brain.actualPosition = actualPos; //Test
            distanceBetweenInitialPos = Vector3.Distance(initialPos, actualPos);
            brain.distance = distanceBetweenInitialPos; //Test*/
            brain.playerPos = brain.Sensor.TargetPosition;


            //EL estado de patrol debe moverse desde su punto inicial a 10mts dividido entre ambas
            //Direcciones, al llegar al limite de un lado debera cambiar direccion.
            //Tambien debemos comprobar que exista tierra para poder avanzar.
        }

        void OrientationUpdate(IABrain brain)
        {
            m_direction = (brain.m_facingSide == SideMode.Left) ? Vector3.up * -180f : Vector3.zero;
            brain.transform.localEulerAngles = m_direction;
        }

        void Move(IABrain brain, float speed, RaycastHit2D collisionInfo, SideMode side)
        {
            brain.Body2D.MovePosition(brain.Body2D.position +
                Vector2.right * speed * Time.fixedDeltaTime);

            if (collisionInfo.collider == null)
            {
                brain.m_facingSide = side;
            }
        }
    }
}

