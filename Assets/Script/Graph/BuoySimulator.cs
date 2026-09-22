using System.Collections.Generic;
using UnityEngine;

public class BuoySimulator : MonoBehaviour
{
    public UIGraphLine graphLine;
    public int pointCount = 50;
    public float baseAmplitude = 1f;   // gelombang kecil normal
    public float bumpAmplitude = 5f;   // tinggi bump besar
    public float frequency = 1f;
    public float scrollSpeed = 1.5f;

    private List<float> values = new List<float>();
    private float bumpTimer = 0f;
    private float bumpPosition = -10f; // posisi bump di sepanjang x

    void Update()
    {
        values.Clear();
        float t = Time.time * scrollSpeed;

        // trigger bump baru sesekali
        bumpTimer -= Time.deltaTime;
        if (bumpTimer <= 0f)
        {
            bumpTimer = Random.Range(4f, 8f);
            bumpPosition = 0f; // bump mulai dari titik awal
        }

        for (int i = 0; i < pointCount; i++)
        {
            float x = i * 0.2f;
            float baseWave = Mathf.Sin(x + t) * baseAmplitude;

            // bump: gelombang gaussian yang lewat sepanjang grafik
            float dist = x - (bumpPosition + t);
            float bump = bumpAmplitude * Mathf.Exp(-dist * dist * 0.5f);

            values.Add(baseWave + bump);
        }

        graphLine.SetValues(values);
    }
}
