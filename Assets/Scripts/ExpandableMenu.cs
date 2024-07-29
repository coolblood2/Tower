using UnityEngine;
using UnityEngine.UI;

public class ExpandableMenu : MonoBehaviour
{
    public Button toggleButton; // Assign the button in the inspector
    public RectTransform panelRectTransform;
    public float expandedHeight = 0.3f; // 30% of the screen height
    public float collapsedHeight = 0.05f; // 5% of the screen height
    public float animationDuration = 0.5f; // Duration of the expand/collapse animation

    private bool isExpanded = false;
    private Vector2 expandedSize;
    private Vector2 collapsedSize;
    private ScrollRect scrollRect;

    void Start()
    {
        if (toggleButton != null)
        {
            toggleButton.onClick.AddListener(ToggleMenu);
        }

        panelRectTransform = GetComponent<RectTransform>();
        collapsedSize = new Vector2(panelRectTransform.rect.width, Screen.height * collapsedHeight);
        expandedSize = new Vector2(panelRectTransform.rect.width, Screen.height * expandedHeight);

        // Initialize the panel to be collapsed
        panelRectTransform.sizeDelta = collapsedSize;
        panelRectTransform.anchoredPosition = new Vector2(0, collapsedSize.y / 2);

        scrollRect = GetComponentInChildren<ScrollRect>();
        if (scrollRect != null)
        {
            scrollRect.gameObject.SetActive(isExpanded);
        }
    }

    void ToggleMenu()
    {
        isExpanded = !isExpanded;
        StopAllCoroutines();
        StartCoroutine(AnimatePanel(isExpanded ? expandedSize : collapsedSize));
        if (scrollRect != null)
        {
            scrollRect.gameObject.SetActive(isExpanded);
        }
    }

    System.Collections.IEnumerator AnimatePanel(Vector2 targetSize)
    {
        Vector2 initialSize = panelRectTransform.sizeDelta;
        Vector2 initialPosition = panelRectTransform.anchoredPosition;
        Vector2 targetPosition = new Vector2(0, targetSize.y / 2);

        float elapsedTime = 0;

        while (elapsedTime < animationDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / animationDuration);

            panelRectTransform.sizeDelta = Vector2.Lerp(initialSize, targetSize, t);
            panelRectTransform.anchoredPosition = Vector2.Lerp(initialPosition, targetPosition, t);

            yield return null;
        }

        panelRectTransform.sizeDelta = targetSize;
        panelRectTransform.anchoredPosition = targetPosition;
    }
}
