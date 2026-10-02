using UnityEngine;
using TMPro;

public class GraphManager : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] private OutpostManager outpostManager;

    [Header("Disaster Script")]
    [SerializeField] private SeismographSimulator seismographSimulator;
    [SerializeField] private GasSensorSimulator gasSensorSimulator;
    [SerializeField] private BuoySimulator buoySimulator;
    //[SerializeField] private TideGaugeSimulator tideGaugeSimulator


    public void OutpostDisasterGraphSetUp(UIGraphLine seis, UIGraphLine gasSensor, UIGraphLine buoyA, UIGraphLine buoyB, TMP_Text tideGauge)
    {
        if (!outpostManager.OutPostUnlocked)
        {
            Debug.Log("Outpost belum unlock");
            return;
        }
        if (seismographSimulator == null || gasSensorSimulator == null)
        {
            Debug.Log("Graph kureng lengkap");
            return;
        }
        seismographSimulator.graphLine = seis;
        gasSensorSimulator.graphLine = gasSensor;
        buoySimulator.SetUp(buoyA, buoyB);

        seismographSimulator.InitSeimograph();
        gasSensorSimulator.InitGasSensor();
        buoySimulator.InitBuoy();

        Debug.Log("SetUpGraph");
    }

    public void CloseUIGraph()
    {
        seismographSimulator.graphLine = null;
        gasSensorSimulator.graphLine = null;
        buoySimulator.ResetUIBuoyGraph();
    }
}
