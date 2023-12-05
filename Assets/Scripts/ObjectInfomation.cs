using UnityEngine;
using UnityEngine.UI;

public class ObjectInfomation : MonoBehaviour
{
    public GameObject InfoWindow;
    public bool Active = false;
    
    public void ActiveWindow()
    {  
        Active = !Active;
        InfoWindow.SetActive(Active);
    }

    public void MakeObject(string[] objects)
    {
        int id = 0;
        string name;
        GameObject newObject;

        //있던 오브젝트들 제거
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        //오브젝트 추가
        foreach (string obj in objects)
        {
            name = "obj_" + id;
            newObject = new GameObject(name);
            newObject.transform.SetParent(InfoWindow.transform);
            RectTransform rectTransform = newObject.AddComponent<RectTransform>();
            rectTransform.localScale = new Vector3 (1f, 1f, 1f);
            Image imageComponent = newObject.AddComponent<Image>();

            if (obj == "tree")
            {
                imageComponent.color = Color.red;
            }
            else if(obj == "rock")
            {
                imageComponent.color = Color.blue;
            }
            id++;
        }
    }
}
