using UnityEngine;

public class ObjectInfomation : MonoBehaviour
{
    public GameObject InfoWindow, objectPrefab;
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
            if (obj == "tree")
            {
                name = "obj_" + id;
                newObject = new GameObject(name);
                SpriteRenderer spriteRenderer = newObject.AddComponent<SpriteRenderer>();
                spriteRenderer.sprite = Tree;
            }
            else if(obj == "rock")
            {
                name = "obj_" + id;
                newObject = new GameObject(name);
                SpriteRenderer spriteRenderer = newObject.AddComponent<SpriteRenderer>();
                spriteRenderer.sprite = Rock;
            }
            id++;
        }
    }
}
