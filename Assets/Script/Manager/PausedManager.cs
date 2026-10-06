using DG.Tweening;
using UnityEngine;

public class PausedManager : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;


    [Header("Animation")]
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float animDuration = 0.5f;

    private bool onAnimation = false;

    public void OpenPausePanel()
    {
        if(onAnimation) return;
        if (!pausePanel.activeSelf)
        {
            UIManager.instance.OpenPanel(pausePanel);
            UIManager.instance.CanOpenPanel(false);
            PlayAnimationEntry();
        }
        else if (pausePanel.activeSelf)
        {
            PlayAnimationExit();
        }
    }
    private void PlayAnimationEntry()
    {
        onAnimation = true;
        canvasGroup.DOKill();

        Sequence s = DOTween.Sequence();
        s.Append(canvasGroup.DOFade(1, animDuration))
            .SetEase(Ease.OutQuad)
            .SetUpdate(true)
            .OnComplete(() =>
            {
                canvasGroup.DOKill();
                onAnimation = false;
            });
    }

    private void PlayAnimationExit()
    {
        onAnimation = true;
        canvasGroup.DOKill();

        Sequence s = DOTween.Sequence();
        s.Append(canvasGroup.DOFade(0, animDuration))
            .SetEase(Ease.InQuad)
            .SetUpdate(true)
            .OnComplete(() =>
            {
                canvasGroup.DOKill();
                onAnimation = false;
                UIManager.instance.ClosePanel(pausePanel);
                UIManager.instance.CanOpenPanel(true);
            });
    }
}
