using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    public static GameplayManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }


    [Header("Outpost")]
    [SerializeField] private List<OutpostManager> outpostManagers = new List<OutpostManager>();

    [Header("UI--Population")]
    [SerializeField] private TMP_Text populationText;

    [Header("GraphLine Script")]
    [SerializeField] private UIGraphLine seismographGraphLine;
    [SerializeField] private UIGraphLine gasSensorGraphLine;
    [SerializeField] private UIGraphLine buoyAGraphLine;
    [SerializeField] private UIGraphLine buoyBGraphLine;
    [SerializeField] private TMP_Text tideGaugeText;

    [SerializeField] private VCamManager vcamManager;

    private void OnEnable()
    {
        vcamManager.onCameraChange += OutPostGraphSetUp;
        vcamManager.onCameraChange += OutPostPopulationSetUp;
        foreach(var manager in outpostManagers)
        {
            manager.cycleStart += OutPostPopulationSetUp;
        }
    }

    private void OnDisable()
    {
        vcamManager.onCameraChange -= OutPostGraphSetUp;
        vcamManager.onCameraChange += OutPostPopulationSetUp;

        foreach (var manager in outpostManagers)
        {
            manager.cycleStart -= OutPostPopulationSetUp;
        }
    }

    public void OutPostGraphSetUp()
    {
        seismographGraphLine.ClearGraph();
        gasSensorGraphLine.ClearGraph();
        buoyAGraphLine.ClearGraph();
        buoyBGraphLine.ClearGraph();
        tideGaugeText.text = string.Empty;

        for (int i = 0; i < outpostManagers.Count; i++)
        {
            outpostManagers[i].GraphManager.CloseUIGraph();
        }
        outpostManagers[VCamManager.instance.Index].GraphManager.OutpostDisasterGraphSetUp(seismographGraphLine,gasSensorGraphLine,buoyAGraphLine,buoyBGraphLine, tideGaugeText);
    }

    public void OutPostPopulationSetUp()
    {
        populationText.text = outpostManagers[VCamManager.instance.Index].Population.ToString();
    }
}
