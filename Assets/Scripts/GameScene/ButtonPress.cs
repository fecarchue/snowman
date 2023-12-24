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
        text.rectTransform.localPosition = new Vector2(-1, -0.5f);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        text.rectTransform.localPosition = new Vector2(-1, 2.5f);
    }
}
