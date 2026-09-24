using UnityEngine;
using System.Collections.Generic;

public class GasSensorSimulator : MonoBehaviour
{
    public UIGraphLine graphLine;

    [Header("Settings")]
    public int maxPoints = 60;
    public float flatNoise = 0.1f;      // getaran kecil pas normal (biar gak flat sempurna)
    public float riseSpeed = 0.05f;     // seberapa cepat naik pas alert
    public float maxValue = 8f;         // batas atas biar gak infinite naik

    [Header("Trigger")]
    public bool isAlerting = false;     // toggle ini dari script lain / Inspector buat testing

    private List<float> values = new List<float>();
    private float currentLevel = 0f;

    void Start()
    {
        // isi awal flat
        for (int i = 0; i < maxPoints; i++)
            values.Add(0f);
    }

    void Update()
    {
        float newValue;

        if (isAlerting)
        {
            // naik pelan-pelan, gak lompat
            currentLevel += riseSpeed;
            currentLevel = Mathf.Min(currentLevel, maxValue);
            newValue = currentLevel + Random.Range(-flatNoise, flatNoise);
        }
        else
        {
            // normal: flat + noise kecil doang
            currentLevel = Mathf.Max(0f, currentLevel - riseSpeed * 0.5f); // turun pelan kalau alert berhenti
            newValue = currentLevel + Random.Range(-flatNoise, flatNoise);
        }

        values.Add(newValue);
        if (values.Count > maxPoints) values.RemoveAt(0);

        graphLine.SetValues(values);
    }
}