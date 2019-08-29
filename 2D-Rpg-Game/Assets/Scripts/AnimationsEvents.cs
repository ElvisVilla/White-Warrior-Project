using UnityEngine;
using UnityEngine.Events;

public class AnimationsEvents : MonoBehaviour
{
    Animator anim;
    public UnityEvent events;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }
    //Este metodo es llamado en los eventos de animacion de los ataques.
    public void AnimEvent()
    {
        events.Invoke();
    }

    public void AnimationCrossFade(Ability ability)
    {
        //int clip = AnimationHashCode(ability.AnimationName);
        anim.CrossFade(ability.AnimationName, 0);
    }

    public int AnimationHashCode(string animationName)
    {
        return Animator.StringToHash(animationName);
    }
}