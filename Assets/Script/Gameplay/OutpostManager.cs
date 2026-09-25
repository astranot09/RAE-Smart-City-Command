using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Metadata;
using TMPro;
using UnityEngine;

[System.Serializable]
public enum DisasterType
{
    None,
    Earthquake,
    Volcano,
    Tsunami
}


public class OutpostManager : MonoBehaviour
{
    [Header("Disaster Time")]
    [SerializeField] private float minTimeDisaster = 12f;
    [SerializeField] private float maxTimeDisaster = 45f;
    private float disasterTime = 0f;
    private float currTime = 0f;

    [Header("Disaster Type")]
    [SerializeField] private DisasterType disasterType = DisasterType.None;
    private DisasterType disasterChoosen = DisasterType.None;

    [Header("Disaster Script")]
    [SerializeField] private SeismographSimulator seismographSimulator;
    [SerializeField] private GasSensorSimulator gasSensorSimulator;
    [SerializeField] private BuoySimulator buoySimulatorA;
    [SerializeField] private BuoySimulator buoySimulatorB;

    //Tide Gauge

    [Header("Population")]
    [SerializeField] private int population;

    [Header("Setting")]
    [SerializeField] private float beginning_Stage_of_Disaster = 2f;
    [SerializeField] private float second_Stage_of_Disaster = 2f;
    [SerializeField] private float third_Stage_of_Disaster = 1f;
    [SerializeField] private bool outPostUnlocked = false;
    [SerializeField] private float delay_Before_Check_False_Alarm = 2f;
    private bool onEvacuate = false;

    [Header("NPC")]
    [SerializeField] private int maxNpcShow = 5;
    private int currentNpcShow;
    private int currentNpcEscape;
    private int npcIdle;
    [SerializeField] private List<Transform> npcSpawner;
    [SerializeField] private GameObject npcPrefab;
    [SerializeField] private Transform evacuationLocation;
    [SerializeField] private float minRangeWandering;
    [SerializeField] private float maxRangeWandering;

    [Header("Failed")]
    [SerializeField] private int populationLoss_Because_No_Evacuation = 30;
    [SerializeField] private int populationLoss_Because_Wrong_Evacuation = 15;
    [SerializeField] private int populationLoss_Because_FalseAlarm = 5;

    [Header("Conclusion")]
    private bool correctEvacuation = false;
    private bool falseAlarm = false;
    private int totalPopulationLoss = 0;
    private int totalEvacuation;

    [SerializeField] private Animator animator;

    public event Action cycleStart;

    private void OnEnable()
    {
        cycleStart += CycleReset;
    }
    private void OnDisable()
    {
        cycleStart -= CycleReset;
    }

    private void Start()
    {
        if(outPostUnlocked)
            StartCycle();
    }

    private void Update()
    {
        if(disasterType == DisasterType.None && outPostUnlocked)
        {
            if(currTime >= disasterTime)
            {
                RandomizerDisaster();
            }
            currTime += Time.deltaTime;
        }
    }

    public void StartCycle()
    {
        cycleStart?.Invoke();
    }

    private void CycleReset()
    {
        onEvacuate = false;
        currTime = 0f;
        totalPopulationLoss = 0;
        totalEvacuation = 0;
        currentNpcEscape = 0;
        correctEvacuation = false;
        falseAlarm = false;
        disasterChoosen = DisasterType.None;


        for (int i = 0; i < npcSpawner.Count; i++)
        {
            if (npcSpawner[i] != null && npcSpawner[i].childCount > 0)
            {
                GameObject x = npcSpawner[i].GetChild(0).gameObject;
                if (x != null)
                {
                    NPCScript y = x.GetComponent<NPCScript>();
                    if (y != null)
                    {
                        y.ChangeState(NPCState.BackFromEvacuation);
                    }
                }
            }

        }


        SpawnNPC();


        disasterTime = UnityEngine.Random.Range(minTimeDisaster, maxTimeDisaster);
        disasterType = DisasterType.None;
    }

    private void RandomizerDisaster()
    {
        int x = UnityEngine.Random.Range(0, 3);
        switch (x)
        {
            case 0:
                disasterType = DisasterType.Earthquake;
                StartCoroutine(DisasterCountDown(disasterType));
                break;
            case 1:
                disasterType = DisasterType.Volcano;
                StartCoroutine(DisasterCountDown(disasterType));
                break;
            case 2:
                disasterType = DisasterType.Tsunami;
                StartCoroutine(DisasterCountDown(disasterType));
                break;
        }
    }

    IEnumerator DisasterCountDown(DisasterType type)
    {
        //chart naik (versi upgrade)
        yield return new WaitForSeconds(beginning_Stage_of_Disaster);
        //chart naik (versi normal)
        yield return new WaitForSeconds(second_Stage_of_Disaster);
        //Predicted status muncul
        yield return new WaitForSeconds(third_Stage_of_Disaster);
        //mainin animasi disaster
        switch (disasterType)
        {
            case DisasterType.Volcano:
                animator.SetTrigger("Volcano");
                break;
            case DisasterType.Tsunami:
                animator.SetTrigger("Tsunami");
                break;
            case DisasterType.Earthquake:
                animator.SetTrigger("Earthquake");
                break;
        }
    }

    //Ini dipasang di animasi kena
    public void DisasterHitVillage()
    {
        if (!onEvacuate)
        {
            PopulationDecrease(populationLoss_Because_No_Evacuation);
            totalPopulationLoss += populationLoss_Because_No_Evacuation;
        }

        for (int i = 0; i < npcSpawner.Count; i++)
        {
            if (npcSpawner[i] != null && npcSpawner[i].childCount > 0)
            {
                GameObject x = npcSpawner[i].GetChild(0).gameObject;
                if (x != null)
                {
                    NPCScript y = x.GetComponent<NPCScript>();
                    if (y != null && !y.InEvacuationArea)
                    {
                        totalPopulationLoss--;
                        currentNpcShow--;
                        PopulationDecrease(-1);
                        CheckNPCEvac();
                        y.NPC_Dead();
                    }
                }
            }

        }
        DayReportManager.instance.DayReportSetUp(population, totalPopulationLoss, this, totalEvacuation);
    }

    //pasang di tombol
    public void Evacuate()
    {
        if(onEvacuate || !outPostUnlocked) return;
        totalEvacuation++;
        onEvacuate = true;
        npcIdle = 0;

        for (int i = 0; i < npcSpawner.Count; i++)
        {
            if (npcSpawner[i] != null && npcSpawner[i].childCount > 0)
            {
                GameObject x = npcSpawner[i].GetChild(0).gameObject;
                if (x != null)
                {
                    NPCScript y = x.GetComponent<NPCScript>();
                    if (y != null)
                    {
                        y.SetUp(this, evacuationLocation, minRangeWandering, maxRangeWandering);
                        y.ChangeState(NPCState.Evacuation);
                    }
                }
            }

        }
        EvacuateManager.instance.SetUpEvacuate();
        Time.timeScale = 0; //Dipause
    }

    public void EvacuateConclusionType(DisasterType x)
    {
        disasterChoosen = x;
        Time.timeScale = 1; //Unpaused
    }

    public void PopulationDecrease(int value)
    {
        population += value;


        if (currentNpcShow > population)
        {
            int difference = currentNpcShow - population;
            for (int i = npcSpawner.Count - 1; i >= 0; i--)
            {
                if (npcSpawner[i] != null && npcSpawner[i].childCount > 0)
                {
                    difference--;
                    GameObject x = npcSpawner[i].GetChild(0).gameObject;
                    Destroy(x);
                    if (difference <= 0)
                    {
                        break;
                    }
                }

            }
        }


        if (population <= 0)
        {
            //kalah
        }
    }

    //Dipanggil di script NPC
    public void NPCEscaped()
    {
        currentNpcEscape++;
        CheckNPCEvac();
    }

    private void CheckNPCEvac()
    {
        Debug.Log($"{currentNpcEscape}/{currentNpcShow}");
        if (currentNpcEscape >= currentNpcShow)
        {
            Debug.Log("Halo");
            EvacuateFinished();
        }
    }
    //public void NPCLeaveEvacuateArea()
    //{
    //    currentNpcEscape--;
    //    Mathf.Clamp(currentNpcEscape, 0, maxNpcShow);
    //}

    public void NPCBackFromEvacuateArea()
    {
        npcIdle++;
        if(npcIdle >= currentNpcShow)
        {
            onEvacuate = false;
        }
    }

    private void CheckEvacuateType()
    {
        if (!onEvacuate) return;
        if (disasterChoosen == disasterType)
        {
            Debug.Log("Pilihan benar");
            //bener
        }
        else if (disasterType == DisasterType.None && !falseAlarm)
        {
            falseAlarm = true;
            Debug.Log("Check bakal false alarm ga");
            StartCoroutine(DoubleCheckDisaster());
        }
        else if (disasterType == DisasterType.None && falseAlarm)
        {
            Debug.Log("False Alarm");
            falseAlarm = false;
            PopulationDecrease(populationLoss_Because_FalseAlarm);
            totalPopulationLoss += (populationLoss_Because_FalseAlarm);

            for (int i = 0; i < npcSpawner.Count; i++)
            {
                if (npcSpawner[i] != null && npcSpawner[i].childCount > 0)
                {
                    GameObject x = npcSpawner[i].GetChild(0).gameObject;
                    if (x != null)
                    {
                        NPCScript y = x.GetComponent<NPCScript>();
                        if (y != null)
                        {
                            y.ChangeState(NPCState.BackFromEvacuation);
                        }
                    }
                }

            }
            currentNpcEscape = 0;
        }
        else
        {
            Debug.Log("Pilihan Salah");
            PopulationDecrease(populationLoss_Because_Wrong_Evacuation);
            totalPopulationLoss += (populationLoss_Because_Wrong_Evacuation);
            //salah
        }
    }

    IEnumerator DoubleCheckDisaster()
    {
        yield return new WaitForSeconds(delay_Before_Check_False_Alarm);
        CheckEvacuateType();
    }

    private void EvacuateFinished()
    {
        CheckEvacuateType();
    }

    private void SpawnNPC()
    {
        foreach (Transform x in npcSpawner)
        {
            if (x != null && x.childCount == 0 && currentNpcShow <= maxNpcShow && currentNpcShow < population)
            {
                GameObject spawnedNpc = Instantiate(npcPrefab, x.position, x.rotation, x);
                spawnedNpc.GetComponent<NPCScript>().SetUp(this, evacuationLocation, minRangeWandering, maxRangeWandering);
                currentNpcShow++;
            }
        }
    }

    public void UnlockOutpost()
    {
        outPostUnlocked = true;
    }

    public void OutpostDisasterGraphSetUp(UIGraphLine seis, UIGraphLine gasSensor, UIGraphLine buoyA, UIGraphLine buoyB, TMP_Text tideGauge)
    {
        if (!outPostUnlocked)
        {
            Debug.Log("Outpost belum unlock");
            return;
        }
        if(seismographSimulator == null || gasSensorSimulator == null || buoySimulatorA == null || buoySimulatorB == null)
        {
            Debug.Log("Graph kureng lengkap");
            return;
        }
        seismographSimulator.graphLine = seis;
        gasSensorSimulator.graphLine = gasSensor;
        buoySimulatorA.graphLine = buoyA;
        buoySimulatorB.graphLine = buoyB;
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
