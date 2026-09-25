using System.Collections.Generic;
using UnityEngine;

public class SeismographSimulator : MonoBehaviour
{
    public UIGraphLine graphLine;
    public int maxPoints = 60;
    public float baseNoise = 5f;
    public float spikeChance = 0.02f;
    public float spikeIntensity = 40f;

    private List<float> values = new List<float>();
    private bool isSpiking = false;
    private int spikeTimer = 0;
    private int spikeDuration = 3; // durasi spike pas NORMAL (jumlah titik)

    public float updateInterval = 0.1f;
    private float timer = 0f;

    [Header("Trigger")]
    public bool isAlerting = false;

    private void Start()
    {
        InitSeimograph();
    }

    public void InitSeimograph()
    {
        for (int i = 0; i < maxPoints; i++)
        {
            values.Add(Random.Range(-baseNoise, baseNoise));
        }
        if (graphLine == null) return;
        graphLine.SetValues(values);
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= updateInterval)
        {
            timer = 0f;

            float value = GenerateNextValue();
            values.Add(value);
            if (values.Count > maxPoints) values.RemoveAt(0);
            if (graphLine == null) return;
            graphLine.SetValues(values);
        }
    }

    float GenerateNextValue()
    {
        // MODE ALERT: selalu spiky, gak pernah berhenti
        if (isAlerting)
        {
            return Mathf.Sin(Time.time * 40f) * spikeIntensity
                 + Random.Range(-2f, 2f);
        }

        // MODE NORMAL: ada chance random buat mulai spike pendek
        if (!isSpiking && Random.value < spikeChance)
        {
            isSpiking = true;
            spikeTimer = spikeDuration; // cuma 3 titik (bukan 5-10 kayak sebelumnya)
        }

        if (isSpiking)
        {
            spikeTimer--;
            if (spikeTimer <= 0) isSpiking = false;

            // decay makin kecil seiring habisnya durasi, biar transisi halus
            float decay = (float)spikeTimer / spikeDuration;
            return Mathf.Sin(Time.time * 40f) * spikeIntensity * decay
                 + Random.Range(-2f, 2f);
        }
        else
        {
            return Random.Range(-baseNoise, baseNoise);
        }
    }
}