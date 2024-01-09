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

    //버튼 누를때
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

    //버튼에서 손 땔때
    public void OnPointerUp(PointerEventData eventData)
    {
        first = true;
        description.SetActive(false);
    }
}
