using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class EvacuateManager : MonoBehaviour
{
    public static EvacuateManager instance;

    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    //[SerializeField] private OutpostManager outpostManager;

    [Header("Panel")]
    [SerializeField] private GameObject disasterChoosePanel;

    [Header("Outpost")]
    [SerializeField] private List<OutpostManager> outpostManagers = new List<OutpostManager>();

    [Header("Animation")]
    [SerializeField] private Transform disasterPanelTransform;
    [SerializeField] private float animDuration = 0.3f;

    [SerializeField] private Vector3 startScale = new Vector3(0.2f, 0.2f, 0.2f);
    [SerializeField] private Vector3 targetScale = new Vector3(1f, 1f, 1f);
    private bool onAnimation = false;

    private void Start()
    {
        disasterPanelTransform.localScale = startScale;
    }
    public void SetUpEvacuate()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.buttonClick);
        UIManager.instance.OpenPanel(disasterChoosePanel);
        PlayAnimationEntry();
    }

    public void ChooseVolcano()
    {
        if(onAnimation) return;
        outpostManagers[VCamManager.instance.Index].DisasterManager.EvacuateConclusionType(DisasterType.Volcano);
        CloseEvactuate();
    }
    public void ChooseTsunami()
    {
        if (onAnimation) return;
        outpostManagers[VCamManager.instance.Index].DisasterManager.EvacuateConclusionType(DisasterType.Tsunami);
        CloseEvactuate();
    }
    public void ChooseEarthquake()
    {
        if (onAnimation) return;
        outpostManagers[VCamManager.instance.Index].DisasterManager.EvacuateConclusionType(DisasterType.Earthquake);
        CloseEvactuate();
    }

    public void Evacuate()
    {
        if (onAnimation) return;
        outpostManagers[VCamManager.instance.Index].Evacuate();
    }

    private void CloseEvactuate()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.buttonClick);
        PlayAnimationExit();
    }

    private void PlayAnimationEntry()
    {
        onAnimation = true;
        disasterPanelTransform.DOKill();

        Sequence s = DOTween.Sequence();
        s.Append(disasterPanelTransform.DOScale(targetScale, animDuration))
            .SetEase(Ease.OutBack)
            .SetUpdate(true)
            .OnComplete(() =>
            {
                disasterPanelTransform.DOKill();
                onAnimation = false;
            });
    }

    private void PlayAnimationExit()
    {
        onAnimation = true;
        disasterPanelTransform.DOKill();

        Sequence s = DOTween.Sequence();
        s.Append(disasterPanelTransform.DOScale(startScale, animDuration))
            .SetEase(Ease.InQuad)
            .SetUpdate(true)
            .OnComplete(() =>
            {
                disasterPanelTransform.DOKill();
                onAnimation = false;
                UIManager.instance.ClosePanel(disasterChoosePanel);
            });
    }
}
