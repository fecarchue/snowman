using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectInfomation : MonoBehaviour
{
    public GameObject InfoWindow, closebutton, SampleStar, SampleRock;

    public void MakeObject(List<int> objects)
    {
        //�ִ� ������Ʈ�� ����
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        //������Ʈ �߰�
        foreach (int obj in objects)
        {
            //���� ������Ʈ �����ؼ� ����
            if (obj / 10000 == 21)
            {
                GameObject newObject = Instantiate(SampleStar);
                newObject.transform.SetParent(InfoWindow.transform);
                RectTransform rectTransform = newObject.GetComponent<RectTransform>();
                rectTransform.localScale = new Vector3(5f, 5f, 1f);
            }
            else if(obj == 654321)
            {
                GameObject newObject = Instantiate(SampleRock);
                newObject.transform.SetParent(InfoWindow.transform);
                RectTransform rectTransform = newObject.GetComponent<RectTransform>();
                rectTransform.localScale = new Vector3(1f, 1f, 1f);
            }
        }
    }
}
