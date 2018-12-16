using System.Collections;
using UnityEngine;
using System;

public class ParticlesController : MonoBehaviour
{
    [Header("Particles Effect")]
    [SerializeField] private GameObject dustRun;
    [SerializeField] private GameObject dustLanded;
    [SerializeField] private GameObject healEffect;
    [SerializeField] private GameObject hitEffect;
    public Transform foot;
    public Vector3 healPositioner = new Vector3(0, -0.2f, 0);
    public Vector3 dustLandPositioner = new Vector3(0, 0.1f, 0);


    void Start ()
    {
        dustRun.SetActive(false);
	}

    public IEnumerator DustRunParticle(float timeActive, float speed, bool grounded ,Player player)
    {
        if (grounded && speed != 0f)
        {
            dustRun.SetActive(true);
        }
        else if (!grounded)
        {
            yield return new WaitForSeconds(timeActive);
            dustRun.SetActive(false);
        }
        else
        {
            yield return new WaitForSeconds(0.35f);
            dustRun.SetActive(false);
        }
    }

    public void DustLandInstantiate(float destroyTime)
    {
        ParticleInstance(dustLanded, destroyTime, dustLandPositioner);
    }

    public void HealInstantiate(float destroyTime)
    {
        ParticleInstance(healEffect, destroyTime, healPositioner);
    }

    public void HitInstantiate(float destroyTime, Vector3 position)
    {
        ParticleInstance(hitEffect, destroyTime, position);
    }

    void ParticleInstance(GameObject particle, float destroy, Vector3 positioner)
    {
        GameObject particleInstance = Instantiate(particle, foot.position + positioner, Quaternion.identity, foot);
        Destroy(particleInstance, destroy);
    }
}
