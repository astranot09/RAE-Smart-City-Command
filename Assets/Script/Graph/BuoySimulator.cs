using System.Collections.Generic;
using UnityEngine;

public class BuoySimulator : MonoBehaviour
{
    public UIGraphLine graphLine;
    public int pointCount = 50;
    public float baseAmplitude = 1f;   // Gelombang kecil normal
    public float bumpAmplitude = 5f;   // Tinggi bump besar
    public float frequency = 1f;
    public float scrollSpeed = 1.5f;

    [Header("Trigger & Transition")]
    public bool isAlerting = false;
    public float alertAmplitude = 3f;
    public float transitionSpeed = 2f; // Kecepatan perubahan amplitude

    private List<float> values = new List<float>();
    private float bumpTimer = 0f;
    private float bumpPosition = -10f; // Posisi bump di sepanjang x


    [Header("Reference")]
    [SerializeField] private OutpostManager outpostManager;

    [Header("Upgrade")]
    [SerializeField] private int level = 0;

    // Variabel untuk menyimpan amplitude saat ini
    private float currentAmplitude;

    void Start()
    {
        if (graphLine == null) return;

        // Atur amplitude awal sesuai status isAlerting
        currentAmplitude = isAlerting ? alertAmplitude : baseAmplitude;
        InitBuoy();
    }

    public void InitBuoy()
    {
        values.Clear();
        float t = Time.time * scrollSpeed;

        for (int i = 0; i < pointCount; i++)
        {
            float x = i * 0.2f;
            float baseWave = Mathf.Sin(x + t) * currentAmplitude;
            values.Add(baseWave);
        }

        graphLine.SetValues(values);
    }

    void Update()
    {
        // 1. Tentukan target amplitude berdasarkan status isAlerting
        float targetAmplitude = isAlerting ? alertAmplitude : baseAmplitude;

        // 2. Transisi mulus nilai currentAmplitude menuju targetAmplitude
        currentAmplitude = Mathf.Lerp(currentAmplitude, targetAmplitude, Time.deltaTime * transitionSpeed);

        values.Clear();
        float t = Time.time * scrollSpeed;

        // Trigger bump baru sesekali
        bumpTimer -= Time.deltaTime;
        if (bumpTimer <= 0f)
        {
            bumpTimer = Random.Range(4f, 8f);
            bumpPosition = 0f;
        }

        for (int i = 0; i < pointCount; i++)
        {
            float x = i * 0.2f;

            // Gunakan currentAmplitude yang sudah mengalami transisi Lerp
            float baseWave = Mathf.Sin(x + t) * currentAmplitude;

            // Bump: gelombang gaussian
            float dist = x - (bumpPosition + t);
            float bump = bumpAmplitude * Mathf.Exp(-dist * dist * 0.5f);

            values.Add(baseWave + bump);
        }

        if (graphLine == null) return;
        graphLine.SetValues(values);
    }
}
