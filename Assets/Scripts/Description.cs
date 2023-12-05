using UnityEngine;
using UnityEngine.EventSystems;

public class Description : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameObject description;
    
    //��ư ������
    public void OnPointerDown(PointerEventData eventData)
    {
        description.SetActive(true);
    }

    //��ư���� �� ����
    public void OnPointerUp(PointerEventData eventData)
    {
        description.SetActive(false);
    }

}
