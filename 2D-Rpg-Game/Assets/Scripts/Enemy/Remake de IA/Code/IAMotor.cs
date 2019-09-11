using System;
using UnityEngine;
using Bissash;
using Bissash.IA;
using System.Collections;

public enum IAMotorStates
{
    Stun,
    Moving,
    NockBack,
}

[Serializable]
public class IAMotor
{
    #region Variables
    
    [SerializeField] private FacingSide facingSide = FacingSide.Right;
    [SerializeField] private float savedSpeed = 0f;
    [SerializeField] private float currentSpeed = 2f;
    [SerializeField] private Vector2 target;

    #endregion

    public void Init(IABrain iABrain)
    {
        savedSpeed = 0f;
        currentSpeed = 0f;
    }

   /// <summary>
   /// Movimiento por defecto bajo una velocidad dada.
   /// </summary>
   /// <param name="body2D"></param>
   /// <param name="speed"></param>
    public void Move(IABrain brain, Vector2 target , float speed)
    {
        target.y = 0f; //Colocamos y en cero para no avanzar hacia arriba ni hacia abajo.

        //Es necesario evitar la posicion en 0, dado que al multiplicar por 0 siempre nos dara cero y tendremos 0 en movimiento.
        /*if (target.x == 0)
            target.x = 0.1f;*/

        savedSpeed = speed;
        this.target = target;

        brain.Body2D.MovePosition(brain.Body2D.position + target.normalized * currentSpeed * Time.fixedDeltaTime);
        OrientationUpdate(target, brain.Position, brain);
    }

    /// <summary>
    /// Custom movement.
    /// </summary>
    /// <param name="customBehaviour"></param>
    public void Move(Action customBehaviour)
    {
        customBehaviour?.Invoke();
    }

    /// <summary>
    /// Se reproduce la animacion de movimiento por defecto si no se pasa una animacion mediante Action.
    /// </summary>
    /// <param name="anim"></param>
    /// <param name="parameterName"></param>
    /// <param name="parameterValue"></param>
    /// <param name="animBehaviour"></param>
    public void MoveAnimation(Animator anim, string parameterName, float parameterValue)
    {
        anim.PerformAnimation(parameterName, parameterValue);
    }

    public void SetSpeed(float speed)
    {
        savedSpeed = speed;
    }

    public void MoveAnimation(Action action)
    {
        action?.Invoke();
    }

    /// <summary>
    /// La orientación/rotación del enemigo cambiará al establecer este metodo.
    /// </summary>
    /// <param name="side"></param>
    public void SetFacingSide(FacingSide side, IABrain brain)
    {
        //Cambiamos la orientacion una sola vez.
        if (IsFacingSide(side) == false) 
        {
            facingSide = side;
            UpdateRotation(brain);
        }
    }

    public FacingSide GetFacingSide()
    {
        return facingSide;
    }

    public bool IsFacingSide(FacingSide side)
    {
        if (facingSide == side)
            return true;

        return false;
    }

    private void UpdateRotation(IABrain brain)
    {
        var dir = IsFacingSide(FacingSide.Left) ? Vector2.up * 180f : Vector2.zero;
        brain.transform.localEulerAngles = dir;
    }

    public Vector2 NockBackDirection()
    {
        return IsFacingSide(FacingSide.Left) ? -Vector2.one: Vector2.one;
    }

    void OrientationUpdate(Vector2 targetPosition, Vector2 actualPosition, IABrain brain)
    {
        if (actualPosition.x < targetPosition.x) //Si el target esta a nuestra derecha.
        {
            SetFacingSide(FacingSide.Right, brain); //Rotamos.

            /*Comprobamos que nuestra velocidad y la posicion del target no sean menor a cero ya que nos moveremos
            en reversa y no hacia el target al aplicar la velocidad en el metodo de movimiento.*/
            if(targetPosition.x * savedSpeed < 0)
            {
                //Asignamos la velocidad negada para poder movernos correctamente.
                currentSpeed = -savedSpeed;
                return;
            }

            //De otro modo nos moveremos con una velocidad positiva (hacia la derecha).
            currentSpeed = savedSpeed;
        }
        else if (actualPosition.x > targetPosition.x) //Si el target esta a nuestra izquierda.
        {
            SetFacingSide(FacingSide.Left, brain); //Rotamos.

            /*Comprobamos que nuestra velocidad y la posicion del target no sean mayor a cero ya que nos moveremos
           en reversa y no hacia el target al aplicar la velocidad en el metodo de movimiento.*/
            if (targetPosition.x * -savedSpeed > 0)
            {
                //Asignamos la velocidad positiva para poder movernos correctamente.
                currentSpeed = savedSpeed;
                return;
            }

            //De otro modo daremos una velocidad negativa (hacia la izquierda).
            currentSpeed = -savedSpeed;
        }
        else
        {
            //Mantenemos la rotacion.

            if (IsFacingSide(FacingSide.Left))
            {
                SetFacingSide(FacingSide.Left, brain);
            }
            else if (IsFacingSide(FacingSide.Right))
            {
                SetFacingSide(FacingSide.Right, brain);
            }
        }
    }
}
