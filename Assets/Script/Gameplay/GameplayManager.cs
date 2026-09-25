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
    }

    private void OnDisable()
    {
        vcamManager.onCameraChange -= OutPostGraphSetUp;
    }

    public void OutPostGraphSetUp()
    {
        for(int i = 0; i < outpostManagers.Count; i++)
        {
            outpostManagers[i].CloseUIGraph();
        }
        outpostManagers[VCamManager.instance.Index].OutpostDisasterGraphSetUp(seismographGraphLine,gasSensorGraphLine,buoyAGraphLine,buoyBGraphLine, tideGaugeText);
    }

}
