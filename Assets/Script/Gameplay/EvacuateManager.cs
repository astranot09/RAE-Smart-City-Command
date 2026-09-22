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

    [SerializeField] private DisasterType disasterType;
    [SerializeField] private OutpostManager outpostManager;

    [Header("Panel")]
    [SerializeField] private GameObject disasterChoosePanel;

    public void SetUpEvacuate(DisasterType type, OutpostManager outpost)
    {
        disasterType = type;
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
        disasterType = DisasterType.None;
        outpostManager = null;
        UIManager.instance.ClosePanel(disasterChoosePanel);
    }
}
