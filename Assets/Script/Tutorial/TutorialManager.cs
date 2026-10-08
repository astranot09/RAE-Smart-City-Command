using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private TutorialSO tutorialSO;

    [SerializeField] private TutorialUI tutorialUI;

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

    }

}
