using System;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private TutorialSO tutorialSO;

    [SerializeField] private TutorialUI tutorialUI;

    public event Action tutorialEvent;

    private bool onTutorial;
    public static TutorialManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void CallTutorial(TutorialSO tutorialSO)
    {
        if(onTutorial) return;
        onTutorial = true;
        tutorialEvent = null;
        tutorialUI.SetUpTutorialData(tutorialSO);
    }

    public void CloseTutorial()
    {
        onTutorial = false;
        tutorialEvent?.Invoke();
    }

}
