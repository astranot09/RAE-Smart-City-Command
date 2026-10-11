using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.Video;
public class TutorialUI : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private TutorialSO tutorialSO;

    [Header("UI")]
    [SerializeField] private GameObject tutorialPanel;
    [SerializeField] private Transform tutorialPanelTransform;

    [SerializeField] private TMP_Text titleTutorial;
    [SerializeField] private VideoPlayer videoTutorial;
    [SerializeField] private TMP_Text descriptionTutorial;

    [SerializeField] private GameObject prevButton;
    [SerializeField] private GameObject nextButton;
    [SerializeField] private GameObject closeButton;

    [Header("Animation")]
    [SerializeField] private float animDuration;

    private int indexTutorial = 0;
    private int maximumIndexTutorial = 0;
    public void SetUpTutorialData(TutorialSO data)
    {
        tutorialSO = data;
        indexTutorial = 0;
        maximumIndexTutorial = tutorialSO.tutorials.Count;
        UIManager.instance.OpenPanel(tutorialPanel);
        EntryAnimation();
        SetUpUITutorial();
    }

    public void SetUpUITutorial()
    {
        prevButton.SetActive(true);
        nextButton.SetActive(true);
        closeButton.SetActive(false);
        Mathf.Clamp(indexTutorial, 0, maximumIndexTutorial-1);
        if(tutorialSO.tutorials[indexTutorial].videoClip != null)
        {
            videoTutorial.clip = tutorialSO.tutorials[indexTutorial].videoClip;
        }
        if (tutorialSO.tutorials[indexTutorial].title != null)
        {
            titleTutorial.text = tutorialSO.tutorials[indexTutorial].title.ToString();
        }
        if (tutorialSO.tutorials[indexTutorial].description != null)
        {
            descriptionTutorial.text = tutorialSO.tutorials[indexTutorial].description.ToString();
        }
        descriptionTutorial.text = tutorialSO.tutorials[indexTutorial].description.ToString();
        if(indexTutorial >= maximumIndexTutorial - 1)
        {
            nextButton.SetActive(false);
            closeButton.SetActive(true);
        }
        if (indexTutorial <= 0)
        {
            prevButton.SetActive(false);
        }
    }
    public void NextPageTutorial()
    {
        indexTutorial++;
        SetUpUITutorial();
    }
    public void PreviousPageTutorial()
    {
        indexTutorial--;
        SetUpUITutorial();
    }
    public void TutorialClose()
    {
        Debug.Log("Close Tutorial");
        ExitAnimation();
    }

    private void EntryAnimation()
    {
        tutorialPanelTransform.DOKill();

        tutorialPanelTransform.localScale = Vector3.zero;
        Sequence s = DOTween.Sequence().SetUpdate(true);

        s.Append(tutorialPanelTransform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), animDuration).SetEase(Ease.OutQuad))
                          .Append(tutorialPanelTransform.DOScale(Vector3.one, animDuration * 0.5f).SetEase(Ease.InOutQuad));
    }
    private void ExitAnimation()
    {
        tutorialPanelTransform.DOKill();

        Sequence s = DOTween.Sequence().SetUpdate(true);

        s.Append(tutorialPanelTransform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), animDuration * 0.4f).SetEase(Ease.OutQuad))
                     .Append(tutorialPanelTransform.DOScale(Vector3.zero, animDuration).SetEase(Ease.InBack))
                     .OnComplete(() =>
                     {
                         UIManager.instance.ClosePanel(tutorialPanel);
                         TutorialManager.instance.CloseTutorial();
                            
                     }

                     );
    }
}
