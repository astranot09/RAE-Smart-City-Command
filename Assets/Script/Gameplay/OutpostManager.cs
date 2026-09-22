using System.Collections;
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


    [Header("Population")]
    [SerializeField] private int population;

    [Header("Setting")]
    [SerializeField] private float beginning_Stage_of_Disaster = 4f;
    [SerializeField] private bool outPostUnlocked = false;
    private bool onEvacuate = false;

    [Header("NPC")]
    [SerializeField] private int maxNpcShow = 5;
    private int currentNpcShow;
    private int currentNpcEscape;
    [SerializeField] private Transform npcSpawner;
    [SerializeField] private GameObject npcPrefab;

    [Header("Failed")]
    [SerializeField] private int populationLoss = 30;

    [Header("Conclusion")]
    private bool correctEvacuation = false;
    private int totalPopulationLoss = 0;


    private void Start()
    {
        if(outPostUnlocked)
            StartTimer();
    }

    private void Update()
    {
        if(DisasterType.None == disasterType)
        {
            if(currTime >= disasterTime)
            {
                RandomizerDisaster();
            }
            currTime += Time.deltaTime;
        }
    }

    public void StartTimer()
    {
        onEvacuate = false;
        currTime = 0f;
        disasterTime = Random.Range(minTimeDisaster, maxTimeDisaster);
        disasterType = DisasterType.None;
    }

    private void RandomizerDisaster()
    {
        int x = Random.Range(0, 3);
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
        //chart naik
        yield return new WaitForSeconds(beginning_Stage_of_Disaster);
        //mainin animasi disaster
    }

    //Ini dipasang di animasi kena
    public void DisasterHitVillage()
    {
        if (!onEvacuate)
        {
            PopulationDecrease(populationLoss);
            totalPopulationLoss += populationLoss;
        }

        for (int i = 0; i < npcSpawner.childCount; i++)
        {
            NPCScript x = npcSpawner.GetChild(i).gameObject.GetComponent<NPCScript>();
            if (!x.InEvacuationArea)
            {
                totalPopulationLoss++;
                x.NPC_Dead();
            }
        }
    }


    public void Evacuate()
    {
        onEvacuate = true;
        for(int i = 0; i < npcSpawner.childCount; i++)
        {
            npcSpawner.GetChild(i).gameObject.GetComponent<NPCScript>().ChangeState(NPCState.Evacuation);
        }
        EvacuateManager.instance.SetUpEvacuate(disasterType, this);
        Time.timeScale = 0; //Dipause
    }

    public void EvacuateConclusionType(DisasterType x)
    {
        disasterChoosen = x;
        Time.timeScale = 1; //Unpaused
    }

    public void PopulationDecrease(int value)
    {
        population -= value;
        if (population <= 0)
        {
            //kalah
        }
    }

    //Dipanggil di script NPC
    public void NPCEscaped()
    {
        currentNpcEscape++;
        if(currentNpcEscape >= currentNpcShow)
        {
            EvacuateFinished();
        }
    }


    private void CheckEvacuateType()
    {
        if(disasterChoosen == disasterType)
        {
            //bener
        }
        else if (disasterType == DisasterType.None)
        {
            //false alarm
        }
        else
        {
            //salah
        }
    }


    private void EvacuateFinished()
    {
        //DayReportManager.instance.DayReportSetUp(totalPopulationLoss, );
    }


}
