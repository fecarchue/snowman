using UnityEngine;
using UnityEngine.UI;

public class ObjectInfomation : MonoBehaviour
{
    public GameObject InfoWindow, objectPrefab, objectInfo;
    public Sprite Tree, Rock;
    public Transform parentObject;
    public bool Active;
    private void Awake()
    {
        Active = false;
        InfoWindow.SetActive(false);
    }
    
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
        foreach (string obj in objects)
        {
            name = "obj_" + id;
            newObject = new GameObject(name);
            newObject.transform.SetParent(objectInfo.transform);
            RectTransform rectTransform = newObject.AddComponent<RectTransform>();
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
