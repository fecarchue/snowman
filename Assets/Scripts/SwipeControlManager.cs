using GG.Infrastructure.Utils.Swipe;
using UnityEngine;

public class SwipeControlManager : MonoBehaviour
{
    [SerializeField] private SwipeListener swipeListener;

    private float rawtouch; // ó�� Ŭ���� x ��ǥ
    private float rawswipe; // ó�� Ŭ�� �� ���콺�� ���� ����
    private float realtouch; // ó�� Ŭ�� �� ��ǥ�� ����ġ
    private float realswipe; // ���� ������ ����ġ�� ������ ��

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
            rawtouch = swipeListener.GetSwipeStartPoint().x; // ó�� Ŭ���� x ��ǥ
            rawswipe = 0f; // ó�� Ŭ�� �� ���콺�� ���� ���� �ʱ�ȭ(Ŭ���� ����->���������� ���� ���)
            realtouch = Mathf.Abs(rawtouch - 540); // �߽����κ��� �Ÿ� Ȯ��(1080x1920�̶� 540�� �߽��Ӥ���)
            realswipe = rawswipe; // ���� ������ ����ġ�� ������ ��

            // ����� ���� ���
            Debug.Log("rawtouch: " + rawtouch);
            Debug.Log("rawswipe: " + rawswipe);
            Debug.Log("realtouch: " + realtouch);
            Debug.Log("realswipe: " + realswipe);
        }

        // ���� ���� ����
        if (Input.GetMouseButton(0))
        {
            rawswipe = Input.mousePosition.x - rawtouch; // ���� ���� ����
            realswipe = rawswipe; // ����ġ ����

            // ����� ���� ���
            Debug.Log("rawtouch: " + rawtouch);
            Debug.Log("rawswipe: " + rawswipe);
            Debug.Log("realtouch: " + realtouch);
            Debug.Log("realswipe: " + realswipe);
        }
    }

    // �ٸ� ��ũ��Ʈ���� rawtouch ���� ������ �� �ֵ��� ��ȯ�ϴ� �޼���
    public float GetRawTouch()
    {
        return rawtouch;
    }

    // �ٸ� ��ũ��Ʈ���� rawswipe ���� ������ �� �ֵ��� ��ȯ�ϴ� �޼��带 �߰��� �� �ֽ��ϴ�.
    public float GetRawSwipe()
    {
        return rawswipe;
    }

    // �ٸ� ��ũ��Ʈ���� realtouch ���� ������ �� �ֵ��� ��ȯ�ϴ� �޼��带 �߰��� �� �ֽ��ϴ�.
    public float GetRealTouch()
    {
        return realtouch;
    }

    // �ٸ� ��ũ��Ʈ���� realswipe ���� ������ �� �ֵ��� ��ȯ�ϴ� �޼��带 �߰��� �� �ֽ��ϴ�.
    public float GetRealSwipe()
    {
        return realswipe;
    }

    private void OnDisable()
    {
        swipeListener.OnSwipe.RemoveListener(OnSwipe);
    }
}
