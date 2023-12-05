using UnityEngine;
using UnityEngine.UI;

public class ObjectInfomation : MonoBehaviour
{
    public GameObject InfoWindow, closebutton, SampleTree, SampleRock;
    public bool Active = false;
    
    public void ActiveWindow()
    {  
        //Info â ������ ����
        Active = !Active;
        InfoWindow.SetActive(Active);
        closebutton.SetActive(Active);
    }

    public void MakeObject(string[] objects)
    {
        //�ִ� ������Ʈ�� ����
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        //������Ʈ �߰�
        foreach (string obj in objects)
        {
            //���� ������Ʈ �����ؼ� ����
            if (obj == "tree")
            {
                GameObject newObject = Instantiate(SampleTree);
                newObject.transform.SetParent(InfoWindow.transform);
                RectTransform rectTransform = newObject.GetComponent<RectTransform>();
                rectTransform.localScale = new Vector3(1f, 1f, 1f);
            }
            else if(obj == "rock")
            {
                GameObject newObject = Instantiate(SampleRock);
                newObject.transform.SetParent(InfoWindow.transform);
                RectTransform rectTransform = newObject.GetComponent<RectTransform>();
                rectTransform.localScale = new Vector3(1f, 1f, 1f);
            }
        }
    }
}
