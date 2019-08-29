using Bissash.IA;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

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
    public List<Transform> wayPoints = null;
    
    private IABrain brain;
    private Vector2 orientation2D;
    private FacingSide facingSide = FacingSide.Right;

    #endregion

    public Vector2 Orientation2D => orientation2D;

    public void Init(IABrain iABrain)
    {
        brain = iABrain;
    }

   /// <summary>
   /// Movimiento por defecto bajo una velocidad dada.
   /// </summary>
   /// <param name="body2D"></param>
   /// <param name="speed"></param>
    public void MovementUpdate(Rigidbody2D body2D, float speed)
    {
        body2D.MovePosition(body2D.position + Vector2.right * speed * Time.deltaTime);
    }

    /// <summary>
    /// Custom movement.
    /// </summary>
    /// <param name="moveBehaviour"></param>
    public void MovementUpdate(Action moveBehaviour)
    {
        moveBehaviour?.Invoke();
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
        anim.SetFloat(parameterName, parameterValue);
    }

    public void MoveAnimation(Action action)
    {
        action?.Invoke();
    }

    /// <summary>
    /// La orientación/rotación del enemigo cambiará al establecer este metodo.
    /// </summary>
    /// <param name="side"></param>
    public void SetFacingSide(FacingSide side)
    {
        facingSide = side;
        if (IsFacingSide(facingSide) == false)
        {
            OrientationUpdate();
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

    private void OrientationUpdate()
    {
        orientation2D = IsFacingSide(FacingSide.Left) ? Vector3.up * 180f : Vector3.zero;
        brain.transform.localEulerAngles = orientation2D;

        //La variacion de la velocidad se podria hacer aca?, Tocara revisar.
    }

    //Variacion de la velocidad dependiendo la direccion del enemigo, es decir speed o -speed.
}
