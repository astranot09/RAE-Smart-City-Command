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

    [SerializeField] private OutpostManager outpostManager;

    [Header("Panel")]
    [SerializeField] private GameObject disasterChoosePanel;

    public void SetUpEvacuate(DisasterType type, OutpostManager outpost)
    {
        outpostManager = outpost;
        UIManager.instance.OpenPanel(disasterChoosePanel);
    }

    public void ChooseVolcano()
    {
        outpostManager.EvacuateConclusionType(DisasterType.Volcano);
        CloseEvactuate();
    }
    public void ChooseTsunami()
    {
        outpostManager.EvacuateConclusionType(DisasterType.Tsunami);
        CloseEvactuate();
    }
    public void ChooseEarthquake()
    {
        outpostManager.EvacuateConclusionType(DisasterType.Earthquake);
        CloseEvactuate();
    }

    private void CloseEvactuate()
    {
        outpostManager = null;
        UIManager.instance.ClosePanel(disasterChoosePanel);
    }
}
