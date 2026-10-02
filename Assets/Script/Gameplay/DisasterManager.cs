using System;
using System.Collections;
using UnityEngine;

public class DisasterManager : MonoBehaviour
{
    [Header("Reference")]
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
    public DisasterType DisasterChoosen => disasterChoosen;

    [Header("Setting")]
    [SerializeField] private float beginning_Stage_of_Disaster = 2f;
    [SerializeField] private float second_Stage_of_Disaster = 2f;
    [SerializeField] private float third_Stage_of_Disaster = 1f;

    [Header("Animator")]
    [SerializeField] private Animator animator;

    public event Action earlyStageOfDisasterEvent;
    public event Action secondStageOfDisasterEvent;
    public event Action thirdStageOfDisasterEvent;
    public event Action finalStageOfDisasterEvent;

    public event Action onDisasterHitOutpost;

    private void OnEnable()
    {
        outpostManager.cycleStart += StartCycleOfDisaster;
    }

    private void OnDisable()
    {
        outpostManager.cycleStart -= StartCycleOfDisaster;
    }

    private void Start()
    {
        if(animator == null)
            animator = GetComponent<Animator>();
    }

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

    private void StartCycleOfDisaster()
    {
        currTime = 0f;
        disasterChoosen = DisasterType.None;

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
        earlyStageOfDisasterEvent?.Invoke();
        yield return new WaitForSeconds(beginning_Stage_of_Disaster/2);

        //chart naik (versi upgrade)
        secondStageOfDisasterEvent?.Invoke();
        yield return new WaitForSeconds(beginning_Stage_of_Disaster/2);

        //chart naik (versi normal)
        thirdStageOfDisasterEvent?.Invoke();
        yield return new WaitForSeconds(second_Stage_of_Disaster);

        //Predicted status muncul
        finalStageOfDisasterEvent?.Invoke();
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

    public void DisasterHitVillage()
    {
        onDisasterHitOutpost?.Invoke();
    }
    public void EvacuateConclusionType(DisasterType x)
    {
        disasterChoosen = x;
        Time.timeScale = 1; //Unpaused
    }

}
