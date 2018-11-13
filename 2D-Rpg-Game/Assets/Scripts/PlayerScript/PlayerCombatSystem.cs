using UnityEngine;
using System;

[RequireComponent(typeof(BoxCollider2D))]
public class PlayerCombatSystem : MonoBehaviour
{
    [Header("Lapso de activacion de espada")]
    [SerializeField] float minTimeActive = 0.1f;                            //Default value;
    [SerializeField] float maxTimeActive = 0.4f;                            //Defalut value;
    [SerializeField] AudioClip SwordSound;
    [Range(0f,1f)][SerializeField] float volume = 0.3f;

    Animator anim;
    PlayerController controller;
    BoxCollider2D swordCollider2D;
    AudioSource audioSource;

    void Awake ()
    {
        anim = GetComponentInParent<Animator>();
        controller = GetComponentInParent<PlayerController>();
        audioSource = GetComponentInParent<AudioSource>();

        swordCollider2D = GetComponent<BoxCollider2D>();
        swordCollider2D.enabled = false;
    }

    // Update is called once per frame
    private void Update()
    {
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        float tempo = stateInfo.normalizedTime;

        //Evitar la repeticion de animacion.
        bool attackOne = stateInfo.IsName("AtaqueDiagonalCorriendo_warrior");
        //bool attackTwo = stateInfo.IsName("Ataque_warrior");
        //bool attackThree = stateInfo.IsName("AtaqueGiratorio_warrior");

        bool activeSword = (tempo > minTimeActive && tempo < maxTimeActive) ? true : false;

        AtaqueDiagonal(attackOne, activeSword);
        //AtaqueNormal(attackTwo, activeSword);
        //AtaqueGiratorio(attackThree, activeSword);
    }

    private void AtaqueDiagonal(bool isAttacking, bool activeSword)
    {
        if (Input.GetKeyDown(KeyCode.H) && !isAttacking)
        {
            anim.SetTrigger("AtaqueDiagonal");
            audioSource.PlayOneShot(SwordSound, volume);
        }

        if (isAttacking && activeSword)
        {
            controller.enabled = false;
            swordCollider2D.enabled = true;
        }
        else
        {
            controller.enabled = true;
            swordCollider2D.enabled = false;
        }
    }
}
