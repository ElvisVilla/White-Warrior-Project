using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class DialogueManager : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI dialogueName = null;
    [SerializeField]private TextMeshProUGUI dialogueText = null;
    [SerializeField]private Image dialoguePortrait = null;
    [SerializeField]private float delaySeconds = 0.02f;

    private DialogueBase.Info currentDialogInfo;
    private Queue<DialogueBase.Info> dialogInfo = new Queue<DialogueBase.Info>();

    [Header("Events")]
    [SerializeField] private GameEvent DialogueStartEvent = null;
    [SerializeField] private GameEvent DialogueFinishedEvent = null;

    public void StartDialogue(DialogueBase dialogue, Action optionalEvent = null)
    {
        DialogueStartEvent.Raise();
        gameObject.SetActive(true);

        optionalEvent?.Invoke();
        EnqueueDialog(dialogue);
    }

    public void EndOfDialogue(Action optionalEvent = null)
    {
        DialogueFinishedEvent.Raise();
        optionalEvent?.Invoke();
        gameObject.SetActive(false);
    }

    public void NextText()
    {
        if(dialogueText.text != currentDialogInfo.text)
        {
            dialogueText.text = "";
            dialogueText.text = currentDialogInfo.text;
            StopAllCoroutines();
        }else
        {
            DequeueDialog();
        }

        //Normal Dequeue con el tiempo normal.
        //DequeueDialog(optionalEvent);
    }

    private void EnqueueDialog(DialogueBase dialog)
    {
        dialogInfo.Clear();

        foreach(DialogueBase.Info info in dialog.info)
        {
            dialogInfo.Enqueue(info);
        }

        DequeueDialog();
    }

    private void DequeueDialog(Action optionalEvent = null)
    {
        if(dialogInfo.Count == 0)
        {
            EndOfDialogue(optionalEvent);
            return;
        }

        currentDialogInfo = dialogInfo.Dequeue();
        dialogueName.text = currentDialogInfo.name;
        dialoguePortrait.sprite = currentDialogInfo.portrait;

        StopAllCoroutines();
        StartCoroutine(TypeText(currentDialogInfo));
    }

    IEnumerator TypeText(DialogueBase.Info info)
    {
        dialogueText.text = "";
        foreach(char c in info.text.ToCharArray())
        {
            yield return new WaitForSeconds(delaySeconds);
            dialogueText.text += c;
        }
    }

    public int DialogueRemaining()
    {
        return dialogInfo.Count;
    }
}
