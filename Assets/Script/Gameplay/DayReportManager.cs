using UnityEngine;
using TMPro;
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
    [SerializeField] private OutpostManager outpost2;
    [SerializeField] private int outPost_2_Unlocked = 4;
    [SerializeField] private OutpostManager outpost3;
    [SerializeField] private int outPost_3_Unlocked = 6;

    [Header("Setting")]
    [SerializeField] private OutpostManager outpostManager;
    [SerializeField] private int earningValue = 50;
    [SerializeField] private int evacuationFee = 10;

    [Header("Panel")]
    [SerializeField] private GameObject dayReportPanel;

    [Header("UI")]
    [SerializeField] private TMP_Text earningText;
    [SerializeField] private TMP_Text evacFeeText;
    [SerializeField] private TMP_Text totalEarningText;

    [SerializeField] private TMP_Text populationText;
    [SerializeField] private TMP_Text totalPopulationText;

    public void DayReportSetUp(int populationCurr, int populationChange, OutpostManager x, int evacTot)
    {
        Time.timeScale = 0;
        UIManager.instance.OpenPanel(dayReportPanel);
        outpostManager = x;
        earningText.text = $"Earnings : {earningValue}$";
        evacFeeText.text = $"Evacuation Fee : {evacuationFee * evacTot}$";
        totalEarningText.text = $"Earnings : {earningValue + (evacuationFee * evacTot)}$";
        populationText.text = $"Population : {populationChange}";
        totalPopulationText.text = $"Total Population : {populationCurr}";
    }

    public void Confirm()
    {
        outpostManager.StartCycle();
        UIManager.instance.ClosePanel(dayReportPanel);
        UpgradeManager.instance.UpgradeSetUp();
        dayCurrent++;
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

    }
}
