using System.Collections;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{

    [SerializeField] CinemachineVirtualCamera playerCam = null;
    [SerializeField] CinemachineVirtualCamera healCam = null;
    private CinemachineBasicMultiChannelPerlin noise;

    public float shakeTime = 0.2f;
    public float amplitud;
    public float frecuency;

	void Start ()
    {
        playerCam.Priority = 10;
        healCam.Priority = 9;
        noise = playerCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        ResetCameraShake();
    }

    //Metodo que establesca el camera group para los eventos de combate.

    public void OnAbilityCameraEffect(Ability ability)
    {
        IEnumerator OnAbilityEffect(Ability ab)
        {

            if (ab.AbilityType == AbilityType.AreaMelee || ab.AbilityType == AbilityType.Melee)
            {
                SetShakeValues();
                yield return new WaitForSeconds(shakeTime);
                ResetCameraShake();
            }
            else if (ab.AbilityType == AbilityType.Magic)
            {
                healCam.Priority = 11;
                yield return new WaitForSeconds(0.7f);
                healCam.Priority = 9;
            }

            yield return 0;
        }

        StartCoroutine(OnAbilityEffect(ability));
    }

    private void SetShakeValues()
    {
        noise.m_AmplitudeGain = amplitud;
        noise.m_FrequencyGain = frecuency;
    }

    private void ResetCameraShake()
    {
        noise.m_AmplitudeGain = 0f;
        noise.m_FrequencyGain = 0f;
    }
}
