using UnityEngine;
using System.Collections.Generic;

public class GasSensorSimulator : MonoBehaviour
{
    public UIGraphLine graphLine;

    [Header("Settings")]
    public int maxPoints = 60;
    public float flatNoise = 0.1f;
    public float riseSpeed = 0.05f;
    public float maxValue = 8f;

    [Header("Trigger (controlled by external script)")]
    public bool isAlerting = false; // HANYA dibaca, diubah dari luar

    [Header("False Alarm (independent random event)")]
    public float falseAlarmCheckInterval = 3f;  // tiap berapa detik dicek chance-nya
    [Range(0f, 1f)]
    public float falseAlarmChance = 0.2f;       // chance muncul tiap interval
    public float falseAlarmRiseAmount = 3f;     // seberapa tinggi blip-nya
    public float falseAlarmDuration = 0.5f;     // lama fase naik
    public float falseAlarmFallSpeed = 0.15f;

    private List<float> values = new List<float>();
    private float currentLevel = 0f;

    // state internal false alarm
    private bool isFalseAlarmActive = false;
    private float falseAlarmTimer = 0f;
    private float falseAlarmCheckTimer = 0f;

    [Header("Reference")]
    [SerializeField] private OutpostManager outpostManager;

    [Header("Upgrade")]
    [SerializeField] private int level = 0;
    [SerializeField] private float flatNoiseUpgrade = 0.15f;
    [SerializeField] private GameObject statusGameObject;
    [SerializeField] private bool statusUnlock;

    void Start()
    {
        for (int i = 0; i < maxPoints; i++)
            values.Add(0f);
    }

    void Update()
    {
        HandleFalseAlarmRoll();

        float newValue = ProcessLevel();

        values.Add(newValue);
        if (values.Count > maxPoints) values.RemoveAt(0);
        if (graphLine == null) return;
        graphLine.SetValues(values);
    }

    void HandleFalseAlarmRoll()
    {
        // false alarm cuma boleh muncul pas gak lagi alert asli
        if (isAlerting || isFalseAlarmActive) return;

        falseAlarmCheckTimer += Time.deltaTime;
        if (falseAlarmCheckTimer >= falseAlarmCheckInterval)
        {
            falseAlarmCheckTimer = 0f;

            if (Random.value < falseAlarmChance)
            {
                isFalseAlarmActive = true;
                falseAlarmTimer = 0f;
            }
        }
    }

    float ProcessLevel()
    {
        if (isAlerting)
        {
            // ALERT ASLI: naik terus, gak peduli false alarm sama sekali
            currentLevel += riseSpeed;
            currentLevel = Mathf.Min(currentLevel, maxValue);
        }
        else if (isFalseAlarmActive)
        {
            falseAlarmTimer += Time.deltaTime;

            if (falseAlarmTimer < falseAlarmDuration)
            {
                // fase naik dikit
                currentLevel += riseSpeed * 2f; // sedikit lebih cepat biar 0.5 detik cukup buat blip
                currentLevel = Mathf.Min(currentLevel, falseAlarmRiseAmount);
            }
            else
            {
                // fase turun
                currentLevel = Mathf.Max(0f, currentLevel - falseAlarmFallSpeed);
                if (currentLevel <= 0.01f) isFalseAlarmActive = false; // selesai, balik normal
            }
        }
        else
        {
            // normal total: turun pelan ke 0
            currentLevel = Mathf.Max(0f, currentLevel - riseSpeed * 0.5f);
        }

        return currentLevel + Random.Range(-flatNoise, flatNoise);
    }

    public void Upgrade()
    {
        level++;
        switch (level)
        {
            case 1:
                falseAlarmChance = 0;
                break;
            case 2:
                flatNoise = flatNoiseUpgrade;
                break;
            case 3:
                statusUnlock = true;
                break;
        }
    }
}