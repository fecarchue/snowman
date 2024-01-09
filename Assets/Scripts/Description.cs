using UnityEngine;
using UnityEngine.EventSystems;

public class Description : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameObject description;
    private RectTransform DescriptionPosition,ButtonPosition;
    private GameObject Button;
    private bool first;

    private void Awake()
    {
        first = true;
    }

    //��ư ������
    public void OnPointerDown(PointerEventData eventData)
    {
        if(first)
        {
            Button = EventSystem.current.currentSelectedGameObject;

            DescriptionPosition = description.GetComponent<RectTransform>();
            ButtonPosition = Button.GetComponent<RectTransform>();

            DescriptionPosition.localPosition = ButtonPosition.localPosition + new Vector3(1000, 0, 0);
        }
        first = false;
        description.SetActive(true);
    }

    //��ư���� �� ����
    public void OnPointerUp(PointerEventData eventData)
    {
        first = true;
        description.SetActive(false);
    }
}
