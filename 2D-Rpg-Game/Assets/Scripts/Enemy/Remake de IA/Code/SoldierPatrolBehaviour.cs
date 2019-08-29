using UnityEngine;
using Bissash.Util;

namespace Bissash.IA
{
    [CreateAssetMenu(menuName = "IA/Behaviour/SoldierPatrolBehaviour")]
    public class SoldierPatrolBehaviour : BaseBehaviour
    {
        public Transform[] waypoints = null;
        public Transform waypointTarget = null;
        public IAMotor motor;
        public StateValues stateValues;
        public int index;
        public float reachedDistance = 1f;

        [Header("Debug values")]
        [SerializeField] Vector3 m_direction;
        [SerializeField] Vector3 initialPos;
        [SerializeField] float distance;
        Timer timer;

        public override void Init(IABrain brain, PatrolState state)
        {
            initialPos = brain.Position;
            timer = new Timer();

            motor = brain.Motor;
            stateValues = state.StateValues;
        }

        public override void BehaviourExcecute(IABrain brain)
        {
            Move(brain);
        }

        //Por Revisar.
        void PatrolRotation(IABrain brain, float waitForRotation)
        {
            //OnTimerEvent crea un lapso de tiempo en el que la IA no podra rotar llegados al limite, esto evita que se haga un bucle
            //de rotacion al estar a maxima distancia. La IA rotara y el timer estara listo para volver a usarse.
            //distance = brain.Sensor.MeasureDistance(initialPos, brain.Position);

            /*timer.OnTimerEvent(() => {
                if (distance > state.StatesValues.Range && brain.IsFacingSide(SideMode.Left))
                {
                    brain.SetFacingSide(SideMode.Right);
                    timer.ResetTimer();
                }
                else if (distance > state.StatesValues.Range && brain.IsFacingSide(SideMode.Right))
                {
                    brain.SetFacingSide(SideMode.Left);
                    timer.ResetTimer();
                }
            }, seconds: waitForRotation, resetMode: ResetMode.Manual);*/


            //POSIBLE ALTERNATIVA.

            //La idea es cambiar la ruta del enemigo al llegar a cada waypoin
            //Lo que haremos es que el enemigo en su patrolBehaviour se dirija hacia un target,
            //Al llegar al target, se avanzara el indice para actualizar la lista de targets, tambien se definira
            //La direccion de ese target y modificaremos la orientacion basandonos en el componente x del target en cuestion.

            //Cosas a tener encuenta:
            //El enemigo debe reconocer cuando se queda sin camino para recorrer hacia su target, en ese momento
            //Avanzaremos en la lista de targets tal como si el lo hubiese alcanzado.

            //Posibles mejoras: El enemigo puede entrar en un evento de comportamiento, por ejemplo detenerse unos 3 o 5s 
            //a descanzar o interactuar con algun elemento del entorno.
        }

        //Por Revisar.
        void WaypointsUpdate(float distance)
        {
            if(distance <= reachedDistance)
            {
                index++;
                waypointTarget = waypoints[index];

                if (waypoints.Length >= index)
                    index = 0;
            }
        }

        //Por Revisar.
        float ComputedSpeed(IABrain brain)
        {
            if(waypointTarget.position.x > brain.Position.x)
            {
                return stateValues.MovementSpeed / 5;        
            } else
            {
                return -stateValues.MovementSpeed / 5;
            }
        }


        void Move(IABrain brain)
        {
            float speed = ComputedSpeed(brain);
            motor.MoveAnimation(brain.Anim, "Speed", 2f/*speed*/);
            motor.MovementUpdate(brain.Body2D, speed);
            brain.Sensor.OnDetect(OnNoFloor, brain, stateValues.whatIsFloor);

            //Por Revisar
            distance = brain.Sensor.Distance(waypointTarget.position, brain.Position);

            //Antigua.
            //float speed = brain.IsFacingSide(SideMode.Right) ?
            //state.StatesValues.MovementSpeed : -state.StatesValues.MovementSpeed;
            //brain.Body2D.MovePosition(brain.Body2D.position + Vector2.right * speed * Time.fixedDeltaTime);

            //Con los waypoints, pero debe cambiarse.
            //brain.Body2D.MovePosition(brain.Position + new Vector2(waypointTarget.position.x, 0) * speed * Time.deltaTime);

        }

        void OnNoFloor(IABrain brain)
        {
            var facingSide = motor.IsFacingSide(FacingSide.Right) ? FacingSide.Left : FacingSide.Right;
            motor.SetFacingSide(facingSide);
        }
    }
}

