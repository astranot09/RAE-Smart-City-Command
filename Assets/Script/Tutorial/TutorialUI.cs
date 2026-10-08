using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
public class TutorialUI : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private TutorialSO tutorialSO;

    [SerializeField] private GameObject tutorialPanel;

    [SerializeField] private TMP_Text titleTutorial;
    [SerializeField] private TMP_Text descriptionTutorial;

    [SerializeField] private Image imageTutorial;

    private int indexTutorial = 0;
    private int maximumIndexTutorial = 0;
    public void SetUpTutorialData(TutorialSO data)
    {
        tutorialSO = data;
        indexTutorial = 0;
        maximumIndexTutorial = tutorialSO.tutorials.Count;
        UIManager.instance.OpenPanel(tutorialPanel);
        SetUpUITutorial();
    }

    public void SetUpUITutorial()
    {
        Mathf.Clamp(indexTutorial, 0, maximumIndexTutorial);
        imageTutorial.sprite = tutorialSO.tutorials[indexTutorial].tutorialSprite;
        titleTutorial.text = tutorialSO.tutorials[indexTutorial].title.ToString();
        descriptionTutorial.text = tutorialSO.tutorials[indexTutorial].description.ToString();
    }

    public void TutorialClose()
    {
        UIManager.instance.ClosePanel(tutorialPanel);
    }

    private void EntryAnimation()
    {
        
    }
    private void ExitAnimation()
    {

    }
}
