using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.UI;

public class SlotController : MonoBehaviour
{
    private Image frame, image;
    public bool isUsed;
    public string condition = "None"; //{None, Top, Bot}

    [SerializeField]
    private Sprite[] framesprites; // => {사용됨, 선택됨, 들어있음, 비어있음}
    [SerializeField]
    private Sprite[] imagesprites; // => {들어있음, 비어있음}

    //프레임과 이미지 가져오기
    private void Awake()
    {
        Transform TransformChild = this.gameObject.transform.Find("Frame");
        frame = TransformChild.GetComponent<Image>();

        TransformChild = this.gameObject.transform.Find("Image");
        image = TransformChild.GetComponent<Image>();

        image.sprite = imagesprites[1]; //비어있음
        frame.sprite = framesprites[3]; //테두리 없음
    }
    
    //프레임 변경하는 함수
    public void Empty()
    {
        frame.sprite = framesprites[3];
    }
    //프레임 변경하는 함수
    public void NonClicked()
    {
        frame.sprite = framesprites[2];
    }

    //프레임 변경하는 함수
    public void Clicked()
    {
        frame.sprite = framesprites[1];
    }

    //프레임 변경하는 함수
    public void Used()
    {
        frame.sprite = framesprites[0];
    }

    //이미지 변경하는 함수
    public void exist()
    {
        image.sprite = imagesprites[0];
    }

    //이미지 변경하는 함수
    public void nonexist()
    {
        image.sprite = imagesprites[1];
    }
}