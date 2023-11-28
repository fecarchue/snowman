using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonPress : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        GetComponentInChildren<TextMeshProUGUI>().rectTransform.localPosition = new Vector2(-1, -0.5f);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        GetComponentInChildren<TextMeshProUGUI>().rectTransform.localPosition = new Vector2(-1, 2.5f);
    }
}
