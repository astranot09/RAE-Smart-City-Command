using UnityEngine;

public class DisasterManager : MonoBehaviour
{
    [SerializeField] private OutpostManager outpostManager;

    [Header("Disaster Time")]
    [SerializeField] private float minTimeDisaster = 12f;
    [SerializeField] private float maxTimeDisaster = 45f;
    private float disasterTime = 0f;
    private float currTime = 0f;

    [Header("Disaster Type")]
    [SerializeField] private DisasterType disasterType = DisasterType.None;
    public DisasterType CurrentDisaster => disasterType;
    private DisasterType disasterChoosen = DisasterType.None;

    [Header("Disaster Script")]
    [SerializeField] private SeismographSimulator seismographSimulator;
    [SerializeField] private GasSensorSimulator gasSensorSimulator;
    [SerializeField] private BuoySimulator buoySimulatorA;
    [SerializeField] private BuoySimulator buoySimulatorB;
    //[SerializeField] private TideGaugeSimulator tideGaugeSimulator

    [Header("Setting")]
    [SerializeField] private float beginning_Stage_of_Disaster = 2f;
    [SerializeField] private float second_Stage_of_Disaster = 2f;
    [SerializeField] private float third_Stage_of_Disaster = 1f;
    //[SerializeField] private float delay_Before_Check_False_Alarm = 2f;
    //private bool onEvacuate = false;

    private void Update()
    {
        if (disasterType == DisasterType.None && outpostManager.OutPostUnlocked)
        {
            if (currTime >= disasterTime)
            {
                RandomizerDisaster();
            }
            currTime += Time.deltaTime;
        }
    }

    private void RandomizerDisaster()
    {
        int x = UnityEngine.Random.Range(0, 3);
        switch (x)
        {
            case 0:
                disasterType = DisasterType.Earthquake;
                //StartCoroutine(DisasterCountDown(disasterType));
                break;
            case 1:
                disasterType = DisasterType.Volcano;
                //StartCoroutine(DisasterCountDown(disasterType));
                break;
            case 2:
                disasterType = DisasterType.Tsunami;
                //StartCoroutine(DisasterCountDown(disasterType));
                break;
        }
    }

}
