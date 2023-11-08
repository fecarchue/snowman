using GG.Infrastructure.Utils.Swipe;
using UnityEngine;

public class Demo : MonoBehaviour
{
    [SerializeField] private SwipeListener swipeListener;

    private float a; // ó�� Ŭ���� x ��ǥ
    private float b; // ó�� Ŭ�� �� ���콺�� ���� ����
    private float q; // ó�� Ŭ�� �� ��ǥ�� ����ġ
    private float w; // ���� ������ ����ġ�� ������ ��

    private void OnEnable()
    {
        swipeListener.OnSwipe.AddListener(OnSwipe);
    }

    private void OnSwipe(string swipe)
    {
        Debug.Log(swipe);
    }

    private void Update()
    {
        // Ŭ���� ��ǥ�� SwipeListener ��ũ��Ʈ���� �޾ƿͼ� ���
        if (Input.GetMouseButtonDown(0))
        {
            a = swipeListener.GetSwipeStartPoint().x; // ó�� Ŭ���� x ��ǥ
            b = 0f; // ó�� Ŭ�� �� ���콺�� ���� ���� �ʱ�ȭ
            q = Mathf.Abs(a - 500); // ����ġ ���
            w = q * b; // ���� ������ ����ġ�� ������ ��

            // ����� ���� ���
            Debug.Log("a: " + a);
            Debug.Log("b: " + b);
            Debug.Log("q: " + q);
            Debug.Log("w: " + w);
        }

        // ���� ���� ����
        if (Input.GetMouseButton(0))
        {
            b = Input.mousePosition.x - a; // ���� ���� ����
            w = q * b; // ����ġ ����

            // ����� ���� ���
            Debug.Log("a: " + a);
            Debug.Log("b: " + b);
            Debug.Log("q: " + q);
            Debug.Log("w: " + w);
        }
    }

    // �ٸ� ��ũ��Ʈ���� a ���� ������ �� �ֵ��� ��ȯ�ϴ� �޼���
    public float GetA()
    {
        return a;
    }

    // �ٸ� ��ũ��Ʈ���� b ���� ������ �� �ֵ��� ��ȯ�ϴ� �޼��带 �߰��� �� �ֽ��ϴ�.
    public float GetB()
    {
        return b;
    }

    // �ٸ� ��ũ��Ʈ���� q ���� ������ �� �ֵ��� ��ȯ�ϴ� �޼��带 �߰��� �� �ֽ��ϴ�.
    public float GetQ()
    {
        return q;
    }

    // �ٸ� ��ũ��Ʈ���� w ���� ������ �� �ֵ��� ��ȯ�ϴ� �޼��带 �߰��� �� �ֽ��ϴ�.
    public float GetW()
    {
        return w;
    }

    private void OnDisable()
    {
        swipeListener.OnSwipe.RemoveListener(OnSwipe);
    }
}
