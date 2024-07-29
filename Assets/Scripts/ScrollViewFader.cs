using UnityEngine;
using UnityEngine.UI;

public class ScrollViewFader : MonoBehaviour
{
    public RectTransform scrollViewTransform;
    public float fadeHeight = 50f; // Height over which the buttons will fade out

    void Update()
    {
        // Loop through each child of the content (i.e., each button)
        for (int i = 0; i < transform.childCount; i++)
        {
            RectTransform childRectTransform = transform.GetChild(i).GetComponent<RectTransform>();
            CanvasGroup canvasGroup = transform.GetChild(i).GetComponent<CanvasGroup>();

            if (childRectTransform != null && canvasGroup != null)
            {
                // Calculate the child's position relative to the top of the scroll view
                Vector3 worldPosition = childRectTransform.position;
                Vector3 viewportPosition = scrollViewTransform.InverseTransformPoint(worldPosition);

                float fadeAmount = Mathf.Clamp01((fadeHeight - viewportPosition.y) / fadeHeight);
                canvasGroup.alpha = 1.0f - fadeAmount;
            }
        }
    }
}
