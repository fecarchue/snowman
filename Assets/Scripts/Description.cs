using UnityEngine;
using UnityEngine.EventSystems;

public class Description : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameObject description;
    
    //버튼 누를때
    public void OnPointerDown(PointerEventData eventData)
    {
        description.SetActive(true);
    }

    //버튼에서 손 땔때
    public void OnPointerUp(PointerEventData eventData)
    {
        description.SetActive(false);
    }

}
