using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour {

    //TargetGroup
    //CameraPrincipal
    [SerializeField] CinemachineVirtualCamera playerCam;
    [SerializeField] CinemachineVirtualCamera shakeCam;
    [SerializeField] CinemachineStateDrivenCamera stateDrivenCam;

    public float shakeTime = 0.2f;
    //Camera State Driven

	// Use this for initialization
	void Start ()
    {
        playerCam.Priority = 10;
        shakeCam.Priority = 9;
        stateDrivenCam.Priority = 9;
    }

    public IEnumerator OnAbilityEffect(AbilityType type)
    {
        switch (type)
        {
            case AbilityType.Melee:
                shakeCam.Priority = 11;

                yield return new WaitForSeconds(shakeTime);
                shakeCam.Priority = 9;
                break;
            case AbilityType.Magic:
                stateDrivenCam.Priority = 11;

                yield return new WaitForSeconds(2f);
                stateDrivenCam.Priority = 9;
                break;
        }
        //Mas opciones.
        yield return 0;
    }

    private void SetCamerasPriority(int value1 = 9, int value2 = 10, int value3 = 11)
    {
       
    }
}
