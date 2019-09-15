using System.Collections;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{

    [SerializeField] CinemachineVirtualCamera playerCam = null;
    [SerializeField] CinemachineVirtualCamera healCam = null;
    [SerializeField] CinemachineVirtualCamera dialogueCam = null;
    private CinemachineBasicMultiChannelPerlin noiseModule;

    public float shakeTime = 0.2f;
    public float amplitud;
    public float frecuency;

	void Start ()
    {
        playerCam.Priority = 10;
        healCam.Priority = 9;
        noiseModule = playerCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        ResetCameraShake();
    }

    //Metodo que establesca el camera group para los eventos de combate.
    public void SetDialogueCamera()
    {
        dialogueCam.Priority = 11;
    }

    public void ResetDialogueCamera()
    {
        dialogueCam.Priority = 9;
    }

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

    public void SimpleShake()
    {
        IEnumerator Shake()
        {
            SetShakeValues();
            yield return new WaitForSeconds(shakeTime);
            ResetCameraShake();
        }

        StartCoroutine(Shake());
    }

    private void SetShakeValues()
    {
        noiseModule.m_AmplitudeGain = amplitud;
        noiseModule.m_FrequencyGain = frecuency;
    }

    private void ResetCameraShake()
    {
        noiseModule.m_AmplitudeGain = 0f;
        noiseModule.m_FrequencyGain = 0f;
    }
}
