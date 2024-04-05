using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectInfomation : MonoBehaviour
{
    public GameObject InfoWindow, closebutton, SampleStar, SampleRock;

    public void MakeObject(List<int> objects)
    {
        //있던 오브젝트들 제거
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        //오브젝트 추가
        foreach (int obj in objects)
        {
            //샘플 오브젝트 복사해서 생성
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
