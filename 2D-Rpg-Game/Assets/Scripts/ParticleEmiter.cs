using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEmiter : MonoBehaviour
{
    private List<ParticleSystem> particles = new List<ParticleSystem>(10);
    [SerializeField] bool loop = false;
    [SerializeField] bool playOnAwake = false;
    //bool alreadyPlayed = false;

    private void OnEnable()
    {
        particles = transform.GetChildElementsTo<ParticleSystem>();

        particles.ForEach(particleSystem =>
        {
            var mainModule = particleSystem.main;
            mainModule.loop = loop;
            mainModule.playOnAwake = playOnAwake;
        });
    }

    public void Play()
    {
        particles.ForEach(particle => 
        {
            if (!particle.isPlaying)
            {
                particle.Play();
            }
        });
    }

    public void Stop() //Podria ser buena idea crear una version con play with loop y otra sin playwith loop.
    {
        particles.ForEach(particle => 
        {
            if (particle.isEmitting == true) //Es posible que esta comprobacion se deba realizar en el codigo donde se llama.
            {
                particle.Stop();
            }
        });
    }

    public void SetLoop(bool value)
    {
        particles.ForEach(particles =>
        {
            var mainModule = particles.main;
            mainModule.loop = value;
        });
    }
}
