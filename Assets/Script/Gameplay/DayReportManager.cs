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

    [Header("Setting")]
    [SerializeField] private OutpostManager outpostManager;
    [SerializeField] private int earningValue = 50;
    [SerializeField] private int evacuationFee = 10;
    [SerializeField] private int pop;

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
        UIManager.instance.OpenPanel(dayReportPanel);
        outpostManager = x;
        earningText.text = $"Earnings : {earningValue}$";
        evacFeeText.text = $"Evacuation Fee : {evacuationFee * evacTot}$";
        totalEarningText.text = $"Earnings : {earningValue - (evacuationFee * evacTot)}$";
        populationText.text = $"Population : {populationChange}";
        totalPopulationText.text = $"Total Population : {populationCurr}";
    }

    public void Confirm()
    {
        outpostManager.StartTimer();
    }
}
