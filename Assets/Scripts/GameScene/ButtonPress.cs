using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonPress : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    TextMeshProUGUI text;
    private void Awake()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        text.rectTransform.anchoredPosition = new Vector2(-3, -3);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        text.rectTransform.anchoredPosition = new Vector2(-3, 9);
    }
}
