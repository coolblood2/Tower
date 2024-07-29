using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class CircleRenderer : MonoBehaviour
{
    private LineRenderer lineRenderer;
    public int segments = 50;
    public float radius = 3.0f;  // Set the radius here

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.useWorldSpace = false;
        lineRenderer.loop = true;
        CreatePoints();
    }

    void CreatePoints()
    {
        lineRenderer.positionCount = segments + 1;
        float angle = 360f / segments;

        for (int i = 0; i <= segments; i++)
        {
            float x = Mathf.Sin(Mathf.Deg2Rad * angle * i) * radius;
            float y = Mathf.Cos(Mathf.Deg2Rad * angle * i) * radius;
            lineRenderer.SetPosition(i, new Vector3(x, y, 0)); // Ensure Z position is 0 for 2D
        }
    }
}
