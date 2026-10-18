using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[System.Serializable]
public class Cutscene
{
    public Sprite cutsceneSprite;
    public bool cutsceneEarthquake;
}
public class CutsceneManager : MonoBehaviour
{
    [SerializeField] private GameObject cutscenePanel;
    [SerializeField] private Image cutsceneImage;
    [SerializeField] private List<Cutscene> cutscenes = new();
    [SerializeField] private CanvasGroup canvasGroup;

    [Header("Animation")]
    [SerializeField] private float delayBeforeCutsceneStart = 1f;
    [SerializeField] private float panelFadeDuration = 0.5f;


    private bool onNext = false;
    private bool isTransitioning = false;
    private Coroutine cutsceneCoroutine;


    [Header("Shake Settings")]
    [SerializeField] private Transform backgroundTransform;
    [SerializeField] private float duration = 1f;
    [SerializeField] private float strength = 0.3f;
    [SerializeField] private int vibrato = 20;
    private Tween shakeTween;
    private Vector3 backgroundOriginalPos;

    private void Start()
    {
        AudioManager.instance.PlayBGM(AudioManager.instance.storyBGM);
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

        foreach (Cutscene nextCutscene in cutscenes)
        {
            cutsceneImage.sprite = nextCutscene.cutsceneSprite;
            isTransitioning = true;
            onNext = false;

            if (nextCutscene.cutsceneEarthquake)
            {
                EarthquakeOn();
            }
            yield return canvasGroup.DOFade(1f, panelFadeDuration).WaitForCompletion();

            isTransitioning = false;


            yield return new WaitUntil(() => onNext);
            onNext = false;

            isTransitioning = true;
            if (nextCutscene.cutsceneEarthquake)
            {
                EarthquakeStop();
            }
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

    public void EarthquakeOn()
    {
        Debug.Log("GETER");
        shakeTween = backgroundTransform.DOShakePosition(duration, strength, vibrato, 90f, false, false)
            .SetLoops(-1, LoopType.Restart);
    }

    public void EarthquakeStop()
    {
        Debug.Log("BERHENTI");
        if (shakeTween != null && shakeTween.IsActive())
        {
            shakeTween.Kill();
        }

        // Kembalikan background persis ke posisi awalnya
        if (backgroundTransform != null)
        {
            backgroundTransform.localPosition = backgroundOriginalPos;
        }
    }

}