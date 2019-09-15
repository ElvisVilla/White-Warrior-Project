using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Dialogs/Dialog")]
public class DialogueBase : ScriptableObject
{
    [System.Serializable]
    public class Info
    {
        public string name;
        public Sprite portrait;
        [TextArea(4, 8)]
        public string text;
    }

    public Info[] info;
}