using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.UI;

public class SlotController : MonoBehaviour
{
    private Image frame, image;
    public string condition; // => {None, Top, Bot}

    public Sprite[] framesprites; // => { 0 = ����, 1 = ���õ�, 2 = �������, 3 = �������}
    public Sprite[] imagesprites; // => { 0 = �������, 1 = �������}

    public void Awake()
    {
        
    }

    //�����Ӱ� �̹��� ��������
    public void BringImage()
    {
        condition = "None";
        Transform TransformChild = this.gameObject.transform.Find("Frame");
        frame = TransformChild.GetComponent<Image>();

        TransformChild = this.gameObject.transform.Find("Image");
        image = TransformChild.GetComponent<Image>();

        image.sprite = imagesprites[1]; //�������
        frame.sprite = framesprites[3]; //�׵θ� ����
    }
    
    //������ �����ϴ� �Լ�
    public void Empty()
    {
        frame.sprite = framesprites[3];

    }
    //������ �����ϴ� �Լ�
    public void NonClicked()
    {
        frame.sprite = framesprites[2];
    }

    //������ �����ϴ� �Լ�
    public void Clicked()
    {
        frame.sprite = framesprites[1];
    }

    //������ �����ϴ� �Լ�
    public void Used()
    {
        frame.sprite = framesprites[0];
    }

    //�̹��� �����ϴ� �Լ�
    public void exist()
    {
        image.sprite = imagesprites[0];
    }

    //�̹��� �����ϴ� �Լ�
    public void nonexist()
    {
        image.sprite = imagesprites[1];
    }
}