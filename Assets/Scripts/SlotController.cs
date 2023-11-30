using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.UI;

public class SlotController : MonoBehaviour
{
    private Image frame, image;
    public string condition; // => {None, Top, Bot}

    public Sprite[] framesprites; // => { 0 = 사용됨, 1 = 선택됨, 2 = 들어있음, 3 = 비어있음}
    public Sprite[] imagesprites; // => { 0 = 들어있음, 1 = 비어있음}

    public void Awake()
    {
        
    }

    //프레임과 이미지 가져오기
    public void BringImage()
    {
        condition = "None";
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