using UnityEngine;
using TMPro;

public class GraphManager : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] private OutpostManager outpostManager;

    [Header("Disaster Script")]
    [SerializeField] private SeismographSimulator seismographSimulator;
    [SerializeField] private GasSensorSimulator gasSensorSimulator;
    [SerializeField] private BuoySimulator buoySimulatorA;
    [SerializeField] private BuoySimulator buoySimulatorB;
    //[SerializeField] private TideGaugeSimulator tideGaugeSimulator


    public void OutpostDisasterGraphSetUp(UIGraphLine seis, UIGraphLine gasSensor, UIGraphLine buoyA, UIGraphLine buoyB, TMP_Text tideGauge)
    {
        if (!outpostManager.OutPostUnlocked)
        {
            Debug.Log("Outpost belum unlock");
            return;
        }
        if (seismographSimulator == null || gasSensorSimulator == null || buoySimulatorA == null || buoySimulatorB == null)
        {
            Debug.Log("Graph kureng lengkap");
            return;
        }
        seismographSimulator.graphLine = seis;
        gasSensorSimulator.graphLine = gasSensor;
        buoySimulatorA.graphLine = buoyA;
        buoySimulatorB.graphLine = buoyB;

        seismographSimulator.InitSeimograph();
        gasSensorSimulator.InitGasSensor();
        buoySimulatorB.InitBuoy();
        buoySimulatorB.InitBuoy();

        Debug.Log("SetUpGraph");
    }

    public void CloseUIGraph()
    {
        seismographSimulator.graphLine = null;
        gasSensorSimulator.graphLine = null;
        buoySimulatorA.graphLine = null;
        buoySimulatorB.graphLine = null;
    }
}
