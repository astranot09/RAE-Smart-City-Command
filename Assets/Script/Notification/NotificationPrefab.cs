using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
public class NotificationPrefab : MonoBehaviour
{
    [SerializeField] private TMP_Text notifText;
    [SerializeField] private RectTransform notifTransform;
    [SerializeField] private CanvasGroup notifCanvasGroup;

    [Header("Animation Settings")]
    [SerializeField] private float moveDistanceY = 100f;
    [SerializeField] private float animDuration = 1.5f;
    [SerializeField] private float displayDuration = 1.5f;

    private Vector2 startAnchoredPos;

    private void Awake()
    {
        if (notifTransform == null)
            notifTransform = GetComponent<RectTransform>();

        if (notifCanvasGroup == null)
            notifCanvasGroup = GetComponent<CanvasGroup>();
    }

    public void NotificationSetUp(string x)
    {
        Debug.Log("Halooooooo");
        notifText.text = x;
        Debug.Log(x);
        PlayAnimationNotification();
    }

    private void PlayAnimationNotification()
    {
        startAnchoredPos = notifTransform.anchoredPosition;
        notifTransform.DOKill();
        notifCanvasGroup.DOKill();

        notifTransform.anchoredPosition = startAnchoredPos;
        notifCanvasGroup.alpha = 0f;

        Sequence s = DOTween.Sequence().SetUpdate(true); ;

        s.Append(notifTransform.DOAnchorPosY(startAnchoredPos.y + moveDistanceY, animDuration / 2).SetEase(Ease.OutQuad))
            .Join(notifCanvasGroup.DOFade(1f, animDuration * 0.5f));

        s.AppendInterval(displayDuration);

        s.Append(notifCanvasGroup.DOFade(0f, animDuration))
            .Join(notifTransform.DOAnchorPosY(startAnchoredPos.y + (moveDistanceY * 1.5f), animDuration/2).SetEase(Ease.InQuad))
            .OnComplete(() =>
        {
            Destroy(gameObject);
        });
    }
}
