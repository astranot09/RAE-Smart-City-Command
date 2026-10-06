using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class DayReportManager : MonoBehaviour
{
    public static DayReportManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    [SerializeField] private int dayCurrent = 0;
    public int DayCurrent => dayCurrent;
    [Header("Outpost")]
    [SerializeField] private OutpostManager outpost1;
    [SerializeField] private OutpostManager outpost2;
    [SerializeField] private int outPost_2_Unlocked = 4;
    [SerializeField] private OutpostManager outpost3;
    [SerializeField] private int outPost_3_Unlocked = 6;

    [Header("Setting")]
    [SerializeField] private OutpostManager outpostManager;
    [SerializeField] private int earningValue = 50;
    [SerializeField] private int evacuationFee = 10;

    [Header("Population Pay Check")]
    [SerializeField] private int populationGrowth = 8;
    [SerializeField] private int populationGrowthInterval = 3;


    [Header("Panel")]
    [SerializeField] private GameObject dayReportPanel;

    [Header("UI")]
    [SerializeField] private TMP_Text earningText;
    [SerializeField] private TMP_Text evacFeeText;
    [SerializeField] private TMP_Text totalEarningText;

    [SerializeField] private TMP_Text populationText;
    [SerializeField] private TMP_Text totalPopulationText;

    [Header("Animation")]
    [SerializeField] private Transform dayReportTransform;
    [SerializeField] private float animDuration = 0.3f;

    [SerializeField] private Vector3 startScale = new Vector3(0.2f, 0.2f, 0.2f);
    [SerializeField] private Vector3 targetScale = new Vector3(1f, 1f, 1f);
    private bool onAnimation = false;

    private void Start()
    {
        dayReportTransform.localScale = startScale;
    }

    public void DayReportSetUp(int populationCurr, int populationChange, OutpostManager x, int evacTot)
    {
        CurrencyManager.instance.ChangeCurrency(earningValue);
        UIManager.instance.OpenPanel(dayReportPanel);
        outpostManager = x;

        if(dayCurrent % populationGrowthInterval == populationGrowthInterval-1)
        {
            populationChange += populationGrowth;
        }

        earningText.text = $"Earnings : {earningValue}$";
        evacFeeText.text = $"Evacuation Fee : {evacuationFee * evacTot}$";
        totalEarningText.text = $"Earnings : {earningValue + (evacuationFee * evacTot)}$";
        populationText.text = $"Population : {populationChange}";
        totalPopulationText.text = $"Total Population : {populationCurr}";
        PlayAnimationEntry();
    }

    public void Confirm()
    {
        outpostManager.StartCycle();
        AudioManager.instance.PlaySFX(AudioManager.instance.buttonClick);
        dayCurrent++;
        CheckDay();
        PlayAnimationExit();
    }


    public void CheckDay()
    {
        if(dayCurrent == outPost_2_Unlocked)
        {
            outpost2.UnlockOutpost();
        }
        else if(dayCurrent == outPost_3_Unlocked)
        {
            outpost3.UnlockOutpost();
        }
        if(dayCurrent%populationGrowthInterval == 0)
        {
            outpost1.PopulationChange(populationGrowth);
            outpost2.PopulationChange(populationGrowth);
            outpost3.PopulationChange(populationGrowth);
        }
    }

    private void PlayAnimationEntry()
    {
        onAnimation = true;
        dayReportTransform.DOKill();

        Sequence s = DOTween.Sequence();
        s.Append(dayReportTransform.DOScale(targetScale, animDuration))
            .SetEase(Ease.OutBack)
            .SetUpdate(true)
            .OnComplete(() =>
            {
                dayReportTransform.DOKill();
                onAnimation = false;
            });
    }

    private void PlayAnimationExit()
    {
        onAnimation = true;
        dayReportTransform.DOKill();

        Sequence s = DOTween.Sequence();
        s.Append(dayReportTransform.DOScale(startScale, animDuration))
            .SetEase(Ease.InQuad)
            .SetUpdate(true)
            .OnComplete(() =>
            {
                dayReportTransform.DOKill();
                onAnimation = false;
                UIManager.instance.ClosePanel(dayReportPanel);
                UpgradeManager.instance.UpgradeSetUp();
            });
    }

}
