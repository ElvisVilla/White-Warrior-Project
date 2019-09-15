using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;


public class DialogTrigger : MonoBehaviour
{
    //[HideInInspector] public Text dialogName;
    //[HideInInspector] public Text dialogText;
    //[HideInInspector] public Image dialogPortrait;
    [Header("Set Interactable info")]
    [SerializeField] Vector3 offset;
    [SerializeField] private LayerMask mask;
    [SerializeField] private float radius = 4f;
    private bool alreadyTriggered;
    
    [Header("Set Dialogue Resources")]
    [SerializeField] private GameObject indicatorDialogueCanvas;
    [SerializeField] private DialogueManager dialogueManager;
    public DialogueBase dialogue;

    private void Update()
    {
        var coll = Physics2D.OverlapCircle(transform.position + offset, radius, mask);

        if(coll != null)
        {
            if (alreadyTriggered == false)
            {
                SetTriggersValues(true, true);
            }

            if(dialogueManager.gameObject.activeInHierarchy == false)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    SetTriggersValues(false, true);
                    dialogueManager.StartDialogue(dialogue);
                }
            }

            if(Input.GetKeyDown(KeyCode.M))
            {
                SetTriggersValues(true, false);
                dialogueManager.EndOfDialogue();
            }

            if (dialogueManager.gameObject.activeInHierarchy)
            {
                if (Input.GetKeyDown(KeyCode.L))
                {
                    dialogueManager.NextText();

                    if (dialogueManager.DialogueRemaining() == 0)
                        SetTriggersValues(true, false);
                }
            }
        }
        else
        {
            if(alreadyTriggered == true)
                SetTriggersValues(false, false);

            dialogueManager.EndOfDialogue();
        }
    }

    //Estos valores bool solo sirven para activar y desactivar el indicador de interaccion, no realizan mas que eso.
    private void SetTriggersValues(bool canvasValue, bool triggeredValue)
    {
        indicatorDialogueCanvas.SetActive(canvasValue);
        alreadyTriggered = triggeredValue;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position + offset, radius);
    }
}
