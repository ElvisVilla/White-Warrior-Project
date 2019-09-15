using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Bissash.IA;

public class MusicManager : MonoBehaviour
{
    public float fadeDuration = 1.5f;

    IABrain[] enemies;
    [SerializeField] Music MainTheme;
    [SerializeField] Music[] CombatTheme;

    AudioSource source;

    // Start is called before the first frame update
    void Awake()
    {
        source = GetComponent<AudioSource>();
        enemies = FindObjectsOfType<IABrain>();
        MainTheme.Play(source);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void PlayCurrentSound()
    {
        
    }
    //IEnumerator PlayWithDelay(Music music)
    //{
    //    yield return new WaitForSeconds(transitionSecods);
    //    music.Play(audioSource);
    //}
}

[System.Serializable]
public struct Music
{
    public float volume;
    public AudioClip clip;
    public bool loop;


    public void Play(AudioSource source)
    {
        source.volume = volume;
        source.loop = loop;
        source.PlayOneShot(clip);
    }

    public void PlayAndFade(AudioSource source)
    {
        //source.DOFade()
    }
}
