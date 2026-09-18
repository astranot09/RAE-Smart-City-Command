using UnityEngine;
using UnityEngine.UI;

public class UIGridRenderer : Graphic
{
    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();
    }
}
