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


    [Header("Seismograph")]
    [SerializeField] private SeismographSimulator seismographSimulator;

    [Header("Gas Sensor")]
    [SerializeField] private GameObject GasSensorPanel;

    [Header("Buoy Type A")]
    [SerializeField] private BuoySimulator buoySimulator_A;

    [Header("Buoy Type B")]
    [SerializeField] private BuoySimulator buoySimulator_B;

    [Header("Tide Gauge")]
    [SerializeField] private GameObject TideGaugePanel;

    [Header("Currency")]
    [SerializeField] private int currency;


}
