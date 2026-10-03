using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;
using UnityEngine.InputSystem;
public class CutsceneManager : MonoBehaviour
{
    [SerializeField] private GameObject cutscenePanel;
    [SerializeField] private Image cutsceneImage;
    [SerializeField] private List<Sprite> cutsceneSprites = new();
    [SerializeField] private CanvasGroup canvasGroup;

    [Header("Animation")]
    [SerializeField] private float delayBeforeCutsceneStart = 1f;
    [SerializeField] private float panelFadeDuration = 0.5f;


    private bool onNext = false;
    private bool isTransitioning = false;
    private Coroutine cutsceneCoroutine;

    private void Start()
    {
        InitializeComic();
    }

    public void InitializeComic()
    {
        cutscenePanel.SetActive(true);
        canvasGroup.alpha = 0f;

        if (cutsceneCoroutine != null)
        {
            StopCoroutine(cutsceneCoroutine);
        }

        cutsceneCoroutine = StartCoroutine(CutsceneStartRoutine());
    }

    private IEnumerator CutsceneStartRoutine()
    {
        yield return new WaitForSeconds(delayBeforeCutsceneStart);

        foreach (Sprite nextSprite in cutsceneSprites)
        {
            cutsceneImage.sprite = nextSprite;
            isTransitioning = true;
            onNext = false;

            yield return canvasGroup.DOFade(1f, panelFadeDuration).WaitForCompletion();

            isTransitioning = false;

            yield return new WaitUntil(() => onNext);
            onNext = false;

            isTransitioning = true;
            yield return canvasGroup.DOFade(0f, panelFadeDuration).WaitForCompletion();
        }

        CloseCutscene();
    }

    public void OnNextCutscene()
    {
        if (!isTransitioning)
        {
            onNext = true;
        }
    }

    public void CloseCutscene()
    {
        if (cutsceneCoroutine != null)
        {
            StopCoroutine(cutsceneCoroutine);
            cutsceneCoroutine = null;
        }

        canvasGroup.alpha = 0f;
        cutscenePanel.SetActive(false);
        if (SceneController.instance != null)
            SceneController.instance.LoadSceneByIndexPlus();
    }
}