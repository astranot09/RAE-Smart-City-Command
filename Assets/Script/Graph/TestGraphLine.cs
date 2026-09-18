using UnityEngine;

public class TestGraphLine : MonoBehaviour
{
    public UIGraphLine graphLine;

    void Start()
    {
        graphLine.color = Color.cyan; // biar pasti keliatan
        graphLine.SetValues(new System.Collections.Generic.List<float>
        {
            0f, 2f, -1f, 5f, 3f, -4f, 6f, 1f, 0f
        });
    }
}