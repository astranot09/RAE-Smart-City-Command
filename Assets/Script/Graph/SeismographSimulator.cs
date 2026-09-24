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

    public float updateInterval = 0.1f; // Ubah nilai ini untuk mengatur kecepatan (misal: 0.1 detik)
    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= updateInterval)
        {
            timer = 0f;

            float value = GenerateNextValue();
            values.Add(value);
            if (values.Count > maxPoints) values.RemoveAt(0);

            graphLine.SetValues(values);
        }
    }

    float GenerateNextValue()
    {
        if (!isSpiking && Random.value < spikeChance)
        {
            isSpiking = true;
            spikeTimer = Random.Range(15, 30);
        }

        if (isSpiking)
        {
            spikeTimer--;
            if (spikeTimer <= 0) isSpiking = false;

            float decay = spikeTimer / 30f;
            return Mathf.Sin(Time.time * 40f) * spikeIntensity * decay
                 + Random.Range(-2f, 2f);
        }
        else
        {
            return Random.Range(-baseNoise, baseNoise);
        }
    }
}