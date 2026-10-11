using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Dialogue
{
    public string dialogueName;
    [TextArea(4,10)] public string dialogueText;
    public bool onLeft;
}

[CreateAssetMenu(fileName = "DialogueSO", menuName = "Scriptable Objects/DialogueSO")]
public class DialogueSO : ScriptableObject
{
    public string speakerName1_Start;
    public string speakerName2_Start;
    public List<Dialogue> dialogueList;
    //public bool onePerson;
    //public DialogueSO dialogueSO;
}
