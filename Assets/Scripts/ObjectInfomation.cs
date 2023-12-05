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

        //�ִ� ������Ʈ�� ����
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        //������Ʈ �߰�
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
