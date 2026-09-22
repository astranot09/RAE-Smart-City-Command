using System.Collections.Generic;
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

    public void SetUpEvacuate()
    {
        UIManager.instance.OpenPanel(disasterChoosePanel);
    }

    public void ChooseVolcano()
    {
        outpostManagers[VCamManager.instance.Index].EvacuateConclusionType(DisasterType.Volcano);
        CloseEvactuate();
    }
    public void ChooseTsunami()
    {
        outpostManagers[VCamManager.instance.Index].EvacuateConclusionType(DisasterType.Tsunami);
        CloseEvactuate();
    }
    public void ChooseEarthquake()
    {
        outpostManagers[VCamManager.instance.Index].EvacuateConclusionType(DisasterType.Earthquake);
        CloseEvactuate();
    }

    public void Evacuate()
    {
        outpostManagers[VCamManager.instance.Index].Evacuate();
    }

    private void CloseEvactuate()
    {
        UIManager.instance.ClosePanel(disasterChoosePanel);
    }
}
