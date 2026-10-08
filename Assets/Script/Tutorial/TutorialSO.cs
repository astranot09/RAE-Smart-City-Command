using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Tutorial
{
    public string title;
    public Sprite tutorialSprite;
    [TextArea(3,5)]public string description;
}


[CreateAssetMenu(fileName = "TutorialSO", menuName = "Scriptable Objects/TutorialSO")]
public class TutorialSO : ScriptableObject
{
    public List<Tutorial> tutorials;
}
