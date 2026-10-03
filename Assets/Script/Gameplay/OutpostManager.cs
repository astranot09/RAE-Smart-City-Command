using System;
using System.Collections;
using System.Collections.Generic;
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
    [Header("Reference")]
    [SerializeField] private DisasterManager disasterManager;
    [SerializeField] private GraphManager graphManager;
    public GraphManager GraphManager => graphManager;
    public DisasterManager DisasterManager => disasterManager;

    [Header("Population")]
    [SerializeField] private int startPopulation = 36;
    [SerializeField] private int population;
    public int Population => population;

    [Header("Setting")]
    [SerializeField] private bool outPostUnlocked = false;
    public bool OutPostUnlocked => outPostUnlocked;
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

    public event Action cycleStart;

    private void OnEnable()
    {
        cycleStart += CycleReset;
        disasterManager.onDisasterHitOutpost += CheckVillage;
    }
    private void OnDisable()
    {
        cycleStart -= CycleReset;
        disasterManager.onDisasterHitOutpost -= CheckVillage;
    }

    private void Start()
    {
        if(outPostUnlocked)
            StartCycle();
    }

    public void StartCycle()
    {
        cycleStart?.Invoke();
    }

    private void CycleReset()
    {
        onEvacuate = false;
        totalPopulationLoss = 0;
        totalEvacuation = 0;
        currentNpcEscape = 0;
        correctEvacuation = false;
        falseAlarm = false;


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
    }

    //Ini dipasang di animasi kena
    public void CheckVillage()
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
            LoseManager.instance.LoseSetUp(this);
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
        DisasterType disasterChoosen = disasterManager.DisasterChoosen;
        DisasterType disasterType = disasterManager.CurrentDisaster;

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
        population = startPopulation;
    }

}
