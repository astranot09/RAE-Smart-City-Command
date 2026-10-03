using System.Collections.Generic;
using UnityEngine;

public class BuoySimulator : MonoBehaviour
{
    [Header("BuoyA")]
    [SerializeField] private UIGraphLine graphLineBuoyA;

    [Header("Buoy B")]
    [SerializeField] private UIGraphLine graphLineBuoyB;

    [Header("Setting")]
    public int pointCount = 50;
    public float baseAmplitude = 1f;   // Gelombang kecil normal
    public float bumpAmplitude = 5f;   // Tinggi bump besar
    public float frequency = 1f;
    public float scrollSpeed = 1.5f;

    [Header("Trigger & Transition")]
    public bool isAlertingBuoyA = false;
    public bool isAlertingBuoyB = false;
    public float alertAmplitude = 3f;
    public float transitionSpeed = 2f; // Kecepatan perubahan amplitude

    private List<float> valuesBuoyA = new List<float>();
    private List<float> valuesBuoyB = new List<float>();
    private float bumpTimerBuoyA = 0f;
    private float bumpPositionBuoyA = -10f; // Posisi bump di sepanjang x
    private float bumpTimerBuoyB = 0f;
    private float bumpPositionBuoyB = -10f; // Posisi bump di sepanjang x

    [Header("Reference")]
    [SerializeField] private OutpostManager outpostManager;
    [SerializeField] private DisasterManager disasterManager;
    [SerializeField] private GraphManager graphManager;


    [Header("Upgrade")]
    [SerializeField] private int level = 0;
    private bool buoyB_Unlocked = false;

    // Variabel untuk menyimpan amplitude saat ini
    private float currentAmplitudeBuoyA;
    private float currentAmplitudeBuoyB;



    private void OnEnable()
    {
        outpostManager.DisasterManager.earlyStageOfDisasterEvent += DisasterFirstAlarmTrigger;
        outpostManager.DisasterManager.secondStageOfDisasterEvent += DisasterSecondAlarmTrigger;
        outpostManager.DisasterManager.thirdStageOfDisasterEvent += DisasterThirdAlarmTrigger;
        outpostManager.cycleStart += CloseAllAlarmDisaster;
    }
    private void OnDisable()
    {
        outpostManager.DisasterManager.earlyStageOfDisasterEvent -= DisasterFirstAlarmTrigger;
        outpostManager.DisasterManager.secondStageOfDisasterEvent -= DisasterSecondAlarmTrigger;
        outpostManager.DisasterManager.thirdStageOfDisasterEvent -= DisasterThirdAlarmTrigger;
        outpostManager.cycleStart -= CloseAllAlarmDisaster;
    }








    void Start()
    {
        if (graphLineBuoyA == null || graphLineBuoyB) return;

        // Atur amplitude awal sesuai status isAlerting
        currentAmplitudeBuoyA = isAlertingBuoyA ? alertAmplitude : baseAmplitude;
        currentAmplitudeBuoyB = isAlertingBuoyB ? alertAmplitude : baseAmplitude;

        InitBuoy();
    }

    public void SetUp(UIGraphLine buoyA, UIGraphLine buoyB)
    {
        graphLineBuoyA = buoyA;
        graphLineBuoyB = buoyB;
    }

    public void InitBuoy()
    {
        //valuesBuoyA.Clear();
        //valuesBuoyB.Clear();
        float t = Time.time * scrollSpeed;

        for (int i = 0; i < pointCount; i++)
        {
            float x = i * 0.2f;
            float baseWaveBuoyA = Mathf.Sin(x + t) * currentAmplitudeBuoyA;
            float baseWaveBuoyB = Mathf.Sin(x + t) * currentAmplitudeBuoyB;
            valuesBuoyA.Add(baseWaveBuoyA);
            valuesBuoyB.Add(baseWaveBuoyB);
        }

        graphLineBuoyA.SetValues(valuesBuoyA);
        if (graphLineBuoyB == null || !buoyB_Unlocked) return;
        graphLineBuoyB.SetValues(valuesBuoyB);
    }

    void Update()
    {
        // 1. Tentukan target amplitude berdasarkan status isAlerting
        float targetAmplitudeBuoyA = isAlertingBuoyA ? alertAmplitude : baseAmplitude;
        float targetAmplitudeBuoyB = isAlertingBuoyB ? alertAmplitude : baseAmplitude;

        // 2. Transisi mulus nilai currentAmplitude menuju targetAmplitude
        currentAmplitudeBuoyA = Mathf.Lerp(currentAmplitudeBuoyA, targetAmplitudeBuoyA, Time.deltaTime * transitionSpeed);
        currentAmplitudeBuoyB = Mathf.Lerp(currentAmplitudeBuoyB, targetAmplitudeBuoyB, Time.deltaTime * transitionSpeed);

        valuesBuoyA.Clear();
        valuesBuoyB.Clear();

        float t = Time.time * scrollSpeed;

        // Trigger bump baru sesekali
        bumpTimerBuoyA -= Time.deltaTime;
        bumpTimerBuoyB -= Time.deltaTime;
        if (bumpTimerBuoyA <= 0f)
        {
            bumpTimerBuoyA = Random.Range(4f, 8f);
            bumpPositionBuoyA = 0f;
        }

        if (bumpTimerBuoyB <= 0f)
        {
            bumpTimerBuoyB = Random.Range(4f, 8f);
            bumpPositionBuoyA = 0f;
        }

        for (int i = 0; i < pointCount; i++)
        {
            float x = i * 0.2f;

            // Gunakan currentAmplitude yang sudah mengalami transisi Lerp
            float baseWaveBuoyA = Mathf.Sin(x + t) * currentAmplitudeBuoyA;
            float baseWaveBuoyB = Mathf.Sin(x + t) * currentAmplitudeBuoyA;


            // Bump: gelombang gaussian
            float distA = x - (bumpPositionBuoyA + t);
            float bumpA = bumpAmplitude * Mathf.Exp(-distA * distA * 0.5f);

            // Bump: gelombang gaussian
            float distB = x - (bumpPositionBuoyB + t);
            float bumpB = bumpAmplitude * Mathf.Exp(-distB * distB * 0.5f);

            valuesBuoyA.Add(baseWaveBuoyA + bumpA);
            valuesBuoyB.Add(baseWaveBuoyB + bumpB);
        }

        if (graphLineBuoyA == null) return;
        graphLineBuoyA.SetValues(valuesBuoyA);
        if (graphLineBuoyB == null || !buoyB_Unlocked) return;
        graphLineBuoyB.SetValues(valuesBuoyB);
    }


    public void DisasterFirstAlarmTrigger()
    {
        if(outpostManager.DisasterManager.CurrentDisaster != DisasterType.Tsunami) return;
        if(level >= 2)
        {
            isAlertingBuoyB = true;
        }
        if(level >= 3)
        {
            isAlertingBuoyA = true;
        }

    }
    public void DisasterSecondAlarmTrigger()
    {
        if (outpostManager.DisasterManager.CurrentDisaster != DisasterType.Tsunami) return;
        if (level >= 1 && !isAlertingBuoyA)
        {
            isAlertingBuoyA = true;
        }
    }
    public void DisasterThirdAlarmTrigger()
    {
        if (outpostManager.DisasterManager.CurrentDisaster != DisasterType.Tsunami) return;
        if (!isAlertingBuoyA)
        {
            isAlertingBuoyA = true;
        }
    }
    public void DisasterFinalAlarmTrigger()
    {
        if (outpostManager.DisasterManager.CurrentDisaster != DisasterType.Tsunami) return;
        Debug.Log("Final");
    }

    public void CloseAllAlarmDisaster()
    {
        isAlertingBuoyA = false;
        isAlertingBuoyB = false;
    }
    public void ResetUIBuoyGraph()
    {
        graphLineBuoyA = null;
        graphLineBuoyB = null;
    }
    public void Upgrade()
    {
        level++;
        switch (level)
        {
            case 2:
                buoyB_Unlocked = true;
                break;
        }
    }
}
