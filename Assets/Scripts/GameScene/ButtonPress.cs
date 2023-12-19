using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonPress : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    public void OnPointerDown(PointerEventData eventData)
    {
        GetComponentInChildren<TextMeshProUGUI>().rectTransform.localPosition = new Vector2(-1, -0.5f);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        GetComponentInChildren<TextMeshProUGUI>().rectTransform.localPosition = new Vector2(-1, 2.5f);
    }
}
