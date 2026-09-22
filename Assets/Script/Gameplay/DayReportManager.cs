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


    [Header("Panel")]
    [SerializeField] private GameObject dayReportPanel;

    [Header("UI")]
    [SerializeField] private TMP_Text earningText;
    [SerializeField] private TMP_Text falseAlarmText;
    [SerializeField] private TMP_Text totalEarningText;

    [SerializeField] private TMP_Text minPopulationText;
    [SerializeField] private TMP_Text populationText;

    public void DayReportSetUp(int population, bool falseAlarm)
    {
        UIManager.instance.OpenPanel(dayReportPanel);
    }

}
