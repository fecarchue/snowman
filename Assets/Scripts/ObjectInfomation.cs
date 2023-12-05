using UnityEngine;
using UnityEngine.UI;

public class ObjectInfomation : MonoBehaviour
{
    public GameObject InfoWindow, closebutton, SampleTree, SampleRock;
    public bool Active = false;
    
    public void ActiveWindow()
    {  
        //Info 창 켜지고 꺼짐
        Active = !Active;
        InfoWindow.SetActive(Active);
        closebutton.SetActive(Active);
    }

    public void MakeObject(string[] objects)
    {
        //있던 오브젝트들 제거
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        //오브젝트 추가
        foreach (string obj in objects)
        {
            //샘플 오브젝트 복사해서 생성
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
