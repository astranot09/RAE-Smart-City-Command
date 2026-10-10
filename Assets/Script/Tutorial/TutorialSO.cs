using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
[System.Serializable]
public class Tutorial
{
    public string title;
    public VideoClip videoClip;
    [TextArea(3,5)]public string description;
}


[CreateAssetMenu(fileName = "TutorialSO", menuName = "Scriptable Objects/TutorialSO")]
public class TutorialSO : ScriptableObject
{
    public List<Tutorial> tutorials;
}
