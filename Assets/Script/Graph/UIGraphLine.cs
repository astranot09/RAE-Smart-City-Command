using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("UI/Graph Line")]
public class UIGraphLine : Graphic
{
    public List<float> values = new List<float>();
    public float lineThickness = 3f;
    public float valueScale = 50f;   // seberapa tinggi grafik merespons value
    public float minY = 0f;          // offset vertikal tengah garis

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();
        if (values == null || values.Count < 2) return;

        Rect rect = rectTransform.rect;
        float width = rect.width;
        float step = width / (values.Count - 1);

        // convert semua value jadi posisi titik di local space
        Vector2[] points = new Vector2[values.Count];
        for (int i = 0; i < values.Count; i++)
        {
            float x = rect.xMin + i * step;
            float rawY = minY + values[i] * valueScale;

            // clamp biar gak keluar dari tinggi rect
            float halfHeight = rect.height / 2f;
            float y = Mathf.Clamp(rawY, -halfHeight, halfHeight);

            points[i] = new Vector2(x, y);
        }

        // gambar tiap segmen sebagai quad tipis
        for (int i = 0; i < points.Length - 1; i++)
        {
            DrawSegment(vh, points[i], points[i + 1], i * 4);
        }
    }

    void DrawSegment(VertexHelper vh, Vector2 p0, Vector2 p1, int startIndex)
    {
        Vector2 dir = (p1 - p0).normalized;
        Vector2 normal = new Vector2(-dir.y, dir.x) * (lineThickness * 0.5f);

        UIVertex v = UIVertex.simpleVert;
        v.color = color;

        v.position = p0 - normal; vh.AddVert(v);
        v.position = p0 + normal; vh.AddVert(v);
        v.position = p1 + normal; vh.AddVert(v);
        v.position = p1 - normal; vh.AddVert(v);

        vh.AddTriangle(startIndex, startIndex + 1, startIndex + 2);
        vh.AddTriangle(startIndex, startIndex + 2, startIndex + 3);
    }

    public void SetValues(List<float> newValues)
    {
        values = newValues;
        SetVerticesDirty(); // trigger redraw
    }
}
