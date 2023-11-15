using Unity.VisualScripting.ReorderableList;
using UnityEngine;
using UnityEngine.UI;

public class SlotController : MonoBehaviour
{
    private Image image;

    [SerializeField]
    private Sprite[] sprites;

    private void Awake()
    {
        Transform TransformChild = this.gameObject.transform.Find("Frame");
        image = TransformChild.GetComponent<Image>();
        image.sprite = sprites[0];
    }
    
    public void NonClicked()
    {
        image.sprite = sprites[0];
    }

    public void Clicked()
    {
        image.sprite = sprites[1];
    }
    
    public void Used()
    {
        image.sprite = sprites[2];
    }
}